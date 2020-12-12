using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; // Veri tabanı işlemleri yapacagımız için gerekli

namespace personeltakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //veri tabanı dosya yolu belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");

        //formlar arası degişkenler
        public static string tcno, adi, soyadi, yetki;

        //yerel degişkenler
        int hak = 3;
        bool durum = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Giriş";
            this.AcceptButton = button1;
            this.CancelButton = button2;
            label5.Text = Convert.ToString(hak);
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form2 frm2 = new Form2();
                            frm2.Show();
                            break;
                        }
                    }

                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Kullanıcı")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            break;
                        }
                    }
                }
                if (durum == false) hak--;

                baglantim.Close();
            }
            label5.Text = Convert.ToString(hak);
            if (hak == 0)
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş hakkı kalmadı!", "Personel Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
       
    }
}
