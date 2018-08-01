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
    public class GlobalComponent
    {
        private DataTable Result;
        DataBase rd;
        Query iQuery;

        public GlobalComponent()
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

          public bool Save(int componentID,string category, ComponentType componentType, string componentName, string componentDescription)
        {
           // int nextid = rd.getNextID("BusinessComponent");
            iQuery = new QueryBuilder(QueryType._Insert)
                //.AddField("entityKey", "c_GlobalComponent", FieldType._Number, "", entityKey.ToString())
                .AddField("ComponentID", "c_GlobalComponent", FieldType._Number, "", componentID.ToString())
         //       .AddField("componentType", "c_GlobalComponent", FieldType._Number, "", ((int)componentType).ToString())
                .AddField("componentName", "c_GlobalComponent", FieldType._String, "", componentName)
                .AddField("componentDescription", "c_GlobalComponent", FieldType._String, "", componentDescription)
                .AddField("LastUPD", "c_GlobalComponent", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

        public bool Update(int componentID, int entityKey, string category, ComponentType componentType, string componentName,
            string componentDescription)
        {
            iQuery = new QueryBuilder(QueryType._Update)
                //  .AddField("entityKey", "c_GlobalComponent", FieldType._Number, "", entityKey.ToString())
                //  .AddField("ComponentID", "c_GlobalComponent", FieldType._Number, "", nextid.ToString())
             //   .AddField("componentType", "c_GlobalComponent", FieldType._Number, "", ((int)componentType).ToString())
                .AddField("componentName", "c_GlobalComponent", FieldType._String, "", componentName)
                .AddField("componentDescription", "c_GlobalComponent", FieldType._String, "", componentDescription)
                .AddField("LastUPD", "c_GlobalComponent", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "c_GlobalComponent", "ComponentID", FieldType._Number, Operator._Equal, componentID.ToString(), Condition._None);
             //   .AddWhere(0, "c_GlobalComponent", "entityKey", FieldType._Number, entityKey.ToString());

            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

        public bool Delete(int componentID, int entityKey)
        {
            iQuery = new QueryBuilder(QueryType._Delete)
                .AddField("*", "c_GlobalComponent")
                .AddWhere(0, "c_GlobalComponent", "ComponentID", FieldType._Number, componentID.ToString(), Condition._None);
              //  .AddWhere(0, "c_GlobalComponent", "entityKey", FieldType._Number, entityKey.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }


        public DataTable GetComponentByID(int ComponentID)
        {

            iQuery = new QueryBuilder(QueryType._Select)
                // .AddField("entityKey", "c_GlobalComponent", FieldType._Number)
                .AddField("ComponentID", "c_GlobalComponent", FieldType._Number, "")
                // .AddField("componentType", "c_GlobalComponent", FieldType._Number, "")
                .AddField("componentName", "c_GlobalComponent", FieldType._String, "")
                .AddField("componentDescription", "c_GlobalComponent", FieldType._String, "")
                .AddField("TitlePattern", "c_GlobalComponent", FieldType._String, "")
                .AddField("LastUPD", "c_GlobalComponent", FieldType._DateTime, "")
             //   .                AddWhere(0, "c_GlobalComponent", "ComponentID", FieldType._Number, entityKey.ToString(),Condition._And)
            .AddWhere(0, "c_GlobalComponent", "ComponentID", FieldType._Number, ComponentID.ToString(),Condition._None);            
            Result = rd.ExecuteQuery(iQuery).GetResult;
            return Result;
        }


    }
}
