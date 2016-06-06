using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CsvHelper;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Data.Models.Routes;
using UnidecodeSharpFork;

namespace PhotographerUploader
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
                CsvParser parser = new CsvParser(new StringReader(readert));
                CsvReader reader = new CsvReader(parser);

                List<CsvPhotographer> photographers = new List<CsvPhotographer>();
                while (reader.Read())
                {
                    try
                    {
                        photographers.Add(reader.GetRecord<CsvPhotographer>());

                    }
                    catch
                    {
                    }
                }

                foreach (var csvPhotographer in photographers)
                {
                    csvPhotographer.Phone = csvPhotographer.Phone.Replace("(", "").Replace(")", "")
                        .Replace("-", "").Replace(" ", "")
                        .Replace("mts", "").Replace("velcom", "")
                        .Replace("МТС", "")
                        .Replace("8029", "+37529")
                            .Replace("8033", "+37533")
                            .Replace("8025", "+37525")
                            .Replace("8044", "+37544");
                }

                Dictionary<string, CsvPhotographer> dicphoto = new Dictionary<string, CsvPhotographer>();

                foreach (var csvPhotographer in photographers)
                {
                    CsvPhotographer pho;
                    if (dicphoto.TryGetValue(csvPhotographer.Phone, out pho))
                    {
                        if (string.IsNullOrEmpty(pho.Description))
                            pho.Description = csvPhotographer.Description;
                    }
                    else
                    {
                        dicphoto.Add(csvPhotographer.Phone, csvPhotographer);
                    }
                }

                var phots = dicphoto.Values.ToList();

                dataGridView1.DataSource = phots;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var source = dataGridView1.DataSource;
            var dialog = new SaveFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var stream = new StreamWriter(dialog.FileName + ".csv");
                CsvWriter writer = new CsvWriter(stream);
                writer.WriteRecords((IEnumerable<CsvPhotographer>)source);
                stream.Close();
                stream.Dispose();
            }

        }

        private async void toolStripButton5_Click(object sender, EventArgs e)
        {
            toolStripButton5.Enabled = false;
            var source = ((IEnumerable<CsvPhotographer>)dataGridView1.DataSource).ToList();
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = source.Count + 1;

            foreach (var csvPhotographer in source)
            {
                using (AppContext context = new AppContext())
                {
                    var phone = context.Phones.FirstOrDefault(x => x.Number == csvPhotographer.Phone);
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
                            TeaserImage = "http://ph1.photomsk.by/avatars/" + csvPhotographer.Avatar,

                        };

                        var pphone = new Phone()
                        {
                            ID = Guid.NewGuid(),
                            Number = csvPhotographer.Phone,
                            UserPhone = new UserPhone()
                            {
                                ID = Guid.NewGuid(),
                                Confirm = false,
                                DateAdded = DateTime.Now,
                                User = new UserInformation()
                                {
                                    ID = Guid.NewGuid(),
                                    FirstName = csvPhotographer.FIO.Split(' ').First(),
                                    LastName = csvPhotographer.FIO.Split(' ').Last(),
                                    UserPhoto = "http://ph1.photomsk.by/avatars/" + csvPhotographer.Avatar,
                                    Roles = new Collection<Role>()
                                    {
                                        new Role()
                                        {
                                            ID = Guid.NewGuid(),
                                            AccessStatus = AccessStatus.Owner,
                                            Route = photographer
                                        }
                                    }
                                },
                            },
                            RoutesPhone = new Collection<RoutesPhone>()
                            {
                                new RoutesPhone()
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
                            TeaserImage = "http://ph1.photomsk.by/avatars/" + csvPhotographer.Avatar,
                        };

                        phone.RoutesPhone.Add(new RoutesPhone()
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
            }
            toolStripProgressBar1.Value = 0;
            toolStripButton5.Enabled = true;
        }

        private string gerString(string fio)
        {
            var str = fio.Replace("&", "").Replace(" ", "_").Unidecode();
            return str.Length > 25 ? str.Substring(0, 24) : str;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var readert = new StreamReader(dialog.FileName, Encoding.UTF8).ReadToEnd();
                CsvParser parser = new CsvParser(new StringReader(readert));
                parser.Configuration.Delimiter = ";";
                CsvReader reader = new CsvReader(parser);

                while (reader.Read())
                {
                    try
                    {
                        shootings.Add(reader.GetRecord<CSVShooting>());

                    }
                    catch
                    {
                    }
                }
            }
        }

        public List<CSVShooting> shootings = new List<CSVShooting>();

        private async void toolStripButton7_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = shootings.Count + 1;

            foreach (var shooting in shootings)
            {
                using (AppContext context = new AppContext())
                {
                    var shotcut = gerString(shooting.Photographer);
                    var pho = context.Photographers.FirstOrDefault(x => x.Shortcut == shotcut);
                    if (pho == null)
                        continue;
                    if (pho.Shootings.Any(x => x.Title == shooting.Title))
                        continue;
                    var photos = shooting.photos.Split(',');
                    pho.AddWallPost(new Views
                    {
                        ID = Guid.NewGuid(),
                        Date = DateTime.Now,
                        ParticipationType = ParticipationType.Owner,
                        WallPost = new Shooting()
                        {
                            ID = Guid.NewGuid(),
                            Date = DateTime.Now,
                            Desctiption = shooting.Description,
                            Title = shooting.Title,
                            LikeStatus = new LikeStatus { ID = Guid.NewGuid() },
                            Attacments = photos.Select(x => new Photo { ID = Guid.NewGuid(), FileName = x, UploadDate = DateTime.Now, Url = "http://ph1.photomsk.by/shootings/" + x }).ToList<Attachment>(),
                        }
                    });

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
            }
            toolStripProgressBar1.Value = 0;
        }

        private async void toolStripButton8_Click(object sender, EventArgs e)
        {
            using (AppContext context = new AppContext())
            {
                toolStripProgressBar1.Minimum = 0;
                toolStripProgressBar1.Maximum = context.Photographers.Count() + 1;

                foreach (var photographer in context.Photographers.ToList())
                {
                    if (photographer.CoverImage == "http://ph1.photomsk.by/65/aa/1b//65aa1b88f34ff36f7a97f3bd789c5100228fbd24b5a2f392957cbe1d33619385.jpg" && photographer.Shootings.Any())
                        photographer.CoverImage =
                            photographer.Shootings.SelectMany(x => x.Attacments.OfType<Photo>())
                                .OrderBy(x => new Guid())
                                .First()
                                .Url;
                    toolStripProgressBar1.Value++;
                }
                await context.SaveChangesAsync();
            }

        }
    }
}
