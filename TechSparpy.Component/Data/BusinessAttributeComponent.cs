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
                //.AddField("entityKey", "wf_step_action", FieldType._Number, "", entityKey.ToString())

                .AddField("ComponentID", "wf_step_action", FieldType._Number, "", componentID.ToString())
                .AddField("titlepattern", "wf_step_action", FieldType._String, "", (titlepattern).ToString())
                .AddField("componentName", "wf_step_action", FieldType._String, "", componentName)
                .AddField("componentDescription", "wf_step_action", FieldType._String, "", componentDescription)
                .AddField("ParentComponentID", "wf_step_action", FieldType._Number, "", parentComponentID.ToString())
                .AddField("RelatedAttributeID", "wf_step_action", FieldType._Number, "", RelatedAttributeID.ToString())
                .AddField("LastUPD", "wf_step_action", FieldType._DateTime, "", DateTime.Now.ToString());
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
                //  .AddField("entityKey", "wf_step_action", FieldType._Number, "", entityKey.ToString())
                //  .AddField("ComponentID", "wf_step_action", FieldType._Number, "", nextid.ToString())
                .AddField("titlepattern", "wf_step_action", FieldType._String, "", (titlepattern).ToString())
                .AddField("componentName", "wf_step_action", FieldType._String, "", componentName)
                .AddField("componentDescription", "wf_step_action", FieldType._String, "", componentDescription)
                  .AddField("ParentComponentID", "wf_step_action", FieldType._Number, "", parentComponentID.ToString())
                .AddField("RelatedAttributeID", "wf_step_action", FieldType._Number, "", RelatedAttributeID.ToString())
                .AddField("LastUPD", "wf_step_action", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "wf_step_action", "ComponentID", FieldType._Number, Operator._Equal, componentID.ToString(), Condition._None);
            //   .AddWhere(0, "wf_step_action", "entityKey", FieldType._Number, entityKey.ToString());

            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public bool Delete(int componentID, int entityKey)
        {
            iQuery = new QueryBuilder(QueryType._Delete)
                .AddField("*", "wf_step_action")
                .AddWhere(0, "wf_step_action", "ComponentID", FieldType._Number, componentID.ToString(), Condition._None);
            //  .AddWhere(0, "wf_step_action", "entityKey", FieldType._Number, entityKey.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public DataTable GetComponentByID(int ComponentID)
        {
            iQuery = new QueryBuilder(QueryType._Select)
                // .AddField("entityKey", "wf_step_action", FieldType._Number)
                .AddField("ComponentID", "wf_step_action", FieldType._Number, "")
                  .AddField("ParentComponentID", "wf_step_action", FieldType._Number)
                .AddField("RelatedAttributeID", "wf_step_action", FieldType._Number)
                // .AddField("componentType", "wf_step_action", FieldType._Number, "")
                .AddField("componentName", "wf_step_action", FieldType._String, "")
                .AddField("componentDescription", "wf_step_action", FieldType._String, "")
                .AddField("TitlePattern", "wf_step_action", FieldType._String, "")
                .AddField("LastUPD", "wf_step_action", FieldType._DateTime, "")
            //   .                AddWhere(0, "wf_step_action", "ComponentID", FieldType._Number, entityKey.ToString(),Condition._And)
            .AddWhere(0, "wf_step_action", "ComponentID", FieldType._Number, ComponentID.ToString(), Condition._None);
            Result = rd.ExecuteQuery(iQuery).GetResult;
            return Result;
        }
    }
}
