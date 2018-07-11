using System;
using System.Data;
using TechSharpy.Data;
namespace TechSharpy.Entitifier.Data
{
    public class Procedure : DataAccess

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

        public DataTable GetProcedure(int procedureID) {
            dtResult = new DataTable();
            Query iQuery = new Query(QueryType._Select)
                .AddField("ProcedureID", "s_entity_procedure")
                .AddField("Name", "s_entity_procedure")
                .AddField("Steps", "s_entity_procedure")
            .AddField("datasourcekey", "s_entity_procedure");
            dtResult = this.GetData(iQuery);
            return dtResult;
        }

        public int Save(string Name, string steps,string datasourcekey)
        {
            int NextID = this.getNextID("procedure");
            Query iQuery = new Query(QueryType._Insert
                ).AddTable("s_entity_procedure")
                .AddField("ProcedureID", "s_entity_procedure", FieldType._Number, "", NextID.ToString())
                .AddField("Name", "s_entity_procedure", FieldType._String, "", Name.ToString())
                .AddField("datasourcekey", "s_entity_procedure", FieldType._String, "", datasourcekey.ToString())
                .AddField("Steps", "s_entity_procedure", FieldType._String, "", steps.ToString())
                .AddField("LastUPD", "s_entity_procedure", FieldType._DateTime, "", DateTime.Now.ToString());
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return NextID;
            }
            else
            {
                return -1;
            }
        }
        public bool Delete(int procedureID,string datasourcekey)
        {

            Query iQuery = new Query(QueryType._Delete
                ).AddTable("s_entity_procedure")
               .AddField("*", "s_entity_procedure", FieldType._String)
                .AddWhere(0, "s_entity_procedure", "datasourcekey", FieldType._String, Operator._Equal, datasourcekey.ToString(), Condition._And)
                .AddWhere(0, "s_entity_procedure", "ProcedureID", FieldType._Number, Operator._Equal, procedureID.ToString(), Condition._None);

            if (this.ExecuteQuery(iQuery) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool Save(int procedureID, string Name,  string steps,string datasourcekey)
        {

            Query iQuery = new Query(QueryType._Update
                ).AddTable("s_entity_procedure")
               .AddField("Name", "s_entity_procedure", FieldType._String, "", Name.ToString())                
                .AddField("Steps", "s_entity_procedure", FieldType._String, "", steps.ToString())
                .AddField("LastUPD", "s_entity_procedure", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "s_entity_procedure", "datasourcekey", FieldType._String, Operator._Equal, datasourcekey.ToString(), Condition._And)
                .AddWhere(0, "s_entity_procedure", "ProcedureID", FieldType._Number, Operator._Equal, procedureID.ToString(), Condition._None);

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
