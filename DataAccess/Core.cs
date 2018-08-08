using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class Core
    {
        private static MySqlConnection _connection = null;

        internal const String SCHEMA = "BDBOOKMUSIC_TESTE";
        
        public static MySqlConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection("Server=bdeasydist-teste.crekmjq8o5xh.sa-east-1.rds.amazonaws.com;" +
                                                  "Database=BDEASYDIST_TESTE;" +
                                                  "Uid=dba;Pwd=#Openppk;");
            }

            return _connection;
        }
    }
}
