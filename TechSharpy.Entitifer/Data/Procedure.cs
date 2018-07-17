using System;
using System.Data;
using TechSharpy.Data;
using TechSharpy.Data.ABS;

namespace TechSharpy.Entitifier.Data
{
    public class Procedure

    {
        DataTable dtResult;
        TechSharpy.Data.DataBase rd;
        public Procedure()
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

        public DataTable GetProcedure(int procedureID) {
            dtResult = new DataTable();
            Query iQuery = new MYSQLQueryBuilder(QueryType._Select)
                .AddField("ProcedureID", "s_entity_procedure")
                .AddField("Name", "s_entity_procedure")
                .AddField("Steps", "s_entity_procedure")
            .AddField("datasourcekey", "s_entity_procedure");
            dtResult = rd.ExecuteQuery(iQuery).GetResult;
            return dtResult;
        }

        public int Save(string Name, string steps,string datasourcekey)
        {
            int NextID = rd.getNextID("procedure");
            Query iQuery = new MYSQLQueryBuilder(QueryType._Insert
                ).AddTable("s_entity_procedure")
                .AddField("ProcedureID", "s_entity_procedure", FieldType._Number, "", NextID.ToString())
                .AddField("Name", "s_entity_procedure", FieldType._String, "", Name.ToString())
                .AddField("datasourcekey", "s_entity_procedure", FieldType._String, "", datasourcekey.ToString())
                .AddField("Steps", "s_entity_procedure", FieldType._String, "", steps.ToString())
                .AddField("LastUPD", "s_entity_procedure", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
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

            Query iQuery = new MYSQLQueryBuilder(QueryType._Delete
                ).AddTable("s_entity_procedure")
               .AddField("*", "s_entity_procedure", FieldType._String)
                .AddWhere(0, "s_entity_procedure", "datasourcekey", FieldType._String, Operator._Equal, datasourcekey.ToString(), Condition._And)
                .AddWhere(0, "s_entity_procedure", "ProcedureID", FieldType._Number, Operator._Equal, procedureID.ToString(), Condition._None);

            if (rd.ExecuteQuery(iQuery).Result)
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

            Query iQuery = new MYSQLQueryBuilder(QueryType._Update
                ).AddTable("s_entity_procedure")
               .AddField("Name", "s_entity_procedure", FieldType._String, "", Name.ToString())                
                .AddField("Steps", "s_entity_procedure", FieldType._String, "", steps.ToString())
                .AddField("LastUPD", "s_entity_procedure", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "s_entity_procedure", "datasourcekey", FieldType._String, Operator._Equal, datasourcekey.ToString(), Condition._And)
                .AddWhere(0, "s_entity_procedure", "ProcedureID", FieldType._Number, Operator._Equal, procedureID.ToString(), Condition._None);

            if (rd.ExecuteQuery(iQuery).Result)
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
