using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyJournal
{
    public partial class FormPomodorocs : Form
    {
        public FormPomodorocs()
        {
            InitializeComponent();
        }

        public void UpdateLabel(string time)
        {
            label1.Text = time;
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
