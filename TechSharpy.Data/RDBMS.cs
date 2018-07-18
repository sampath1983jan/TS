using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using TechSharpy.Data.QueryAttribute;
using TechSharpy.Data.ABS;

namespace TechSharpy.Data
{

    //obsoluted
    public enum DataSourceType {
        MYSQL=0,
        SQLSERVER=1,
        ORACLE=2,
        DB2=3,
    }

    public class DataBase 
    {
        public DataSourceType Type;
        public DataTable GetResult;
        public bool Result;
        private DataAccess da;
        public DataBase() {
            this.Type =  DataSourceType.MYSQL;           
            GetResult = new DataTable();
            Result = false;
            try
            {
                da = new DataType.MySQL();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int getNextID(string EntityID)
        {
            // DataAccess ds;
            Query sQuery = new QueryBuilder(QueryType._Select).AddField("keyvalue", "s_entitykeys")
                .AddWhere(0, "s_entitykeys", "entityid", FieldType._String, Operator._Equal, EntityID.ToString(), Condition._None);
            System.Data.DataTable dt = new System.Data.DataTable();                       
            dt = da.GetData(sQuery);
            int Nextid = 0;
            if (dt.Rows.Count > 0)
            {
                Nextid = Convert.ToInt32(dt.Rows[0]["keyvalue"]);
                Nextid = Nextid + 1;
                Query iQuery = new QueryBuilder(QueryType._Update
                     ).AddTable("s_entitykeys")
                     .AddField("keyvalue", "s_entitykeys", FieldType._Number, "", Nextid.ToString())
                     .AddWhere(0, "s_entitykeys", "EntityID", FieldType._String, Operator._Equal, EntityID.ToString());
                da.ExecuteQuery(iQuery);
            }
            else
            {
                Nextid = Nextid + 1;
                Query iQuery = new QueryBuilder(QueryType._Insert
                   ).AddTable("s_entitykeys")
                   .AddField("keyvalue", "s_entitykeys", FieldType._Number, "", Nextid.ToString())
                    .AddField("EntityID", "s_entitykeys", FieldType._String, "", EntityID.ToString())
                     .AddField("LastUPD", "s_entitykeys", FieldType._DateTime, "", DateTime.Now.ToString());
                da.ExecuteQuery(iQuery);
            }
            return Nextid;

        }

        
        public DataBase ExecuteQuery(Query qBuilder)
        {
            if (this.Type == DataSourceType.MYSQL) {
                MYSQLQueryBuilder ms = (MYSQLQueryBuilder)qBuilder;
                if (!ms.ValidateSchema())
                {
                    throw new Exception("Query validation failed");
                }
                if (ms.Type == QueryType._Delete || ms.Type == QueryType._Insert || ms.Type == QueryType._Update)
                {
                    if (da.ExecuteQuery(ms) > 0)
                    {
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                    };
                }
                else
                {
                    GetResult = da.GetData(ms);
                }
            }           
            
            return this;
        }
        public DataBase ExecuteTQuery(TQuery tQuery) {
            //  tQuery.AddField
            if (this.Type == DataSourceType.MYSQL)
            {
                MYSQLTQueryBuilder ms = (MYSQLTQueryBuilder) tQuery;

                if (tQuery.ValidateSchema())
                {
                    if (da.ExecuteTQuery(ms))
                    {
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                    }
                }

            }
               
            return this;
        }
    }

   



    

}
