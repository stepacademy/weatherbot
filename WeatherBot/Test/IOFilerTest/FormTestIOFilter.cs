using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherBot.IOFilter;

namespace Test.IOFilerTest
{
    public partial class FormTestIOFilter : Form
    {
        IOFilterator iof;
        public FormTestIOFilter()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            iof = new IOFilterator();
        }

        private void Messaging(string msgin, string msgout)
        {
            textBox1.Text += msgin;
            textBox2.Text += msgout;
        }
    }
}
