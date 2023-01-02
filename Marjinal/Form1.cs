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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="admin" && (textBox2.Text == "123" || textBox2.Text ==""))
            {
                Form2 f2 = new Form2();
                this.Hide();
                f2.Show();
                
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
            }
        }
    }
}
