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
  public  class BusinessAttributeComponent

    {
        private DataTable Result;
        DataBase rd;
        Query iQuery;

        public BusinessAttributeComponent()
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
          string componentName, string componentDescription, string titlepattern,int parentComponentID,int RelatedAttributeID)
        {
            // int nextid = rd.getNextID("BusinessComponent");
            iQuery = new QueryBuilder(QueryType._Insert)
                //.AddField("entityKey", "c_businessattribute", FieldType._Number, "", entityKey.ToString())

                .AddField("ComponentID", "c_businessattribute", FieldType._Number, "", componentID.ToString())
                .AddField("titlepattern", "c_businessattribute", FieldType._String, "", (titlepattern).ToString())
                .AddField("componentName", "c_businessattribute", FieldType._String, "", componentName)
                .AddField("componentDescription", "c_businessattribute", FieldType._String, "", componentDescription)
                .AddField("ParentComponentID", "c_businessattribute", FieldType._Number, "", parentComponentID.ToString())
                .AddField("RelatedAttributeID", "c_businessattribute", FieldType._Number, "", RelatedAttributeID.ToString())
                .AddField("LastUPD", "c_businessattribute", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public bool Update(int componentID, int entityKey, string category, ComponentType
            componentType, string componentName,
            string componentDescription, string titlepattern,int parentComponentID,int RelatedAttributeID)
        {
            iQuery = new QueryBuilder(QueryType._Update)
                //  .AddField("entityKey", "c_businessattribute", FieldType._Number, "", entityKey.ToString())
                //  .AddField("ComponentID", "c_businessattribute", FieldType._Number, "", nextid.ToString())
                .AddField("titlepattern", "c_businessattribute", FieldType._String, "", (titlepattern).ToString())
                .AddField("componentName", "c_businessattribute", FieldType._String, "", componentName)
                .AddField("componentDescription", "c_businessattribute", FieldType._String, "", componentDescription)
                  .AddField("ParentComponentID", "c_businessattribute", FieldType._Number, "", parentComponentID.ToString())
                .AddField("RelatedAttributeID", "c_businessattribute", FieldType._Number, "", RelatedAttributeID.ToString())
                .AddField("LastUPD", "c_businessattribute", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "c_businessattribute", "ComponentID", FieldType._Number, Operator._Equal, componentID.ToString(), Condition._None);
            //   .AddWhere(0, "c_businessattribute", "entityKey", FieldType._Number, entityKey.ToString());

            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public bool Delete(int componentID, int entityKey)
        {
            iQuery = new QueryBuilder(QueryType._Delete)
                .AddField("*", "c_businessattribute")
                .AddWhere(0, "c_businessattribute", "ComponentID", FieldType._Number, componentID.ToString(), Condition._None);
            //  .AddWhere(0, "c_businessattribute", "entityKey", FieldType._Number, entityKey.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public DataTable GetComponentByID(int ComponentID)
        {
            iQuery = new QueryBuilder(QueryType._Select)
                // .AddField("entityKey", "c_businessattribute", FieldType._Number)
                .AddField("ComponentID", "c_businessattribute", FieldType._Number, "")
                  .AddField("ParentComponentID", "c_businessattribute", FieldType._Number)
                .AddField("RelatedAttributeID", "c_businessattribute", FieldType._Number)
                // .AddField("componentType", "c_businessattribute", FieldType._Number, "")
                .AddField("componentName", "c_businessattribute", FieldType._String, "")
                .AddField("componentDescription", "c_businessattribute", FieldType._String, "")
                .AddField("TitlePattern", "c_businessattribute", FieldType._String, "")
                .AddField("LastUPD", "c_businessattribute", FieldType._DateTime, "")
            //   .                AddWhere(0, "c_businessattribute", "ComponentID", FieldType._Number, entityKey.ToString(),Condition._And)
            .AddWhere(0, "c_businessattribute", "ComponentID", FieldType._Number, ComponentID.ToString(), Condition._None);
            Result = rd.ExecuteQuery(iQuery).GetResult;
            return Result;
        }
    }
}
