using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.ActivityStream;
using PhotoMSK.Data.Models.ShoppingCart;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Event;

namespace Calendar_importer
{
    public partial class EventEdit : Form
    {
        AppContext _context = new AppContext();
        private ImportingEvent item;
        private readonly Guid _calendarID;

        public EventEdit()
        {
            InitializeComponent();
        }

        public EventEdit(ImportingEvent item, Guid calendarID)
        {
            InitializeComponent();
            this.item = item;
            _calendarID = calendarID;
            textBox2.Text = item.Description;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            //say you want to do a search when user types 3 or more chars
            if (t?.Text.Length >= 3)
            {
                var arr = FindByPhone(t.Text);


                userInformationViewModelBindingSource.List.Clear();

                foreach (var detailse in arr)
                {
                    userInformationViewModelBindingSource.List.Add(detailse);
                }

                if (arr.Count == 0)
                {
                    try
                    {
                        var strName = t.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (strName.Length > 1)
                        {
                            _firstName.Text = strName[0];
                            _lastName.Text = strName[1];
                        }
                        else
                        {
                            _phone.Text = t.Text;
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
            }
        }

        public IList<UserInformationViewModel.Details> FindByPhone(string search)
        {
            var strName = search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var firstName = "";
            var lastName = "";
            if (strName.Length > 1)
            {
                firstName = strName[0];
                lastName = strName[1];
                return (FindByName(firstName, lastName));
            }

            var str = search;
            if (search.StartsWith("+") == false)
                str = "+" + search;

            var phones = _context.Phones.Where(x => x.Number.StartsWith(str))
                .OrderBy(x => x.Number)
                .Select(x => x.UserPhone.User).Take(10).ToList().Where(x => x != null).ToList();
            return Mapper.Map<IList<UserInformationViewModel.Details>>(phones);
        }

        public IList<UserInformationViewModel.Details> FindByName(string firstName, string lastName)
        {
            var phones = _context.Phones.Where(x => x.UserPhone.User.FirstName.Contains(firstName) &&
            x.UserPhone.User.LastName.Contains(lastName) ||
            x.UserPhone.User.FirstName.Contains(lastName) &&
            x.UserPhone.User.LastName.Contains(firstName))
                .OrderBy(x => x.Number)
                .Select(x => x.UserPhone.User).Take(10).ToList().Where(x => x != null).ToList();
            return Mapper.Map<IList<UserInformationViewModel.Details>>(phones);
        }


        private IList<SaveEventViewModel> AdminBooking(BookingModel model)
        {

            var userInformationID = GetUserInformationID(model);
            var ids = model.Events.Select(x => x.CalendarID).ToList();
            var rawcalendars = _context.Calendars.Where(x => ids.Contains(x.ID)).ToList();


            var adminUid = Guid.Parse("d6fa4a98-1927-4da9-8bb7-36ff5eecde41");
            //var adminUid = Guid.Parse("d2b65c8a-a79e-43e8-b3a1-5666397238c8");

            var calendars =
                rawcalendars.Where(
                    x =>
                        x.RouteEntity.Participants.Any(
                            z =>
                                z.AccessStatus == AccessStatus.Owner ||
                                z.AccessStatus == AccessStatus.Administrator && z.UserInformation.ID == adminUid))
                    .ToList();

            var strs = new List<string>();

            Order order = new Order
            {
                ID = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                UserInformationID = userInformationID,
                Status = _context.Statuses.Find(StatusID.NewOrder) ?? new Status() { ID = StatusID.NewOrder, Title = "Новый Заказ", Description = "Толькло что созданный заказ" }
            };



            foreach (var @event in model.Events)
            {
                var calendar = calendars.FirstOrDefault(x => x.ID == @event.CalendarID);
                if (calendar == null) continue;

                var newevent = new Event
                {
                    ID = Guid.NewGuid(),
                    Calendar = calendar,
                    Title = "Занято",
                    Start = @event.Start,
                    End = @event.End,
                    AllDay = @event.AllDay,
                    GoogleID = @event.GoogleID,
                    UserInformationID = userInformationID,
                    Description = @event.Description,
                    Price = @event.Price ?? calendar.GetCost(@event.Start, @event.End),
                    Tags = "Импортировано",
                    CreatedByID = adminUid
                };

                order.Add(newevent);

                var str =
                    $" {calendar.GetName()} в {calendar.GetRouteName()} на {@event.Start.Day:00}.{@event.Start.Month:00} c {@event.Start.Hour:00}:{@event.Start.Minute:00} по {@event.End.Hour:00}:{@event.End.Minute:00}";
                strs.Add(str);


                var activity = new EventActivity()
                {
                    ID = Guid.NewGuid(),
                    Event = newevent,
                    UserID = adminUid,
                    Title = "Импортировано",
                    Description = str,
                    ActivityTime = DateTime.Now,
                    Route = calendar.RouteEntity,
                    State = EventActivityState.Created
                };
                _context.Activities.Add(activity);
            }


            _context.Orders.Add(order);
            _context.SaveChanges();
            return model.Events;
        }


        private static Guid GetUserInformationID(BookingModel model)
        {
            if (model.UserInformation != null && model.UserInformation.ID != Guid.Empty)
                return model.UserInformation.ID;

            using (var context = new AppContext())
            {
                var userphone = model.UserInformation.UserPhone.Replace("-", "").Replace(" ", "");

                var phone = context.Phones.SingleOrDefault(x => x.Number == userphone);

                Guid id;
                if (phone != null)
                {
                    if (phone.UserPhone != null)
                        return phone.UserPhone.User.ID;
                    id = Guid.NewGuid();

                    phone.UserPhone = new UserPhone
                    {
                        ID = id,
                        Confirm = false,
                        DateAdded = DateTime.Now,
                        User = new UserInformation
                        {
                            ID = id,
                            FirstName = model.UserInformation.FirstName,
                            LastName = model.UserInformation.LastName,

                        }
                    };
                    context.SaveChanges();
                    return id;
                }

                id = Guid.NewGuid();

                var info = new UserInformation
                {
                    ID = id,
                    FirstName = model.UserInformation.FirstName,
                    LastName = model.UserInformation.LastName,
                    Phones = new HashSet<UserPhone>
                    {
                        new UserPhone
                        {
                            ID = id,
                            Confirm = false,
                            DateAdded = DateTime.Now,
                            Phone = new Phone
                            {
                                ID = id,
                                Number = userphone,
                                DateLastSending = DateTime.Now
                            }
                        }
                    }
                };
                context.UserInformations.Add(info);
                context.SaveChanges();
                return id;
            }
        }

        public class BookingModel
        {
            /// <summary>
            /// Это поле показывает бронирование идёт от администратора или от клиента
            /// </summary>
            public bool IsAdmin { get; set; }
            public UserInformationViewModel.Summary UserInformation { get; set; }
            public IList<SaveEventViewModel> Events { get; set; }
            public string Error { get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BookingModel model = new BookingModel()
            {
                IsAdmin = true,
                Events = new List<SaveEventViewModel>
                {
                    new SaveEventViewModel
                    {
                        Admin = true,
                        CalendarID = _calendarID,
                        Description = item.Description,
                        GoogleID = item.ID,
                        Start = item.Start,
                        End = item.End,
                        EventState = EventState.Created,

                    }

                },
                UserInformation = new UserInformationViewModel.Summary()
                {
                    FirstName = _firstName.Text,
                    LastName = _lastName.Text,
                    UserPhone = _phone.Text
                }
            };

            AdminBooking(model);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var index = dataGridView1.SelectedRows[0].Index;

                var list = (ICollection<UserInformationViewModel.Details>)userInformationViewModelBindingSource.List;
                var item = list.ToList()[index];
                _firstName.Text = item.FirstName;
                _lastName.Text = item.LastName;
                _phone.Text = item.UserPhone;
            }
            catch (Exception ex)
            {
                _firstName.Text = "";
                _lastName.Text = "";
                _phone.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BookingModel model = new BookingModel()
            {
                IsAdmin = true,
                Events = new List<SaveEventViewModel>
                {
                    new SaveEventViewModel
                    {
                        Admin = true,
                        CalendarID = _calendarID,
                        Description = item.Description,
                        GoogleID = item.ID,
                        Start = item.Start,
                        End = item.End,
                        EventState = EventState.Created,

                    }

                },
                UserInformation = new UserInformationViewModel.Summary()
                {
                    ID = Guid.Parse("00000000-0000-0000-0000-000000000001")
                }
            };

            AdminBooking(model);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
