using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
using TechSharpy.Data.ABS;

namespace TechSharpy.Component.Data
{
   public class Component
    {
        private DataTable Result;
        DataBase rd;
        Query iQuery;

        public Component()
        {
            try
            {
                rd = new DataBase();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int Save(int entityKey, ComponentType componentType)
        {
            int nextid = rd.getNextID("Component");
            iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("entityKey", "c_Component", FieldType._Number, "", entityKey.ToString())
                .AddField("ComponentID", "c_Component", FieldType._Number, "", nextid.ToString())
                .AddField("componentType", "c_Component", FieldType._Number, "", ((int)componentType).ToString())              
                .AddField("LastUPD", "c_Component", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return nextid;
            }
            else return -1;
        }
              
        public bool Delete(int componentID, int entityKey)
        {
            iQuery = new QueryBuilder(QueryType._Delete)
                .AddField("*", "c_Component")
                .AddWhere(0, "c_Component", "ComponentID", FieldType._Number, componentID.ToString());
               //.AddWhere(0, "c_Component", "entityKey", FieldType._Number, entityKey.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }


        public DataTable GetComponentByID( int ComponentID)
        {
            iQuery = new QueryBuilder(QueryType._Select)              
                .AddField("ComponentID", "c_Component", FieldType._Number, "")               
                .AddField("componentType", "c_Component", FieldType._String, "")
                .AddField("entityKey", "c_Component", FieldType._String, "")
                .AddField("LastUPD", "c_Component", FieldType._DateTime, "")
            .AddWhere(0, "c_Component", "ComponentID", FieldType._Number, ComponentID.ToString());
            Result = rd.ExecuteQuery(iQuery).GetResult;
            return Result;
        }

    }
}
