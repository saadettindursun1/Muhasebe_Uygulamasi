using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marjinal
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
        private void formGetir(Form f1)
        {
            panel1.BackgroundImage = null;
            panel1.Controls.Clear();
            f1.TopLevel = false;
            // f1.Dock = DockStyle.Fill;
            panel1.Controls.Add(f1);
            f1.BringToFront();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            formGetir(f3);
        }
    }
}
