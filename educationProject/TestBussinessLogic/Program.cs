using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TestBussinessLogic
{
    [Serializable]
    class MyClass
    {
        public string name = "gfdgdg";
        public int count = 1000;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cnStrBuilder = new SqlConnectionStringBuilder
            {
                InitialCatalog = "AutoBD",
                IntegratedSecurity = true,
                DataSource = "Home",
                ConnectTimeout = 30
            };
            var sql = new SqlConnection(cnStrBuilder.ConnectionString);
            sql.Open();
            var sqlCommande = new SqlCommand("Insert Into Text (Text) Values ('fsdfsd')", sql);
            sqlCommande.ExecuteNonQuery();

        }
    }
}
