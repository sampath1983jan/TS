using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TechSharpy.Data.ABS;

namespace TechSharpy.Data.DataType
{
    public class MySQL : DataAccess
    {
        Connection.MySQL Connection = new Connection.MySQL();
        DataTable dt ;
        MySqlCommand cmd;
        private MySql.Data.MySqlClient.MySqlDataAdapter da;
        public MySQL() {
              cmd = new MySqlCommand();
        }
        public override bool ExecuteTQuery(TQuery tQuery)
        {
            cmd = Connection.GetConnection().CreateCommand();
            try
            {
                Connection.GetConnection().Open();
                cmd.CommandText = tQuery.toString();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to create table", ex.InnerException);
            }
            finally
            {
                //Connection.Close();
                cmd.Dispose();
            }
            return true;
        }
        public override int ExecuteQuery(Query DataQuery)
        {
            int Result;
            string stQuery = DataQuery.toString();
            try
            {
                 cmd = new MySqlCommand(stQuery, Connection.GetConnection());
                Connection.GetConnection().Open();
                Result = cmd.ExecuteNonQuery();
                Connection.GetConnection().Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Execute this query", ex.InnerException);
            }
            finally
            {
                Connection.GetConnection().Close();
            }
            return Result;
        }
        public override DataTable GetData(Query DataQuery)
        {
            try
            {
                Connection.GetConnection().Open();
                dt = new System.Data.DataTable();
                 cmd = new MySqlCommand();
                cmd.CommandText = DataQuery.toString();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = Connection.GetConnection();
                using (da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                Connection.GetConnection().Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get data", ex.InnerException);
            }
            finally
            {
                Connection.GetConnection().Close();
                cmd.Dispose();
            }
            return dt;
        }

       
    }
}
