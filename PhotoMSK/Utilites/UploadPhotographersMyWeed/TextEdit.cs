using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadPhotographersMyWeed
{
    public partial class TextEdit : Form
    {
        public TextEdit(string str)
        {
            InitializeComponent();
            richTextBox1.Text = str;
        }
    }
}
