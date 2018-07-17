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
    public class Action 
    {

       
        TechSharpy.Data.DataBase rd;
        public Action()
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

        public int SaveAction(int pEntitykey, int pActionType, bool pIsInclude, string pActionName, string pActionSchema)
        {

            int NextID = rd.getNextID("Action");
            Query iQuery = new MYSQLQueryBuilder(QueryType._Insert
                ).AddTable("s_entity_action")
                .AddField("ActionID", "s_entity_action", FieldType._Number, "", NextID.ToString())
                .AddField("ActionName", "s_entity_action", FieldType._String, "", pActionName.ToString())
                .AddField("ActionType", "s_entity_action", FieldType._String, "", pActionType.ToString())
                .AddField("ActionSchema", "s_entity_action", FieldType._String, "", pActionSchema.ToString())
                .AddField("entityKey", "s_entity_action", FieldType._String, "", pEntitykey.ToString())
                .AddField("ActionDate", "s_entity_action", FieldType._String, "", DateTime.Now.ToString())
                .AddField("IsInclude", "s_entity_action", FieldType._Question, "", pIsInclude.ToString())
                 .AddField("lastUPD", "s_entity_action", FieldType._Question, "", DateTime.Now.ToString());
                if (rd.ExecuteQuery(iQuery).Result)
                {
                    return NextID;
                }
                else
                {
                    return -1;
                }


             
        }
    }

 }

