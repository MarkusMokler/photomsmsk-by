using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models;
using UnidecodeSharpFork;

namespace PhototechnicsUpload
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
                var readert = new StreamReader(dialog.FileName, Encoding.GetEncoding(1251)).ReadToEnd();
                CsvParser parser = new CsvParser(new StringReader(readert), new CsvConfiguration() { Delimiter = ";" });
                CsvReader reader = new CsvReader(parser);

                List<CsvPhototechnics> phototechnicses = new List<CsvPhototechnics>();
                while (reader.Read())
                {
                    try
                    {
                        phototechnicses.Add(reader.GetRecord<CsvPhototechnics>());

                    }
                    catch
                    {
                    }
                }
                //var stream = new StreamWriter(dialog.FileName + "1.csv");
                //CsvWriter writer = new CsvWriter(stream);
                //writer.WriteRecords();


                dataGridView1.DataSource = phototechnicses;
            }
        }

        private string GetProperties(CsvPhototechnics csvPhototechnicse)
        {

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(csvPhototechnicse.Properties);
            List<string> properties = new List<string>();
            Regex rgx = new Regex(@"^\s*|\s*$");
            StringBuilder sb = new StringBuilder();
            var links = doc.DocumentNode.SelectNodes("//a[@class]");
            if (links != null)
            {
                var elems = links.ToList();
                foreach (HtmlNode link in elems)
                {
                    if (link.GetAttributeValue("class", "").Contains("par-link"))
                    {
                        properties.Add(link.InnerText);
                        properties.Add(link.ParentNode.ParentNode.ParentNode.ChildNodes.Last(x => x.Name == "td").InnerText);
                    }

                }
                for (int i = 0; i < properties.Count - 1; i += 2)
                {

                    sb.AppendFormat("{0}:{1};", rgx.Replace(properties[i], ""), rgx.Replace(properties[i + 1], ""));
                }
            }

            return sb.ToString();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var source = dataGridView1.DataSource;
            var dialog = new SaveFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var stream = new StreamWriter(dialog.FileName + ".csv");
                CsvWriter writer = new CsvWriter(stream);
                writer.WriteRecords((IEnumerable<CsvPhototechnics>)source);
                stream.Close();
                stream.Dispose();
            }

        }

        private async void toolStripButton3_Click(object sender, EventArgs e)
        {
            toolStripButton3.Enabled = false;
            var source = ((IEnumerable<CsvPhototechnics>)dataGridView1.DataSource).ToList();
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = source.Count + 1;
            toolStripStatusLabel2.Text = toolStripProgressBar1.Maximum.ToString(CultureInfo.InvariantCulture);

            foreach (var phototechnics in source)
            {

                var dict = new List<KeyValuePair<string, string>>();

                try
                {
                    var tk =
                        JToken.Parse("[" +
                                     Regex.Replace(phototechnics.Properties, @"<[^>]*>", String.Empty)
                                         .Replace("{Раздел", "{\"Раздел")
                                         .Replace("{Тип", "{\"Тип")
                                         .Replace("\"}\"", "\"}") +
                                     "]");

                    dict = (tk.OfType<JObject>()
                        .SelectMany(token => token.Properties(),
                            (token, kvp) => new KeyValuePair<string, string>(kvp.Name, kvp.Value.ToString()))).ToList();
                }
                finally
                {
                }

                using (var context = new AppContext())
                {
                    var ph = await context.Phototechnicses.SingleOrDefaultAsync(x => x.Name == phototechnics.Name);


                    if (ph == null)
                    {
                        ph = new Phototechnics
                        {
                            ID = Guid.NewGuid(),
                            Raiting = new Raiting(),
                            Shortcut = gerString(phototechnics.Name),
                            CreationTime = DateTime.Now,
                        };
                        context.Phototechnicses.Add(ph);
                    }
                    else
                    {
                        if (ph.ParameterValues.Count > 1)
                        {
                            toolStripProgressBar1.Value++;
                            toolStripStatusLabel1.Text = toolStripProgressBar1.Value.ToString();

                            continue;
                        }
                    }
                    var paramss = await context.Parameters.ToListAsync();

                    List<ParameterValue> values =
                        dict.Select(x => new ParameterValue
                        {
                            ID = Guid.NewGuid(),
                            Parameter =
                                paramss.FirstOrDefault(y => y.Name == x.Key) ??
                                new Parameter { ID = Guid.NewGuid(), Name = x.Key },
                            Value = x.Value
                        }).ToList();


                    ph.Name = phototechnics.Name;
                    ph.TeaserImage = "http://ph1.photomsk.by/cameras/" + phototechnics.Image;
                    ph.Description = phototechnics.Description;
                    ph.ParameterValues = values;

                    if (ph.Brand == null)
                    {
                        var manufacture =
                            context.Brands.SingleOrDefault(x => x.Name == phototechnics.Brand) ??
                            new Brand { ID = Guid.NewGuid(), Name = phototechnics.Brand };
                        ph.Brand = manufacture;
                    }
                    if (ph.Category == null)
                    {
                        var type =
                            context.Categories.SingleOrDefault(x => x.Name == phototechnics.Category) ??
                            new Category { ID = Guid.NewGuid(), Name = phototechnics.Category };
                        ph.Category = type;
                    }

                    await context.SaveChangesAsync();
                }
                toolStripProgressBar1.Value++;
                toolStripStatusLabel1.Text = toolStripProgressBar1.Value.ToString();
            }
            toolStripButton3.Enabled = true;
        }

        private string gerString(string fio)
        {
            var str = fio.Replace("&", "").Replace(" ", "").Unidecode();
            var statusStrip1 = str.Length > 35 ? Guid.NewGuid().ToString("N") : str;

            using (AppContext context = new AppContext())
            {
                return context.Routes.Any(x => x.Shortcut == statusStrip1) ? Guid.NewGuid().ToString("N") : statusStrip1;
            }
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            AppContext context = new AppContext();
            foreach (var ph in context.Phototechnicses.ToList())
            {
                context.ParameterValues.RemoveRange(ph.ParameterValues);
                context.Entry(ph.Raiting).State = EntityState.Deleted;
                context.Phototechnicses.Remove(ph);

            }
            context.SaveChanges();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            AppContext context = new AppContext();
            //       var paramss = context.Phototechnicses.ToList().SelectMany(x => x.ParameterValues);
            //    var ids1 = paramss.Select(x => x.ID.ToString());
            //   context.Database.ExecuteSqlCommand("DELETE from ParameterValues");
            context.SaveChanges();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }


    }
}
