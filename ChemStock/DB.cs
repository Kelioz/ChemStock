using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemStock
{
    internal class DB
    {
        public SqlConnection connection = new SqlConnection($"Data Source = DESKTOP-Q09V9UI; Initial Catalog = ChemStock; Integrated Security = True");

        public void openConnetion()
        {
            connection.Open();
        }
        public void closeConnetion() 
        {
            connection.Close();
        }
        public SqlConnection getConnection() {  return connection; }
    }
}
