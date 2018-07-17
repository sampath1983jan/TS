using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
using TechSharpy.Data.ABS;

namespace TechSharpy.Entitifier.Data
{
   public class Function  
    {
        DataTable dtResult;
        TechSharpy.Data.DataBase rd;
        public Function()
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
        public int Save(string Name,  string inputParam,string outputParam, string steps)
        {
            int NextID = rd.getNextID("Function");
            Query iQuery = new MYSQLQueryBuilder(QueryType._Insert
                ).AddTable("s_entity_function")
                .AddField("FunctionID", "s_entity_function", FieldType._Number, "", NextID.ToString())
                .AddField("Name", "s_entity_function", FieldType._String, "", Name.ToString())
                .AddField("Inputparam", "s_entity_function", FieldType._String, "", inputParam.ToString())
                .AddField("Outputparam", "s_entity_function", FieldType._String, "", outputParam.ToString())
                .AddField("Steps", "s_entity_function", FieldType._String, "", steps.ToString())
                .AddField("LastUPD", "s_entity_function", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
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

            Query iQuery = new MYSQLQueryBuilder(QueryType._Update
                ).AddTable("s_entity_function")
               .AddField("Name", "s_entity_function", FieldType._String, "", Name.ToString())
                .AddField("Inputparam", "s_entity_function", FieldType._String, "", inputParam.ToString())
                .AddField("Outputparam", "s_entity_function", FieldType._String, "", outputParam.ToString())
                .AddField("Steps", "s_entity_function", FieldType._String, "", steps.ToString())
                .AddField("LastUPD", "s_entity_function", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "s_entity_function", "functionID", FieldType._Number, Operator._Equal, functionID.ToString(), Condition._None);

            if ((rd.ExecuteQuery(iQuery).Result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Boolean Delete(int functionID)
        {
            Query DeleteQ = new MYSQLQueryBuilder(QueryType._Delete).AddTable("s_entity_function").
               AddWhere(0, "s_entity_function", "functionID", FieldType._Number, Operator._Equal, functionID.ToString());
            
            if ((rd.ExecuteQuery(DeleteQ).Result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable getFunction() {
            Query iQuery = new MYSQLQueryBuilder(QueryType._Select
                ).AddTable("s_entity_function")
                .AddField("FunctionID", "s_entity_function", FieldType._String, "", "")
               .AddField("Name", "s_entity_function", FieldType._String, "", "")
                .AddField("Inputparam", "s_entity_function", FieldType._String, "", "")
                .AddField("Outputparam", "s_entity_function", FieldType._String, "", "")
                .AddField("Steps", "s_entity_function", FieldType._String, "", "")
                .AddField("LastUPD", "s_entity_function", FieldType._DateTime, "", "");
           dtResult= rd.ExecuteQuery(iQuery).GetResult;
            return dtResult;
        }

    }
}
