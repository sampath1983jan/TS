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
    public class BusinessLink
    {
        private DataTable Result;
        DataBase rd;
        Query iQuery;

        public BusinessLink()
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

        public bool Save(int componentID, string category, ComponentType componentType,
          string componentName, string componentDescription, string titlepattern, 
          int parentComponentID, int RelatedAttributeID, int linkComponentID, int linkAttributeID)
        {
            // int nextid = rd.getNextID("BusinessComponent");
            iQuery = new QueryBuilder(QueryType._Insert)
                //.AddField("entityKey", "c_businesslink", FieldType._Number, "", entityKey.ToString())

                .AddField("ComponentID", "c_businesslink", FieldType._Number, "", componentID.ToString())
                .AddField("titlepattern", "c_businesslink", FieldType._String, "", (titlepattern).ToString())
                .AddField("componentName", "c_businesslink", FieldType._String, "", componentName)
                .AddField("componentDescription", "c_businesslink", FieldType._String, "", componentDescription)
                .AddField("ParentComponentID", "c_businesslink", FieldType._Number, "", parentComponentID.ToString())
                .AddField("RelatedAttributeID", "c_businesslink", FieldType._Number, "", RelatedAttributeID.ToString())
                .AddField("LinkComponentID", "c_businesslink", FieldType._Number, "", linkComponentID.ToString())
                .AddField("linkAttributeID", "c_businesslink", FieldType._Number, "", linkAttributeID.ToString())

                .AddField("LastUPD", "c_businesslink", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public bool Update(int componentID, int entityKey, string category, ComponentType
            componentType, string componentName,
            string componentDescription, string titlepattern, int parentComponentID,
            int RelatedAttributeID, int linkComponentID, int linkAttributeID)
        {
            iQuery = new QueryBuilder(QueryType._Update)
                //  .AddField("entityKey", "c_businesslink", FieldType._Number, "", entityKey.ToString())
                //  .AddField("ComponentID", "c_businesslink", FieldType._Number, "", nextid.ToString())
                .AddField("titlepattern", "c_businesslink", FieldType._String, "", (titlepattern).ToString())
                .AddField("componentName", "c_businesslink", FieldType._String, "", componentName)
                .AddField("componentDescription", "c_businesslink", FieldType._String, "", componentDescription)
                  .AddField("ParentComponentID", "c_businesslink", FieldType._Number, "", parentComponentID.ToString())
                .AddField("RelatedAttributeID", "c_businesslink", FieldType._Number, "", RelatedAttributeID.ToString())

                .AddField("LinkComponentID", "c_businesslink", FieldType._Number, "", linkComponentID.ToString())
                .AddField("linkAttributeID", "c_businesslink", FieldType._Number, "", linkAttributeID.ToString())
                
                .AddField("LastUPD", "c_businesslink", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "c_businesslink", "ComponentID", FieldType._Number, Operator._Equal, componentID.ToString(), Condition._None);
            //   .AddWhere(0, "c_businesslink", "entityKey", FieldType._Number, entityKey.ToString());

            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public bool Delete(int componentID, int entityKey)
        {
            iQuery = new QueryBuilder(QueryType._Delete)
                .AddField("*", "c_businesslink")
                .AddWhere(0, "c_businesslink", "ComponentID", FieldType._Number, componentID.ToString(), Condition._None);
            //  .AddWhere(0, "c_businesslink", "entityKey", FieldType._Number, entityKey.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public DataTable GetComponentByID(int ComponentID)
        {
            iQuery = new QueryBuilder(QueryType._Select)
                // .AddField("entityKey", "c_businesslink", FieldType._Number)
                .AddField("ComponentID", "c_businesslink", FieldType._Number, "")
                  .AddField("ParentComponentID", "c_businesslink", FieldType._Number)
                .AddField("RelatedAttributeID", "c_businesslink", FieldType._Number)
                    .AddField("LinkComponentID", "c_businesslink", FieldType._Number)
                .AddField("linkAttributeID", "c_businesslink", FieldType._Number)
                // .AddField("componentType", "c_businesslink", FieldType._Number, "")
                .AddField("componentName", "c_businesslink", FieldType._String, "")
                .AddField("componentDescription", "c_businesslink", FieldType._String, "")
                .AddField("TitlePattern", "c_businesslink", FieldType._String, "")
                .AddField("LastUPD", "c_businesslink", FieldType._DateTime, "")
            //   .                AddWhere(0, "c_businesslink", "ComponentID", FieldType._Number, entityKey.ToString(),Condition._And)
            .AddWhere(0, "c_businesslink", "ComponentID", FieldType._Number, ComponentID.ToString(), Condition._None);
            Result = rd.ExecuteQuery(iQuery).GetResult;
            return Result;
        }
    }
}
