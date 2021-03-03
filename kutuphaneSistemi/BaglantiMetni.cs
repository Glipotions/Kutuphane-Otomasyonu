using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kutuphaneSistemi
{
    class BaglantiMetni
    {
        public OleDbConnection baglanti()
        {
            OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\hamza\\Desktop\\Kutuphane.accdb.mdb"); ;
            baglan.Open();
            return baglan;
        }
    }
}
