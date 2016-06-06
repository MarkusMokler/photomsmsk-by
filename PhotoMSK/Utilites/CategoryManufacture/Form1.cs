using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CategoryManufacture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'photomskDataSet2.Brands' table. You can move, or remove it, as needed.
            this.brandsTableAdapter.Fill(this.photomskDataSet2.Brands);
            // TODO: This line of code loads data into the 'photomskDataSet1.Categories' table. You can move, or remove it, as needed.
            this.categoriesTableAdapter.Fill(this.photomskDataSet1.Categories);
            // TODO: This line of code loads data into the 'photomskDataSet.CategoryBrand' table. You can move, or remove it, as needed.
            this.categoryBrandTableAdapter.Fill(this.photomskDataSet.CategoryBrand);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Guid.NewGuid().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            categoryBrandTableAdapter.Update(photomskDataSet.CategoryBrand);
        }
    }
}
