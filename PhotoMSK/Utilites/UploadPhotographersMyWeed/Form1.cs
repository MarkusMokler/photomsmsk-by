using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models;

using UnidecodeSharpFork;

namespace UploadPhotographersMyWeed
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var readert = new StreamReader(dialog.FileName, Encoding.UTF8).ReadToEnd();
                var parser = new CsvParser(new StringReader(readert));

                parser.Configuration.Delimiter = ";";
                parser.Configuration.RegisterClassMap<PhotographerCSVMap>();

                var reader = new CsvReader(parser);

                var photographers = new List<PhotographerCSV>();
                while (reader.Read())
                {
                    try
                    {
                        photographers.Add(reader.GetRecord<PhotographerCSV>());

                    }
                    catch (Exception exception)
                    {
                        exception.GetBaseException();
                    }
                }
                var set = new HashSet<string>();
                foreach (var photographerCsv in photographers)
                {
                    var obj = JsonConvert.DeserializeObject<JObjectPhotographer>(photographerCsv.Table.Replace("{", "{\"").Replace("}\"", "}").Replace("&hellip", "").Replace(@"\", "/"));
                    photographerCsv.Json = obj;


                }

                new TextEdit(string.Join(",", set)).ShowDialog();

                dataGridView1.DataSource = photographers;
            }
        }

        private string gerString(string fio)
        {
            var str = fio.Replace("&", "").Replace(" ", "_").Unidecode();
            return str.Length > 25 ? str.Substring(0, 24) : str;
        }

        private async void toolStripButton2_Click(object sender, EventArgs e)
        {
            const string imageUrl = "http://ph1.photomsk.by/avatars1/";

            toolStripButton2.Enabled = false;
            var source = ((IEnumerable<PhotographerCSV>)dataGridView1.DataSource).ToList();
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = source.Count + 1;

            foreach (var csvPhotographer in source)
            {
                if (!csvPhotographer.GetPhones().Any())
                {
                    toolStripProgressBar1.Value++;
                    continue;
                }
                using (var context = new AppContext())
                {
                    string sch = gerString(csvPhotographer.FIO);
                    if (context.Routes.Any(x => x.Shortcut == sch))
                    {
                        toolStripProgressBar1.Value++;
                        continue;
                    }
                    Phone phone = null;
                    foreach (var phone1 in csvPhotographer.GetPhones())
                    {
                        phone = context.Phones.FirstOrDefault(x => x.Number == phone1);
                        if (phone != null)
                            break;
                    }


                    if (phone == null)
                    {
                        var photographer = new Photographer()
                        {
                            ID = Guid.NewGuid(),
                            City = csvPhotographer.City,
                            Description = csvPhotographer.Description,
                            FirstName = csvPhotographer.FIO.Split(' ').First(),
                            LastName = csvPhotographer.FIO.Split(' ').Last(),
                            Pro = false,
                            Verified = false,
                            Shortcut = gerString(csvPhotographer.FIO),
                            TeaserImage = imageUrl + csvPhotographer.TeaserImage,
                            Site = csvPhotographer.Json.Сайт,
                            Facebook = csvPhotographer.Json.Facebook,
                            Vk = csvPhotographer.Json.ВКонтакте,
                            Instagram = csvPhotographer.Json.Instagram,
                            Twitter = csvPhotographer.Json.Twitter,
                            Skype = csvPhotographer.Json.Skype,


                        };

                        var pphone = new Phone()
                        {
                            ID = Guid.NewGuid(),
                            Number = csvPhotographer.GetPhones().First(),
                            UserPhone = new UserPhone
                            {
                                ID = Guid.NewGuid(),
                                Confirm = false,
                                DateAdded = DateTime.Now,
                                User = new UserInformation()
                                {
                                    ID = Guid.NewGuid(),
                                    FirstName = csvPhotographer.FIO.Split(' ').First(),
                                    LastName = csvPhotographer.FIO.Split(' ').Last(),
                                    UserPhoto = imageUrl + csvPhotographer.TeaserImage,
                                    Roles = new Collection<Role>()
                                    {
                                        new Role
                                        {
                                            ID = Guid.NewGuid(),
                                            AccessStatus = AccessStatus.Owner,
                                            Route = photographer
                                        }
                                    }
                                },
                            },
                            RoutesPhone = new Collection<RoutesPhone>
                            {
                                new RoutesPhone
                                {
                                    ID = Guid.NewGuid(),
                                    Confirm = false,
                                    ConfirmCode = 0,
                                    DateAdded = DateTime.Now,
                                    Entity = photographer,
                                }
                            }
                        };

                        context.Phones.Add(pphone);
                    }
                    else
                    {
                        if (phone.RoutesPhone.Any())
                        {
                            toolStripProgressBar1.Value++;
                            continue;
                        }
                        var photographer = new Photographer
                        {
                            ID = Guid.NewGuid(),
                            City = csvPhotographer.City,
                            Description = csvPhotographer.Description,
                            FirstName = csvPhotographer.FIO.Split(' ').First(),
                            LastName = csvPhotographer.FIO.Split(' ').Last(),
                            Pro = false,
                            Verified = false,
                            Shortcut = gerString(csvPhotographer.FIO),
                            TeaserImage = imageUrl + csvPhotographer.TeaserImage,

                            Site = csvPhotographer.Json.Сайт,
                            Facebook = csvPhotographer.Json.Facebook,
                            Vk = csvPhotographer.Json.ВКонтакте,
                            Instagram = csvPhotographer.Json.Instagram,
                            Twitter = csvPhotographer.Json.Twitter,
                            Skype = csvPhotographer.Json.Skype,
                        };

                        phone.RoutesPhone.Add(new RoutesPhone
                        {
                            ID = Guid.NewGuid(),
                            Confirm = true,
                            ConfirmCode = 0,
                            DateAdded = DateTime.Now,
                            Entity = photographer
                        });

                        phone.UserPhone.User.Roles.Add(new Role
                        {
                            ID = Guid.NewGuid(),
                            AccessStatus = AccessStatus.Owner,
                            Route = photographer,
                        });
                    }
                    try
                    {
                        await context.SaveChangesAsync();
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }
                    }

                }
                toolStripProgressBar1.Value++;
                toolStripStatusLabel1.Text = "" + toolStripProgressBar1.Value;
            }
            toolStripProgressBar1.Value = 0;
            toolStripButton2.Enabled = true;
        }
    }
}
