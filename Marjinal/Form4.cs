using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Marjinal
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("Server=localhost;port=3306;Database=muhasebe;Uid=root;Pwd='';");

        public void sadeceRakam(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        string hataMesaj;
        int hataSayi;
        private void hataKontrol()
        {
            hataMesaj = "";
            hataSayi = 0;
            if (cari_adi.Text == "" || yetkili.Text == "" || mail.Text == "" || vergi_daire.Text == "" || adres.Text == "")
            {
                hataMesaj = hataMesaj + "\n Lütfen Boş Alan Bırakmayınız";
                hataSayi++;
            }

            if (vergi_no.Text.Length != 11)
            {
                hataMesaj = hataMesaj + "\n Vergi/T.C.  Numarası 11 Hane olmalıdır";
                hataSayi++;
            }
            if (telefon.Text.Length != 11)
            {
                hataMesaj = hataMesaj + "\n Telefon Numarası 11 Hane olmalıdır";
                hataSayi++;
            }
            if (telefon2.Text.Length != 11)
            {
                hataMesaj = hataMesaj + "\n İkinci Telefon Numarası 11 Hane olmalıdır";
                hataSayi++;
            }
            if (cari_tipi.Text == "Cari Tipi")
            {
                hataMesaj = hataMesaj + "\n Cari Tipi Seçimi Yapılmalı";
                hataSayi++;
            }
            if (cari_tipi.Text == "Cari Türü")
            {
                hataMesaj = hataMesaj + "\n Cari Türü Seçimi Yapılmalı";
                hataSayi++;
            }

        }
        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

        private void cariGetir()
        {
            listView1.Items.Clear();

            con.Open();
            MySqlCommand cmd = new MySqlCommand("Select * From cari", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem item = new ListViewItem(dr["id"].ToString());
                item.SubItems.Add(dr["cari_adi"].ToString());
                item.SubItems.Add(dr["cari_tipi"].ToString());
                item.SubItems.Add(dr["yetkili"].ToString());
                item.SubItems.Add(dr["telefon"].ToString());
                item.SubItems.Add(dr["telefon2"].ToString());
                item.SubItems.Add(dr["mail"].ToString());
                item.SubItems.Add(dr["cari_tur"].ToString());
                item.SubItems.Add(dr["vergi_no"].ToString());
                item.SubItems.Add(dr["vergi_daire"].ToString());
                item.SubItems.Add(dr["kayit_tarih"].ToString());
                item.SubItems.Add(dr["adres"].ToString());
                collection.Add(dr["cari_adi"].ToString());
                listView1.Items.Add(item);
            }
            con.Close();


        }

      
            


        private void cariGetirbyId(string id)
        {

            con.Open();
            MySqlCommand cmd = new MySqlCommand("Select * From cari where id='" + id + "'", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                t1.Text = dr["cari_adi"].ToString();
                t2.Text = dr["cari_tipi"].ToString();
                t3.Text = dr["yetkili"].ToString();
                t4.Text = dr["telefon"].ToString();
                t5.Text = dr["telefon2"].ToString();
                t6.Text = dr["mail"].ToString();
                t7.Text = dr["cari_tur"].ToString();
                t8.Text = dr["vergi_no"].ToString();
                t9.Text = dr["vergi_daire"].ToString();
                t10.Text = dr["adres"].ToString();



            }
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            hataKontrol();
            if (hataSayi == 0)
            {
                MySqlCommand cmd = new MySqlCommand();
                con.Open();
                cmd.Connection = con;
                string sorgu = "insert into cari " +
                    " (cari_adi,cari_tipi,yetkili,telefon,telefon2,mail,cari_tur,vergi_no,vergi_daire,kayit_tarih,adres) " +
                  "VALUES ('" + cari_adi.Text + "','" + cari_tipi.Text + "','" + yetkili.Text + "','" + telefon.Text + "','" + telefon2.Text + "','" + mail.Text + "','"
                  + cari_tur.Text + "','" + vergi_no.Text + "','" + vergi_daire.Text + "','" + kayit_tarihi.Text + "','" + adres.Text +  "')";
                cmd.CommandText = sorgu;
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Başarılı");
                cariGetir();
                //temizle();
                tabControl1.SelectedTab = tabPage1;
            }

            else
            {
                MessageBox.Show(hataMesaj);
            }
        }

        string id = "";
        bool durum = false;
        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = collection;
            cariGetir();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            string isim = listView1.SelectedItems[0].SubItems[1].Text;
            label43.Text = "Seçilen Cari : " + isim;
            button4.Visible = true;
            label43.Visible = true;
            id = listView1.SelectedItems[0].SubItems[0].Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cariGetirbyId(id);
            durum = true;
            tabControl1.SelectedTab = tabPage3;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.Connection = con;
            string sorgu =
     "update cari set cari_adi='" + t1.Text + "',cari_tipi='" + t2.Text + "',yetkili='" + t3.Text + "',telefon='" + t4.Text
     + "',telefon2='" + t5.Text + "',mail='" + t6.Text + "',cari_tur='" + t7.Text + "',vergi_no='"
     + t8.Text + "',vergi_daire='" + t9.Text + "',adres='" + t10.Text + "' where id=" + id + "";
            cmd.CommandText = sorgu;
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Güncelleme Başarılı");
            cariGetir();
            tabControl1.SelectedTab = tabPage1;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"] && durum == false)
            {
                tabControl1.SelectedTab = tabPage1;
                MessageBox.Show("Lütfen Cari Seçimi Yapınız", "Uyarı");
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {              
                    cariGetir();               
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Cari'yi silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                MySqlCommand cmd = new MySqlCommand();
                con.Open();
                cmd.Connection = con;
                string sorgu = "DELETE FROM cari where id='" + id + "'";
                cmd.CommandText = sorgu;
                cmd.ExecuteNonQuery();
                con.Close();
                button4.Visible = false;
                label43.Visible = false;
                MessageBox.Show("Silme İşlemi Başarılı");
                cariGetir();
                tabControl1.SelectedTab = tabPage1;
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            con.Open();
            MySqlCommand cmd = new MySqlCommand("Select * From cari where cari_adi like '" + textBox1.Text + "%'", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem item = new ListViewItem(dr["id"].ToString());
                item.SubItems.Add(dr["cari_adi"].ToString());
                item.SubItems.Add(dr["cari_tipi"].ToString());
                item.SubItems.Add(dr["yetkili"].ToString());
                item.SubItems.Add(dr["telefon"].ToString());
                item.SubItems.Add(dr["telefon2"].ToString());
                item.SubItems.Add(dr["mail"].ToString());
                item.SubItems.Add(dr["cari_tur"].ToString());
                item.SubItems.Add(dr["vergi_no"].ToString());
                item.SubItems.Add(dr["vergi_daire"].ToString());
                item.SubItems.Add(dr["kayit_tarih"].ToString());
                item.SubItems.Add(dr["adres"].ToString());
                collection.Add(dr["cari_adi"].ToString());
                listView1.Items.Add(item);
            }
            con.Close();
        }
    }
}
