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
    public partial class Form2 : Form
    {
      
    public Form2()
        {
            InitializeComponent();
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

        public int id;
        private void Form2_Load(object sender, EventArgs e)
        {
           // panel1.BackgroundImage = Image.FromFile("images/dene.png");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            formGetir(f3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            formGetir(f5);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.BackColor=Color.LightSeaGreen;
                checkBox1.ForeColor = Color.Black;
            }
            if (checkBox1.Checked == false)
            {
                this.BackColor=Color.FromArgb(0,41,45);
                checkBox1.ForeColor = Color.White;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            formGetir(f4);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            formGetir(f6);
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
