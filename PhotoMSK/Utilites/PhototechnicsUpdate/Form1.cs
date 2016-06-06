using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models;
using PhotoMSK.Models.EditViewModel;
using UnidecodeSharpFork;

namespace PhototechnicsUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly List<OnlinerProductModel> _list = new List<OnlinerProductModel>();
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var readert = new StreamReader(dialog.FileName, Encoding.GetEncoding("windows-1251")).ReadToEnd();
                var parser = new CsvParser(new StringReader(readert));

                parser.Configuration.Delimiter = ";";
                parser.Configuration.RegisterClassMap<OnlinerProductModelMap>();

                var reader = new CsvReader(parser);

                while (reader.Read())
                {
                    try
                    {
                        _list.Add(reader.GetRecord<OnlinerProductModel>());

                    }
                    catch (Exception exception)
                    {
                        exception.GetBaseException();
                    }
                }

                dataGridView1.DataSource = _list;
            }
        }

        private async void toolStripButton2_Click(object sender, EventArgs e)
        {
            var list = new List<OnlinerProductModel>();
            var count = 0;
            var count1 = 0;
            foreach (var onlinerProductModel in _list)
            {
                using (var context = new AppContext())
                {
                    var str = onlinerProductModel.Vendor + " " + onlinerProductModel.Model;
                    var item = await context.Phototechnicses.SingleOrDefaultAsync(x => x.Name == str);
                    if (item == null)
                    {
                        var shortc = gerString(str);
                        item = await context.Phototechnicses.SingleOrDefaultAsync(x => x.Shortcut == shortc);
                    }
                    if (item != null)
                    {
                        if (item.Brand == null)
                        {
                            var manufacture =
                                context.Brands.SingleOrDefault(x => x.Name == onlinerProductModel.Vendor) ??
                                new Brand() { ID = Guid.NewGuid(), Name = onlinerProductModel.Vendor };
                            item.Brand = manufacture;
                        }
                        if (item.Category == null)
                        {
                            var type =
                                context.Categories.SingleOrDefault(x => x.Name == onlinerProductModel.Category) ??
                                new Category { ID = Guid.NewGuid(), Name = onlinerProductModel.Category };
                            item.Category = type;
                        }
                        await context.SaveChangesAsync();
                        count++;
                    }
                    else
                    {
                        item = new Phototechnics
                        {
                            ID = Guid.NewGuid(),
                            Name = str,
                            Description = "",
                            Shortcut = gerString(str),
                            Raiting = new Raiting(),
                        };
                        if (item.Brand == null)
                        {
                            var manufacture =
                                context.Brands.SingleOrDefault(x => x.Name == onlinerProductModel.Vendor) ??
                                new Brand() { ID = Guid.NewGuid(), Name = onlinerProductModel.Vendor };
                            item.Brand = manufacture;
                        }
                        if (item.Category == null)
                        {
                            var type =
                                context.Categories.SingleOrDefault(x => x.Name == onlinerProductModel.Category) ??
                                new Category { ID = Guid.NewGuid(), Name = onlinerProductModel.Category };
                            item.Category = type;
                        }
                        context.Phototechnicses.Add(item);
                        await context.SaveChangesAsync();
                        count1++;
                    }
                    toolStripStatusLabel1.Text = "" + count;
                    toolStripStatusLabel2.Text = "" + count1;
                }
            }
            dataGridView1.DataSource = list;
        }
        private string gerString(string fio)
        {
            var str = fio.Replace("&", "").Replace(" ", "").Unidecode();
            return str.Length > 35 ? Guid.NewGuid().ToString("N") : str;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (var context = new AppContext())
            {
                var technics = context.Phototechnicses.ToList();
                foreach (var phototechnicse in technics)
                {
                    phototechnicse.Category = null;
                }
                context.SaveChanges();
            }

            using (var context = new AppContext())
            {
                var list = context.Categories.ToList();
                foreach (var item in list)
                    context.Entry(item).State = EntityState.Deleted;
                context.SaveChanges();
            }

        }

        private async void toolStripButton4_Click(object sender, EventArgs e)
        {
            var fftid = Guid.Parse("9DF4B82C-EFCE-4B2E-95C2-27B865625C17");
            int count = 0;
            foreach (var onlinerProductModel in _list)
            {
                using (var context = new AppContext())
                {
                    var str = onlinerProductModel.Vendor + " " + onlinerProductModel.Model;
                    var item = await context.Phototechnicses.SingleOrDefaultAsync(x => x.Name == str);
                    if (item == null)
                    {
                        var shortc = gerString(str);
                        item = await context.Phototechnicses.SingleOrDefaultAsync(x => x.Shortcut == shortc);
                    }

                    if (item == null)
                        continue;

                    var pp = context.PricePositions.SingleOrDefault(x => x.PhotoshopID == fftid && x.PhototechnicsID == item.ID);
                    if (pp == null)
                    {
                        pp = new PricePosition()
                        {
                            Currency = onlinerProductModel.Currency,
                            Delivery = onlinerProductModel.Delivery,
                            ID = Guid.NewGuid(),
                            IsCashless = onlinerProductModel.IsCashless,
                            IsCredit = onlinerProductModel.IsCredit,
                            Price = double.Parse(onlinerProductModel.Price),
                            Status = onlinerProductModel.Status,
                            Warranty = onlinerProductModel.Warranty,
                            PhotoshopID = fftid,
                            PhototechnicsID = item.ID
                        };
                        context.PricePositions.Add(pp);
                    }
                    else
                    {
                        pp.Currency = onlinerProductModel.Currency;
                        pp.Delivery = onlinerProductModel.Delivery;
                        pp.IsCashless = onlinerProductModel.IsCashless;
                        pp.IsCredit = onlinerProductModel.IsCredit;
                        pp.Price = double.Parse(onlinerProductModel.Price);
                        pp.Status = onlinerProductModel.Status;
                        pp.Warranty = onlinerProductModel.Warranty;
                    }
                    await context.SaveChangesAsync();
                    count++;
                    toolStripStatusLabel1.Text = string.Format("{0} : {1}", _list.Count, count);
                }

            }
        }
    }
}
