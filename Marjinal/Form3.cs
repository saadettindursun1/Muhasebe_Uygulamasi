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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("Server=localhost;port=3306;Database=muhasebe;Uid=root;Pwd='';");

        /*  public MySqlConnection baglantiYap()
          {

              if (con.State == ConnectionState.Closed)
              {
                  con.Open();
                  label1.Text = "İnternet Bağlantısı kuruldu.";
                  con.Close();
              }
              else
              {
                  label1.Text = "İnternet Bağlantısı Hata !!!";
              }
              return con;
          }*/

        private void personelGetir()
        {
            listView1.Items.Clear();

            con.Open();
            MySqlCommand cmd = new MySqlCommand("Select * From personel" , con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem item = new ListViewItem(dr["id"].ToString());
                item.SubItems.Add(dr["ad"].ToString());
                item.SubItems.Add(dr["soyad"].ToString());
                item.SubItems.Add(dr["tcno"].ToString());
                item.SubItems.Add(dr["giris_tarihi"].ToString());
                item.SubItems.Add(dr["brut_ucret"].ToString());
                item.SubItems.Add(dr["pozisyon"].ToString());
                item.SubItems.Add(dr["dogum_tarihi"].ToString());
                item.SubItems.Add(dr["cinsiyet"].ToString());
                item.SubItems.Add(dr["sgkNo"].ToString());
                item.SubItems.Add(dr["ehliyet"].ToString());

                listView1.Items.Add(item);
            }
            con.Close();
        }

        string[,] Dizi = new string[4,11];
        int diziDoldur = 0;
        private void personelIsimListe()
        {
            
            con.Open();
            MySqlCommand cmd = new MySqlCommand("Select * From personel", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            int diziDeger = 0;
            while (dr.Read())
            {
                Dizi[diziDeger, 0] = dr["id"].ToString();
                Dizi[diziDeger, 1] = dr["ad"].ToString();
                Dizi[diziDeger, 2] = dr["soyad"].ToString();
                Dizi[diziDeger, 3] = dr["tcno"].ToString();
                Dizi[diziDeger, 4] = dr["giris_tarihi"].ToString();
                Dizi[diziDeger, 5] = dr["brut_ucret"].ToString();
                Dizi[diziDeger, 6] = dr["pozisyon"].ToString();
                Dizi[diziDeger, 7] = dr["dogum_tarihi"].ToString();
                Dizi[diziDeger, 8] = dr["cinsiyet"].ToString();
                Dizi[diziDeger, 9] = dr["sgkNo"].ToString();
                Dizi[diziDeger, 10] = dr["ehliyet"].ToString();

                comboBox1.Items.Add(dr["id"]+"-"+dr["ad"] +" "+dr["soyad"]);
                //comboBox1.ValueMember = Dizi[diziDeger,0];
                diziDeger++;
              
            }
            diziDoldur = 1;
            con.Close();
        }


        private void personelGetirbyId(string id)
        {

            con.Open();
            MySqlCommand cmd = new MySqlCommand("Select * From personel where id='"+id+"'", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               ad2.Text= dr["ad"].ToString();
               soyad2.Text= dr["soyad"].ToString();
               tc2.Text= dr["tcno"].ToString();
               cinsiyet2.Text= dr["cinsiyet"].ToString();
               medeni_hali2.Text = dr["medeni_hali"].ToString();
               cocuk_sayi2.Text = dr["cocuk_sayi"].ToString();
               dogumTarihi2.Text = dr["dogum_tarihi"].ToString();
               sgkNo2.Text = dr["sgkNo"].ToString();
               ehliyet2.Text = dr["ehliyet"].ToString();
               pozisyon2.Text = dr["pozisyon"].ToString();
               ise_giris_tarihi2.Text = dr["giris_tarihi"].ToString();
               brut2.Text = dr["brut_ucret"].ToString();
               adres2.Text = dr["adres"].ToString();

               

            }
            con.Close();
        }


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
            if (ad.Text == "" || soyad.Text == "" || tcNo.Text == ""  || cocuk_sayi.Text == "" || sgk_no.Text == "" || pozisyon.Text == "" || brut_ucret.Text == "" || adres.Text == "")
            {
                hataMesaj = hataMesaj + "\n Lütfen Boş Alan Bırakmayınız";
                hataSayi++;
            }

            if (tcNo.Text.Length < 11)
            {
                hataMesaj = hataMesaj + "\n T.C.No 11 Hane olmalıdır";
                hataSayi++;
            }
            if(cinsiyet.Text=="Cinsiyet Seçim")
            {
                hataMesaj = hataMesaj + "\n Cinsiyet Seçimi Yapılmalı";
                hataSayi++;
            }

            if (medeni_hali.Text == "Medeni Hali Seçim")
            {
                hataMesaj = hataMesaj + "\n Medeni Hali Seçimi Yapılmalı";
                hataSayi++;
            }

            if (ehliyet.Text == "Ehliyet Seçim")
            {
                hataMesaj = hataMesaj + "\n Ehliyet Seçimi Yapılmalı";
                hataSayi++;
            }
        }
        private void temizle()
        {
            ad.Text = "";
            soyad.Text = "";
            tcNo.Text = "";
            cinsiyet.Text = "Cinsiyet Seçim";
            medeni_hali.Text = "Medeni Hali Seçim";
            cocuk_sayi.Text = "";
            dogum_tarihi.Text = "";
            sgk_no.Text = "";
            ehliyet.Text = "Ehliyet Seçim";
            pozisyon.Text = "";
            giris_tarihi.Text = "";
            brut_ucret.Text = "";
            adres.Text = "";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            hataKontrol();
            if (hataSayi == 0) { 
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.Connection = con;
            string sorgu = "insert into personel " +
                " (ad,soyad,tcno,cinsiyet,medeni_hali,cocuk_sayi,dogum_tarihi,sgkNo,ehliyet,pozisyon,giris_tarihi,brut_ucret,adres) " +
              "VALUES ('" + ad.Text + "','" + soyad.Text + "','" + tcNo.Text + "','" + cinsiyet.Text + "','" + medeni_hali.Text + "','" + cocuk_sayi.Text + "','"
              + dogum_tarihi.Text + "','" + sgk_no.Text + "','" + ehliyet.Text + "','" + pozisyon.Text + "','" + giris_tarihi.Text + "','" + brut_ucret.Text + "','" + adres.Text + "')";
            cmd.CommandText = sorgu;
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Başarılı");
            personelGetir();
            temizle();
            tabControl1.SelectedTab = tabPage1;
            }

            else
            {
                MessageBox.Show(hataMesaj);
            }

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            personelGetir();

        }

        private void medeni_hali_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (medeni_hali.Text == "Bekar")
            {
                cocuk_sayi.Enabled = false;
            }
        }

        private void listView1_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
        }



        private void listView1_Enter(object sender, EventArgs e)
        {

        }

        private void listView1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
          
        }
        string id = "";
        bool durum = false;
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            string isim = listView1.SelectedItems[0].SubItems[1].Text +" " + listView1.SelectedItems[0].SubItems[2].Text;
            label43.Text = "Seçilen Personel : " + isim;
            button4.Visible = true;
            label43.Visible = true;
            id = listView1.SelectedItems[0].SubItems[0].Text;
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
           
        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            
            personelGetirbyId(id);
            durum = true;
            tabControl1.SelectedTab = tabPage4;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
              MySqlCommand cmd = new MySqlCommand();
                con.Open();
                cmd.Connection = con;
                string sorgu =
         "update personel set ad='" + ad2.Text + "',soyad='" + soyad2.Text + "',tcno='" + tc2.Text + "',cinsiyet='" + cinsiyet2.Text 
         + "',medeni_hali='" + medeni_hali2.Text + "',cocuk_sayi='" + cocuk_sayi2.Text + "',dogum_tarihi='" + dogumTarihi2.Text + "',sgkNo='"
         + sgkNo2.Text + "',ehliyet='" + ehliyet2.Text + "',pozisyon='" + pozisyon2.Text + "',giris_tarihi='" + ise_giris_tarihi2.Text 
         + "',brut_ucret='" + brut_ucret.Text + "',adres='"  + adres2.Text + "' where id=" + id+ "";
                cmd.CommandText = sorgu;
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Güncelleme Başarılı");
                personelGetir();
                tabControl1.SelectedTab = tabPage1;
        
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"] && durum ==false)
            {
                tabControl1.SelectedTab = tabPage1;
                MessageBox.Show("Lütfen Personel Seçimi Yapınız", "Uyarı");                
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
            {
                if (diziDoldur == 0)
                { 
                 personelIsimListe();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Personeli silmek istediğinize emin misiniz?","Uyarı",  MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                MySqlCommand cmd = new MySqlCommand();
                con.Open();
                cmd.Connection = con;
                string sorgu = "DELETE FROM Personel where id='" + id + "'";
                cmd.CommandText = sorgu;
                cmd.ExecuteNonQuery();
                con.Close();
                button4.Visible = false;
                label43.Visible = false;
                MessageBox.Show("Silme İşlemi Başarılı");
                personelGetir();
                tabControl1.SelectedTab = tabPage1;
            }
            else if (dialogResult == DialogResult.No)
            {
            }
           
            
        
    }

        private void button2_Click(object sender, EventArgs e)
        {
           
          //  MessageBox.Show(id[0]);
        }

        public void tarihFark(DateTime digerTarih)
        {
            DateTime bugunTarihi = DateTime.Now;

            TimeSpan ts = bugunTarihi - digerTarih;
            MessageBox.Show(ts.Days.ToString());
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

          
            char ayir = '-';
            string[] parca = comboBox1.SelectedItem.ToString().Split(ayir);
              for(int i=0; i < Dizi.GetLength(0); i++)
              {
                if (Dizi[i, 0] == parca[0])
                {
                    label15.Text = "AYLIK BRÜT ÜCRET : " + Dizi[i, 5];
                    DateTime giristarihim = Convert.ToDateTime(Dizi[i, 4]);
                }
              }

        
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void dogum_tarihi_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
