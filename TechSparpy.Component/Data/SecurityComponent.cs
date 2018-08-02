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
    public class SecurityComponent
    {
        private DataTable Result;
        DataBase rd;
        Query iQuery;

        public SecurityComponent()
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
          string componentName, string componentDescription, string titlepattern, int parentComponentID, int RelatedAttributeID,
          int userComponentID, int userKey)
        {
            
            iQuery = new QueryBuilder(QueryType._Insert)                
                .AddField("ComponentID", "c_Security", FieldType._Number, "", componentID.ToString())
                .AddField("titlepattern", "c_Security", FieldType._String, "", (titlepattern).ToString())
                .AddField("componentName", "c_Security", FieldType._String, "", componentName)
                .AddField("componentDescription", "c_Security", FieldType._String, "", componentDescription)
                .AddField("ParentComponentID", "c_Security", FieldType._Number, "", parentComponentID.ToString())
                .AddField("RelatedAttributeID", "c_Security", FieldType._Number, "", RelatedAttributeID.ToString())
                .AddField("UserComponentID", "c_Security", FieldType._Number, "", userComponentID.ToString())
                .AddField("UserKey", "c_Security", FieldType._Number, "", componentDescription.ToString())
                .AddField("LastUPD", "c_Security", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public bool Update(int componentID, int entityKey, string category, ComponentType componentType, string componentName,
            string componentDescription, string titlepattern, int parentComponentID, int RelatedAttributeID,int userComponentID, int userKey)
        {
            iQuery = new QueryBuilder(QueryType._Update)            
                .AddField("titlepattern", "c_Security", FieldType._String, "", (titlepattern).ToString())
                .AddField("componentName", "c_Security", FieldType._String, "", componentName)
                .AddField("componentDescription", "c_Security", FieldType._String, "", componentDescription)
                .AddField("ParentComponentID", "c_Security", FieldType._Number, "", parentComponentID.ToString())
                .AddField("RelatedAttributeID", "c_Security", FieldType._Number, "", RelatedAttributeID.ToString())
                .AddField("UserComponentID", "c_Security", FieldType._Number, "", userComponentID.ToString())
                .AddField("UserKey", "c_Security", FieldType._Number, "", componentDescription.ToString())
                .AddField("LastUPD", "c_Security", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "c_Security", "ComponentID", FieldType._Number, Operator._Equal, componentID.ToString(), Condition._None);            

            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public bool Delete(int componentID, int entityKey)
        {
            iQuery = new QueryBuilder(QueryType._Delete)
                .AddField("*", "c_Security")
                .AddWhere(0, "c_Security", "ComponentID", FieldType._Number, componentID.ToString(), Condition._None);            
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public DataTable GetComponentByID(int ComponentID)
        {
            iQuery = new QueryBuilder(QueryType._Select)                
                .AddField("ComponentID", "c_Security", FieldType._Number, "")
                .AddField("ParentComponentID", "c_Security", FieldType._Number)
                .AddField("RelatedAttributeID", "c_Security", FieldType._Number)                
                .AddField("componentName", "c_Security", FieldType._String, "")
                .AddField("componentDescription", "c_Security", FieldType._String, "")
                .AddField("TitlePattern", "c_Security", FieldType._String, "")
                .AddField("UserComponentID", "c_Security", FieldType._Number)
                .AddField("UserKey", "c_Security", FieldType._Number)
                .AddField("LastUPD", "c_Security", FieldType._DateTime, "")            
            .AddWhere(0, "c_Security", "ComponentID", FieldType._Number, ComponentID.ToString(), Condition._None);
            Result = rd.ExecuteQuery(iQuery).GetResult;
            return Result;
        }

    }
}
