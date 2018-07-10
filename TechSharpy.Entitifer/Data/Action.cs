using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
namespace TechSharpy.Entitifier.Data
{
    public class Action : DataAccess
    {

        DataTable dtResult;
        public Action()
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

        public int SaveAction(int pEntitykey, int pActionType, bool pIsInclude, string pActionName, string pActionSchema)
        {

            int NextID = this.getNextID("Action");
            

                Query iQuery = new Query(QueryType._Insert
                ).AddTable("s_entity_action")
                .AddField("ActionID", "s_entity_action", FieldType._Number, "", NextID.ToString())
                .AddField("ActionName", "s_entity_action", FieldType._String, "", pActionName.ToString())
                .AddField("ActionType", "s_entity_action", FieldType._String, "", pActionType.ToString())
                .AddField("ActionSchema", "s_entity_action", FieldType._String, "", pActionSchema.ToString())
                .AddField("entityKey", "s_entity_action", FieldType._String, "", pEntitykey.ToString())
                .AddField("ActionDate", "s_entity_action", FieldType._String, "", DateTime.Now.ToString())
                .AddField("IsInclude", "s_entity_action", FieldType._Question, "", pIsInclude.ToString())
                 .AddField("lastUPD", "s_entity_action", FieldType._Question, "", DateTime.Now.ToString());
                if (this.ExecuteQuery(iQuery) > 0)
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

