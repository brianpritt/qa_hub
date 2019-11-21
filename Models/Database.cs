using System;
using MySql.Data.MySqlClient;
using QAHub;

namespace QAHub.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBCOnfiguration.ConnectionString);
            return conn;
        }
    }
}