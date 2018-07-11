using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
namespace TechSharpy.Entitifier.Data
{
    class DataSource : DataAccess
    {
        DataTable dtResult;
        Query iQuery;
        public DataSource()
        {
            try
            {
                this.Init();
                dtResult = new DataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDataSource(int DataSourceID) {
            iQuery = new Query(QueryType._Select).AddField("*", "s_DataSource")
             .AddWhere(0, "s_DataSource", "DataSourceID", FieldType._Text, Operator._Equal, DataSourceID.ToString(), Condition._None);
            dtResult = this.GetData(iQuery);
            return dtResult;
        }

        public int Save(string sourceKey,string name) {
            int nextid = getNextID("DataSource");
            iQuery = new Query(QueryType._Insert)
                .AddField("DataSourceID", "s_DataSource",  FieldType._Number, "", nextid.ToString())
                .AddField("sourceKey","s_DataSource",  FieldType._String, "", sourceKey)
                .AddField("name", "s_DataSource",  FieldType._String, "", name)                
                .AddField("LastUPD", "s_DataSource",  FieldType._DateTime, "", DateTime.Now.ToString());
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return nextid;
            }
            else
            {
                return -1;
            }
        }

        public bool Save(int DataSourceID,string Name) {
            iQuery = new Query(QueryType._Update)
              .AddField("DataSourceName", "s_DataSource", FieldType._String, "", Name.ToString())
              .AddWhere(0, "s_DataSource", "DataSourceID", FieldType._Number, Operator._Equal, DataSourceID.ToString(), Condition._None);
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int DataSourceID)
        {
            iQuery = new Query(QueryType._Delete)
              .AddField("*", "s_DataSource", FieldType._String)
              .AddWhere(0, "s_DataSource", "DataSourceID", FieldType._Number, Operator._Equal, DataSourceID.ToString(), Condition._None);
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
