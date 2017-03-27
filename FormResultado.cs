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
    public partial class FormResultado : Form
    {
        public List<string> resultados = new List<string>();
        public string criterio;

        public FormResultado()
        {
            InitializeComponent();
        }

        private void FormResultado_Load(object sender, EventArgs e)
        {
            var cnt = resultados.Count / 2;
            Text = @"Resultados para '" + criterio + @"' : " + cnt + @" registro" + (cnt>1?"s":"");

            foreach (var resultado in resultados)
            {
                listBox1.Items.Add(resultado.Replace(Environment.NewLine, "§"));
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==Keys.Escape)
                Close();
            if (e.KeyCode == Keys.Return)
                MessageBox.Show(listBox1.SelectedItem.ToString(), @".:: Detalhes ::.");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(listBox1, listBox1.SelectedItem.ToString());
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(listBox1.SelectedItem.ToString().Replace("§§", Environment.NewLine));
        }
    }
}
