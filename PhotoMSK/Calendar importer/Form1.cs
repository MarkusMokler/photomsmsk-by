using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using PhotoMSK.Data;

namespace Calendar_importer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        AppContext _context = new AppContext();
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApiKey = "AIzaSyA_kHmzh1ywenkfIp7T6O6RuH0PbHgA22c";
        static string ApplicationName = "Photolab";
        UserCredential credential;


        public void LoadData()
        {
            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var request = service.CalendarList.List();
            request.MaxResults = 100;
            var elements = request.Execute();
            foreach (var calendarListEntry in elements.Items)
            {
                var id = calendarListEntry.Id;
                comboBox1.Items.Add(new ComboItem()
                {
                    ID = calendarListEntry.Id,
                    Name = calendarListEntry.Summary

                });
            }

        }

        public void events()
        {
            var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var item = (ComboItem)comboBox1.SelectedItem;

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List(item.ID);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 1000;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();

            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    importingEventBindingSource.List.Add(new ImportingEvent()
                    {
                        ID = eventItem.Id,
                        Start = eventItem.Start.DateTime.Value.AddHours(1),
                        End = eventItem.End.DateTime.Value.AddHours(1),
                        Description = eventItem.Summary
                    });
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            events();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var halls = _context.Halls.Where(x => x.Photostudio.Shortcut == "mimika");

            foreach (var hall in halls)
            {
                comboBox2.Items.Add(new ComboItem
                {
                    ID = hall.Calendar.ID.ToString(),
                    Name = hall.Name
                });
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var data = (ComboItem)comboBox2.SelectedItem;
            var id = Guid.Parse(data.ID);
            using (var _context = new AppContext())
            {
                var list = _context.Events.Where(x => x.Calendar.ID == id && x.GoogleID != null).ToList();

                foreach (var item in from @event in list
                                     let items = (ICollection<ImportingEvent>)importingEventBindingSource.List
                                     select items.FirstOrDefault(x => x.ID == @event.GoogleID))
                {
                    if (item != null)
                        item.Sync = true;
                }

                list = _context.Events.Where(x => x.Calendar.ID == id && x.GoogleID == null).ToList();

                foreach (var item in from @event in list
                                     let items = (ICollection<ImportingEvent>)importingEventBindingSource.List
                                     select items.FirstOrDefault(x => x.Start == @event.Start && x.End == @event.End))
                {
                    if (item != null)
                        item.Sync = true;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = 0;
            int total = ((ICollection<ImportingEvent>)importingEventBindingSource.List).Count(x => x.Sync == false);
            var iitem = (ComboItem)comboBox2.SelectedItem;
            foreach (EventEdit edit in ((ICollection<ImportingEvent>)importingEventBindingSource.List).Where(x => x.Sync == false)
                .Select(item => new EventEdit(item, Guid.Parse(iitem.ID))))
            {
                edit.Text = $"импорт ( {i}/{total} )";
                edit.ShowDialog(this);
                i++;
            }
        }
    }
}

public class ComboItem
{
    public string ID { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
