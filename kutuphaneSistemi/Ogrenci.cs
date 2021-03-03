using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;




namespace kutuphaneSistemi
{
    public partial class Ogrenci : Form
    {
        public Ogrenci()
        {
            InitializeComponent();
        }
        //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\kisileri.mdb

        //OleDbConnection baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\hamza\\Desktop\\Kutuphane.accdb.mdb");



        OleDbConnection baglantı;
        OleDbCommand komut;
        OleDbDataAdapter da;
        

        void KisiListele()
        {
            baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\hamza\\Desktop\\Kutuphane.accdb.mdb"); ;
            baglantı.Open();
            da = new OleDbDataAdapter("Select *From Ogrenci", baglantı);
            DataTable tablo = new DataTable(); //
            da.Fill(tablo);     //Tablonun görünmesini sağlar
            dataGridView1.DataSource = tablo; //Verilerin datagridview de görüntülenmesini istiyoruz
            baglantı.Close();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            //Silme işlemi
            string sorgu = "Delete From Ogrenci Where Tc=@Tc";
            komut = new OleDbCommand(sorgu, baglantı);
            komut.Parameters.AddWithValue("@Tc", dataGridView1.CurrentRow.Cells[0].Value);
            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();
            KisiListele();
        }

        private void Ogrenci_Load(object sender, EventArgs e)
        {
            KisiListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtTc.Text = null;
            txtAd.Text = null;
            txtMail.Text = null;
            txtTelefon.Text = null;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {//Ekleme Kısmı
                string sorgu = "Insert into Ogrenci (Tc,Ad,Email,Telefon) values (@p1,@p2,@p3,@p4)";
                komut = new OleDbCommand(sorgu, baglantı);
                komut.Parameters.AddWithValue("@p1", txtTc.Text);
                komut.Parameters.AddWithValue("@p2", txtAd.Text);
                komut.Parameters.AddWithValue("@p3", txtMail.Text);
                komut.Parameters.AddWithValue("@p4", txtTelefon.Text);

                baglantı.Open();
                komut.ExecuteNonQuery(); //uygulama işlemi
                baglantı.Close();
                KisiListele();
            }
            catch { MessageBox.Show($"BU TC'YE KAYITLI ÖĞRENCİ ZATEN VAR!! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

                string sorgu = "Update Ogrenci Set Ad=@ad Where Tc=@no";
                komut = new OleDbCommand(sorgu, baglantı);
                komut.Parameters.AddWithValue("@ad", txtAd.Text);
                komut.Parameters.AddWithValue("@no", Convert.ToInt32(txtTc.Text));
            baglantı.Open();
            komut.ExecuteNonQuery();
                baglantı.Close();
                KisiListele();

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Seçtiğimiz satırın Textboxlarda görünmesini sağlar
            txtTc.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtMail.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
