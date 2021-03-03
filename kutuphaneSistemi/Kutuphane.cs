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
    public partial class Kutuphane : Form
    {
        public Kutuphane()
        {
            InitializeComponent();
        }
        OleDbConnection baglantı;
        OleDbCommand komut;
        OleDbDataAdapter da;
        OleDbDataReader read;
        void KutuphaneListele()
        {
            baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\hamza\\Desktop\\Kutuphane.accdb.mdb"); 
            baglantı.Open();
            da = new OleDbDataAdapter("Select *From Kutuphane", baglantı);
            DataTable tablo = new DataTable(); //
            da.Fill(tablo);     //Tablonun görünmesini sağlar
            dataGridView1.DataSource = tablo; //Verilerin datagridview de görüntülenmesini istiyoruz
            baglantı.Close();

        }

        void AlinanlariListele()
        {
            //baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\\Kutuphane.accdb.mdb");
            baglantı.Open();
            da = new OleDbDataAdapter("Select *From AlinanKitaplar", baglantı);
            DataTable tablo = new DataTable(); //
            da.Fill(tablo);     //Tablonun görünmesini sağlar
            dataGridView2.DataSource = tablo; //Verilerin datagridview de görüntülenmesini istiyoruz
            baglantı.Close();
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            
            string sorgu = "select *from Ogrenci where Tc='"+txtTc.Text+"'";
            komut = new OleDbCommand(sorgu, baglantı);

            baglantı.Open();
            read = komut.ExecuteReader();
            while(read.Read())
            {
                txtAd.Text = read["Ad"].ToString();
                //txtSoyad.Text = read["Soyad"].ToString();
            }
            baglantı.Close();

        }

        private void Kutuphane_Load(object sender, EventArgs e)
        {
            KutuphaneListele();
            AlinanlariListele();

            DataGridViewCellStyle styleYesil = new DataGridViewCellStyle();
            styleYesil.BackColor = Color.Green;
            dataGridView2.Columns[4].DefaultCellStyle=styleYesil;


            //[0] aldığı tarih    [1] teslim etmesi gereken tarih

            DateTime d1;
            DateTime d2;
            DataGridViewCellStyle styleKırmızı = new DataGridViewCellStyle();
            DataGridViewCellStyle styleSarı = new DataGridViewCellStyle();
            
            styleKırmızı.BackColor = Color.Red;
            styleSarı.BackColor = Color.Yellow;
            
            //styleKırmızı.ForeColor = Color.White;
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                d1 = Convert.ToDateTime(DateTime.Now.ToShortDateString());  //bugünün tarihi
                d2 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value);  //vermesi gereken tarih
                TimeSpan ts = d1 - d2;
                
                if (ts.Days > 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle = styleKırmızı;
                }
                else if(ts.Days>=-2)
                {
                    dataGridView1.Rows[i].DefaultCellStyle = styleSarı;
                }
                
            }


        }
        // teslim aldı teslim etti


        private void txtKod_TextChanged(object sender, EventArgs e)
        {
            string sorgu = "select *from Kitaplar where KitapKodu='" + txtKod.Text + "'";
            komut = new OleDbCommand(sorgu, baglantı);


            baglantı.Open();
            read = komut.ExecuteReader();
            while (read.Read())
            {
                cbKitapAdi.Text = read["KitapAdı"].ToString();
                txtYazar.Text = read["Yazar"].ToString();
                txtKitapTuru.Text = read["Tür"].ToString();
                txtSayfaSayisi.Text = read["SayfaSayısı"].ToString();
                txtBasimTarihi.Text = read["BasımTarihi"].ToString();
                
            }
            baglantı.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string sorgu = "Insert into Kutuphane (TeslimAldıgıTarih,TeslimEdilecekTarih,Tc,Ad,KitapKodu,KitapAdı) values (@TeslimAldıgıTarih,@TeslimEdilecekTarih,@Tc,@Ad,@KitapKodu,@KitapAdı)";

            komut = new OleDbCommand(sorgu, baglantı);


            komut.Parameters.AddWithValue("@TeslimAldıgıTarih", DateTime.Now.ToShortDateString());
            komut.Parameters.AddWithValue("@TeslimEdilecekTarih", DateTime.Now.AddDays(15).ToShortDateString());

            komut.Parameters.AddWithValue("@Tc", txtTc.Text);
            komut.Parameters.AddWithValue("@Ad", txtAd.Text);

            komut.Parameters.AddWithValue("@KitapKodu", Convert.ToInt32(txtKod.Text));
            komut.Parameters.AddWithValue("@KitapAdı", cbKitapAdi.Text);

            baglantı.Open();
            komut.ExecuteNonQuery(); //uygulama işlemi
            baglantı.Close();
            KutuphaneListele();


            sorgu = "Insert into OgrenciEmanet (Tc,Ad,KitapKodu,KitapAdı) values (@Tc,@Ad,@KitapKodu,@KitapAdı)";

            komut = new OleDbCommand(sorgu, baglantı);
            komut.Parameters.AddWithValue("@Tc", txtTc.Text);
            komut.Parameters.AddWithValue("@Ad", txtAd.Text);
            //komut.Parameters.AddWithValue("@Soyad", txtSoyad.Text);

            komut.Parameters.AddWithValue("@KitapKodu", Convert.ToInt32(txtKod.Text));
            komut.Parameters.AddWithValue("@KitapAdı", cbKitapAdi.Text);
            baglantı.Open();
            komut.ExecuteNonQuery(); //uygulama işlemi
            baglantı.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //and Ad=@Ad and Soyad=@Soyad and KitapAdı=@KitapAdı and Yazar=@Yazar and Tür=@Tür and SayfaSayısı=@SayfaSayısı and BasımTarihi=@BasımTarihi

            string sorgu = "Update Kutuphane Set Durum=@Durum Where Tc=@Tc and KitapKodu=@KitapKodu ";
            komut = new OleDbCommand(sorgu, baglantı);
            komut.Parameters.AddWithValue("@Durum", "Teslim Etti");

            komut.Parameters.AddWithValue("@Tc", txtTc.Text);

            //komut.Parameters.AddWithValue("@Ad", txtAd.Text);
            //komut.Parameters.AddWithValue("@Soyad", txtSoyad.Text);

            komut.Parameters.AddWithValue("@KitapKodu", txtKod.Text);

            //komut.Parameters.AddWithValue("@KitapAdı", cbKitapAdi.Text);
            //komut.Parameters.AddWithValue("@Yazar", txtYazar.Text);
            //komut.Parameters.AddWithValue("@Tür", txtKitapTuru.Text);
            //komut.Parameters.AddWithValue("@SayfaSayısı", txtSayfaSayisi.Text);
            //komut.Parameters.AddWithValue("@BasımTarihi", txtBasimTarihi.Text);

            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();
            KutuphaneListele();
        }


        private void cbKitapAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sorgu = "select *from Kitaplar where KitapAdı='" + cbKitapAdi.SelectedItem + "'";
            komut = new OleDbCommand(sorgu, baglantı);

            baglantı.Open();
            read = komut.ExecuteReader();
            while (read.Read())
            {
                txtKod.Text = read["KitapKodu"].ToString();
                txtYazar.Text = read["Yazar"].ToString();
                txtKitapTuru.Text = read["Tür"].ToString();
                txtSayfaSayisi.Text = read["SayfaSayısı"].ToString();
                txtBasimTarihi.Text = read["BasımTarihi"].ToString();

            }
            baglantı.Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Alınan Kitaplar Kısmı
            string sorgu = "Insert into AlinanKitaplar (TeslimEdilenTarih,Tc,Ad,KitapKodu,KitapAdı) values (@Tarih,@Tc,@Ad,@KitapKodu,@KitapAdı)";
            baglantı.Open();
            komut = new OleDbCommand(sorgu, baglantı);
            komut.Parameters.AddWithValue("@Tarih", DateTime.Now.ToShortDateString());
            komut.Parameters.AddWithValue("@Tc", dataGridView1.CurrentRow.Cells["Tc"].Value.ToString());
            komut.Parameters.AddWithValue("@Ad", dataGridView1.CurrentRow.Cells["Ad"].Value.ToString());
            //komut.Parameters.AddWithValue("@Soyad", dataGridView1.CurrentRow.Cells["Soyad"].Value.ToString());
            komut.Parameters.AddWithValue("@KitapKodu", dataGridView1.CurrentRow.Cells["KitapKodu"].Value.ToString());
            komut.Parameters.AddWithValue("@KitapAdı", dataGridView1.CurrentRow.Cells["KitapAdı"].Value.ToString());
            komut.ExecuteNonQuery();
            baglantı.Close();
            AlinanlariListele();


            // Öğrenci Teslim Ettiği kitaplar kısmı 
            sorgu = "Insert into OgrenciTeslim (Tc,Ad,KitapKodu,KitapAdı) values (@Tc,@Ad,@KitapKodu,@KitapAdı)";
            baglantı.Open();
            komut = new OleDbCommand(sorgu, baglantı);
            komut.Parameters.AddWithValue("@Tc", dataGridView1.CurrentRow.Cells["Tc"].Value.ToString());
            komut.Parameters.AddWithValue("@Ad", dataGridView1.CurrentRow.Cells["Ad"].Value.ToString());
            //komut.Parameters.AddWithValue("@Soyad", dataGridView1.CurrentRow.Cells["Soyad"].Value.ToString());
            komut.Parameters.AddWithValue("@KitapKodu", dataGridView1.CurrentRow.Cells["KitapKodu"].Value.ToString());
            komut.Parameters.AddWithValue("@KitapAdı", dataGridView1.CurrentRow.Cells["KitapAdı"].Value.ToString());
            komut.ExecuteNonQuery();
            baglantı.Close();


            // Kitap Teslim Geçmişi Tablosuna Ekleme kısmı 
            sorgu = "Insert into KitapTeslimGecmisi (KitapKodu,KitapAdı,TeslimAlmaTarihi,TeslimEdilmeTarihi,Tc,Ad) values (@KitapKodu,@KitapAdı,@TeslimAlmaTarihi,@TeslimEdilmeTarihi,@Tc,@Ad)";
            baglantı.Open();
            komut = new OleDbCommand(sorgu, baglantı);
                
            //komut.Parameters.AddWithValue("@Soyad", dataGridView1.CurrentRow.Cells["Soyad"].Value.ToString());
            komut.Parameters.AddWithValue("@KitapKodu", dataGridView1.CurrentRow.Cells["KitapKodu"].Value.ToString());
            komut.Parameters.AddWithValue("@KitapAdı", dataGridView1.CurrentRow.Cells["KitapAdı"].Value.ToString());
            komut.Parameters.AddWithValue("@TeslimAlmaTarihi", dataGridView1.CurrentRow.Cells["TeslimAldıgıTarih"].Value.ToString());
            komut.Parameters.AddWithValue("@TeslimEdilmeTarihi", DateTime.Now.ToShortDateString());
            komut.Parameters.AddWithValue("@Tc", dataGridView1.CurrentRow.Cells["Tc"].Value.ToString());
            komut.Parameters.AddWithValue("@Ad", dataGridView1.CurrentRow.Cells["Ad"].Value.ToString());
            komut.ExecuteNonQuery();
            baglantı.Close();



        // TABLOLARDAN SİLME KOMUTLARI AŞAĞIDA


        // Öğrenci Emanet Tablosundan Silme Komutları
        sorgu = "Delete From OgrenciEmanet Where Tc=@Tc and KitapKodu=@KitapKodu";
            komut = new OleDbCommand(sorgu, baglantı);
            komut.Parameters.AddWithValue("@Tc", dataGridView1.CurrentRow.Cells["Tc"].Value);
            komut.Parameters.AddWithValue("@KitapKodu", dataGridView1.CurrentRow.Cells["KitapKodu"].Value);
            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();

            // Kütüphaneden Teslim edilen kısmından silinme komutları
            sorgu = "Delete From Kutuphane Where Tc=@Tc and KitapKodu=@KitapKodu";
            komut = new OleDbCommand(sorgu, baglantı);
            komut.Parameters.AddWithValue("@Tc", dataGridView1.CurrentRow.Cells["Tc"].Value);
            komut.Parameters.AddWithValue("@KitapKodu", dataGridView1.CurrentRow.Cells["KitapKodu"].Value);
            baglantı.Open();
            komut.ExecuteNonQuery();
            baglantı.Close();
            KutuphaneListele();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["Tc"].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells["Ad"].Value.ToString();
            //txtSoyad.Text = dataGridView1.CurrentRow.Cells["Soyad"].Value.ToString();


            txtKod.Text = dataGridView1.CurrentRow.Cells["KitapKodu"].Value.ToString();
            cbKitapAdi.Text = dataGridView1.CurrentRow.Cells["KitapAdı"].Value.ToString();
            //txtYazar.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            //txtKitapTuru.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            //txtSayfaSayisi.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            //txtBasimTarihi.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();

            DateTime d1;
            DateTime d2;

            //styleKırmızı.ForeColor = Color.White;
            try
            {
                d1 = Convert.ToDateTime(DateTime.Now.ToShortDateString());  //bugünün tarihi
                d2 = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value);  //vermesi gereken tarih
                                                                                   //d2 = Convert.ToDateTime(dataGridView1.Rows[i].Cells[1].Value);
                TimeSpan ts = d1 - d2;
                if (ts.Days > 0)
                { lblBorc.Text = Convert.ToString(ts.Days); }
                else
                { lblBorc.Text = "0"; }
            }
            catch { }
        }
    }
}
