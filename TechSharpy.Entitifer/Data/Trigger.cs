using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;

namespace TechSharpy.Entitifier.Data
{
   public class Trigger : DataAccess
    {
        DataTable dtResult;
        public Trigger()
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

        public int Save(string Name, int entityKey, TechSharpy.Entitifier.Core.ActionType type, TechSharpy.Entitifier.Core.EventType eventType,string steps) 
        {
            int NextID = this.getNextID("Trigger");
            Query iQuery = new Query(QueryType._Insert
                ).AddTable("s_entity_trigger")
                .AddField("TriggerID", "s_entity_trigger", FieldType._Number, "", NextID.ToString())
                .AddField("Name", "s_entity_trigger", FieldType._String, "", Name.ToString())
                .AddField("entityKey", "s_entity_trigger", FieldType._String, "", entityKey.ToString())
                .AddField("Actiontype", "s_entity_trigger", FieldType._String, "", type.ToString())
                .AddField("eventType", "s_entity_trigger", FieldType._String, "", eventType.ToString())                              
                .AddField("LastUPD", "s_entity_trigger", FieldType._DateTime, "", DateTime.Now.ToString());
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return NextID;
            }
            else
            {
                return -1;
            }
        }
        public bool Save(int triggerID,string Name, int entityKey, TechSharpy.Entitifier.Core.ActionType type, TechSharpy.Entitifier.Core.EventType eventType, string steps)
        {

            Query iQuery = new Query(QueryType._Update
                ).AddTable("s_entity_trigger")
                .AddField("Name", "s_entity_trigger", FieldType._String, "", Name.ToString())
                .AddField("entityKey", "s_entity_trigger", FieldType._Number, "", entityKey.ToString())
                .AddField("Actiontype", "s_entity_trigger", FieldType._Number, "", type.ToString())
                .AddField("eventType", "s_entity_trigger", FieldType._Number, "", eventType.ToString())
                .AddField("steps", "s_entity_trigger", FieldType._String, "", steps.ToString())
                .AddField("LastUPD", "s_entity_trigger", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "s_entity_trigger", "TriggerID", FieldType._Number, Operator._Equal, triggerID.ToString(), Condition._None);
            
            if (this.ExecuteQuery(iQuery) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Boolean Delete(int TriggerID)
        {
            Query DeleteQ = new Query(QueryType._Delete).AddTable("s_entity_trigger").
               AddWhere(0, "s_entity_trigger", "TriggerID", FieldType._Number, Operator._Equal, TriggerID.ToString());
            int iResult;
            iResult = this.ExecuteQuery(DeleteQ);
            if (iResult >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable getTriggers( int entityKey)
        {
            dtResult = new DataTable();
            Query selectQ = new Query(QueryType._Select).AddTable("s_entity_trigger").AddField("*", "s_entity_trigger").           
                AddWhere(0, "s_entity_trigger", "EntityID", FieldType._Number, Operator._Equal, entityKey.ToString());
            dtResult = this.GetData(selectQ);
            return dtResult;
        }

    }
}
