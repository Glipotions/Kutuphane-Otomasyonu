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
    public partial class OgrenciKitapListe : MetroFramework.Forms.MetroForm
    {
        public OgrenciKitapListe()
        {
            InitializeComponent();
        }

        OleDbConnection baglantı;
        OleDbCommand komut;
        OleDbDataAdapter da;
        OleDbDataReader read;

        
        void EmanetListele()
        {
            baglantı = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\hamza\\Desktop\\Kutuphane.accdb.mdb");
            baglantı.Open();
            da = new OleDbDataAdapter("Select *From OgrenciEmanet", baglantı);
            DataTable tablo = new DataTable(); //
            da.Fill(tablo);     //Tablonun görünmesini sağlar
            dataGridView1.DataSource = tablo; //Verilerin datagridview de görüntülenmesini istiyoruz
            baglantı.Close();
        }
        void TeslimEdilen()
        {
            
            baglantı.Open();
            da = new OleDbDataAdapter("Select *From OgrenciTeslim", baglantı);
            DataTable tablo = new DataTable(); //
            da.Fill(tablo);     //Tablonun görünmesini sağlar
            dataGridView2.DataSource = tablo; //Verilerin datagridview de görüntülenmesini istiyoruz
            baglantı.Close();
        }

        private void OgrenciKitapListe_Load(object sender, EventArgs e)
        {
            EmanetListele();
            TeslimEdilen();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable Emanet = new DataTable();
            baglantı.Open();
            da = new OleDbDataAdapter("select *from OgrenciEmanet where Tc like '%" + textBox1.Text + "%'", baglantı); 
            da.Fill(Emanet);
            dataGridView1.DataSource = Emanet;
            baglantı.Close();

            DataTable Teslim = new DataTable();
            baglantı.Open();
            da = new OleDbDataAdapter("select *from OgrenciTeslim where Tc like '%" + textBox1.Text + "%'", baglantı);
            da.Fill(Teslim);
            dataGridView2.DataSource = Teslim;
            baglantı.Close();
        }
    }
}
