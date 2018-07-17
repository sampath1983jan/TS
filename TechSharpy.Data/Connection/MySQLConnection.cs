using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechSharpy.Data.Connection
{
    public class MySQL : DataConnection
    {
        MySql.Data.MySqlClient.MySqlConnection Connection;
        public MySql.Data.MySqlClient.MySqlConnection GetConnection() {
            if (Connection == null) {
                Connection = new MySql.Data.MySqlClient.MySqlConnection(getConnectionstring());
             }
            return Connection;
        }
               

        public override string getConnectionstring()
        {
            string Connection = "server=localhost;user=root;database=tshris;password=admin312;";
            return Connection;
        }
        
        public override bool TestConnection()
        {
            try
            {
                Connection = new MySql.Data.MySqlClient.MySqlConnection(getConnectionstring());
                Connection.Open();
                Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Invalid connection or unable to open connection", ex.InnerException);
                
            }
        }
       
        //public void Open() {
        //    Connection.Open();
        //}
        //public void Close() {
        //    Connection.Close();
        //}
    }
}
