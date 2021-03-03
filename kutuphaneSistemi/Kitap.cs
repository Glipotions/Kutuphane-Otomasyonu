using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kutuphaneSistemi
{
    public partial class Kitap : MetroFramework.Forms.MetroForm
    {
        public Kitap()
        {
            InitializeComponent();
        }
        OleDbConnection baglantı;
        OleDbCommand komut;
        OleDbDataAdapter da;

        void KitapListele()
        {
            baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\hamza\\Desktop\\Kutuphane.accdb.mdb"); ;
            baglantı.Open();
            da = new OleDbDataAdapter("Select *From Kitaplar", baglantı);
            DataTable tablo = new DataTable(); //
            da.Fill(tablo);     //Tablonun görünmesini sağlar
            dataGridView1.DataSource = tablo; //Verilerin datagridview de görüntülenmesini istiyoruz
            baglantı.Close();
        }

        void KitapListele1()
        {
            baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\hamza\\Desktop\\Kutuphane.accdb.mdb"); ;
            baglantı.Open();
            da = new OleDbDataAdapter("Select *From Kitaplar", baglantı);
            DataTable tablo = new DataTable(); //
            da.Fill(tablo);     //Tablonun görünmesini sağlar
            dataGridView2.DataSource = tablo; //Verilerin datagridview de görüntülenmesini istiyoruz
            baglantı.Close();
        }

        void KitapEmanetGoruntule()
        {
            baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\hamza\\Desktop\\Kutuphane.accdb.mdb"); ;
            baglantı.Open();
            da = new OleDbDataAdapter("Select *From Kutuphane", baglantı);
            DataTable tablo = new DataTable(); //
            da.Fill(tablo);     //Tablonun görünmesini sağlar
            dataGridView3.DataSource = tablo; //Verilerin datagridview de görüntülenmesini istiyoruz
            baglantı.Close();
        }

        void KitapTeslimGoruntule()
        {
            baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\hamza\\Desktop\\Kutuphane.accdb.mdb"); ;
            baglantı.Open();
            da = new OleDbDataAdapter("Select *From KitapTeslimGecmisi", baglantı);
            DataTable tablo = new DataTable(); //
            da.Fill(tablo);     //Tablonun görünmesini sağlar
            dataGridView4.DataSource = tablo; //Verilerin datagridview de görüntülenmesini istiyoruz
            baglantı.Close();
        }



        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string sorgu = "Insert into Kitaplar (KitapKodu,KitapAdı,Yazar,Tür,SayfaSayısı,BasımTarihi) values (@KitapKodu,@KitapAdı,@Yazar,@Tür,@SayfaSayısı,@BasımTarihi)";
                komut = new OleDbCommand(sorgu, baglantı);
                komut.Parameters.AddWithValue("@KitapKodu", Convert.ToInt32(txtKod.Text));
                komut.Parameters.AddWithValue("@KitapAdı", txtKitapAdi.Text);
                komut.Parameters.AddWithValue("@Yazar", txtYazar.Text);
                komut.Parameters.AddWithValue("@Tür", txtKitapTuru.Text);
                komut.Parameters.AddWithValue("@SayfaSayısı", txtSayfaSayisi.Text);
                komut.Parameters.AddWithValue("@BasımTarihi", txtBasimTarihi.Text);
                baglantı.Open();
                komut.ExecuteNonQuery(); //uygulama işlemi
                baglantı.Close();
                KitapListele();
            }
            catch { MessageBox.Show($"BU KİTAP KODU İLE KAYITLI KİTAP ZATEN VAR!! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void Kitap_Load(object sender, EventArgs e)
        {
            KitapListele();
            KitapListele1();
            KitapEmanetGoruntule();
            KitapTeslimGoruntule();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //Silme işlemi
            string sorgu = "Delete From Kitaplar Where KitapKodu=@no";
            komut = new OleDbCommand(sorgu, baglantı);
            komut.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells[0].Value);
            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();
            KitapListele();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtKod.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtKitapAdi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtKitapTuru.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtSayfaSayisi.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtBasimTarihi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
    
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "Update Kitaplar Set KitapAdı=@p1,Yazar=@p2,Tür=@p3,SayfaSayısı=@p4,BasımTarihi=@p5 Where KitapKodu=@no";
            komut = new OleDbCommand(sorgu, baglantı);

            
            komut.Parameters.AddWithValue("@p1", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@p2", txtYazar.Text);
            komut.Parameters.AddWithValue("@p3", txtKitapTuru.Text);
            komut.Parameters.AddWithValue("@p4", txtSayfaSayisi.Text);
            komut.Parameters.AddWithValue("@p5", txtBasimTarihi.Text);

            komut.Parameters.AddWithValue("@no", Convert.ToInt32(txtKod.Text));

            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();
            KitapListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtKod.Text = null;
            txtKitapAdi.Text = null;
            txtKitapTuru.Text = null;
            txtYazar.Text = null;
            txtSayfaSayisi.Text = null;
            txtBasimTarihi.Text = null;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable Emanet = new DataTable();
            baglantı.Open();
            da = new OleDbDataAdapter("select *from Kitaplar where KitapKodu like '%" + txtKodlaAra.Text + "%'", baglantı);
            da.Fill(Emanet);
            dataGridView2.DataSource = Emanet;
            baglantı.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable Emanet = new DataTable();
            baglantı.Open();
            da = new OleDbDataAdapter("select *from Kitaplar where KitapAdı like '%" + txtAdAra.Text + "%'", baglantı);
            da.Fill(Emanet);
            dataGridView2.DataSource = Emanet;
            baglantı.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtAdAra.Text = null;
            txtKodlaAra.Text = null;
            KitapListele1();
        }

        private void btnKitapGecmisKod_Click(object sender, EventArgs e)
        {
            DataTable Emanet = new DataTable();
            baglantı.Open();
            da = new OleDbDataAdapter("select *from Kutuphane where KitapKodu like '" + txtKitapGecmisKod.Text + "%'", baglantı);
            da.Fill(Emanet);
            dataGridView3.DataSource = Emanet;
            baglantı.Close();


            DataTable Teslim = new DataTable();
            baglantı.Open();
            da = new OleDbDataAdapter("select *from KitapTeslimGecmisi where KitapKodu like '" + txtKitapGecmisKod.Text + "%'", baglantı);
            da.Fill(Teslim);
            dataGridView4.DataSource = Teslim;
            baglantı.Close();

        }

        private void btnKitapGecmisAd_Click(object sender, EventArgs e)
        {
            DataTable Emanet = new DataTable();
            baglantı.Open();
            da = new OleDbDataAdapter("select *from Kutuphane where KitapAdı like '" + txtKitapGecmisAd.Text + "%'", baglantı);
            da.Fill(Emanet);
            dataGridView3.DataSource = Emanet;
            baglantı.Close();


            DataTable Teslim = new DataTable();
            baglantı.Open();
            da = new OleDbDataAdapter("select *from KitapTeslimGecmisi where KitapAdı like '" + txtKitapGecmisAd.Text + "%'", baglantı);
            da.Fill(Teslim);
            dataGridView4.DataSource = Teslim;
            baglantı.Close();


        }

        private void btnGecmisYeni_Click(object sender, EventArgs e)
        {
            txtKitapGecmisKod.Text = null;
            txtKitapGecmisAd.Text = null;
            KitapEmanetGoruntule();
            KitapTeslimGoruntule();
        }
    }
}
