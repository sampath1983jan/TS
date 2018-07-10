using System;
using System.Data;
using TechSharpy.Data;
namespace TechSharpy.Entitifier.Data
{
    public class Procedure:DataAccess

    {
        DataTable dtResult;
        public Procedure()
        {
            try
            {
                this.Init();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save(string Name, string inputParam, string outputParam, string steps)
        {
            int NextID = this.getNextID("Function");
            Query iQuery = new Query(QueryType._Insert
                ).AddTable("s_entity_function")
                .AddField("FunctionID", "s_entity_function", FieldType._Number, "", NextID.ToString())
                .AddField("Name", "s_entity_function", FieldType._String, "", Name.ToString())
                .AddField("Inputparam", "s_entity_function", FieldType._String, "", inputParam.ToString())
                .AddField("Outputparam", "s_entity_function", FieldType._String, "", outputParam.ToString())
                .AddField("Steps", "s_entity_function", FieldType._String, "", steps.ToString())
                .AddField("LastUPD", "s_entity_function", FieldType._DateTime, "", DateTime.Now.ToString());
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return NextID;
            }
            else
            {
                return -1;
            }
        }

        public bool Save(int functionID, string Name, string inputParam, string outputParam, string steps)
        {

            Query iQuery = new Query(QueryType._Update
                ).AddTable("s_entity_function")
               .AddField("Name", "s_entity_function", FieldType._String, "", Name.ToString())
                .AddField("Inputparam", "s_entity_function", FieldType._String, "", inputParam.ToString())
                .AddField("Outputparam", "s_entity_function", FieldType._String, "", outputParam.ToString())
                .AddField("Steps", "s_entity_function", FieldType._String, "", steps.ToString())
                .AddField("LastUPD", "s_entity_function", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "s_entity_function", "functionID", FieldType._Number, Operator._Equal, functionID.ToString(), Condition._None);

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
