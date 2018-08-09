using System;
using System.Data;
using TechSharpy.Data;
using TechSharpy.Data.ABS;
using TechSharpy.Workflow.Core;

namespace TechSharpy.Workflow.Data
{
    public class Action
    {

        private DataTable Result;
        DataBase rd;
        Query iQuery;
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
        public bool Save(int stepID, int workflowID,string name,ActionType type,string successMsg,
            string failureMsg,int NextStep,string criterials) {

             int nextid = rd.getNextID("Action");
            iQuery = new QueryBuilder(QueryType._Insert)
                //.AddField("entityKey", "wf_step_action", FieldType._Number, "", entityKey.ToString())
                .AddField("ActionID", "wf_step_action", FieldType._Number, "", nextid.ToString())
                .AddField("StepID", "wf_step_action", FieldType._Number, "", (stepID).ToString())
                .AddField("WorkflowID", "wf_step_action", FieldType._Number, "", workflowID.ToString())
                .AddField("Name", "wf_step_action", FieldType._String, "", name)
                .AddField("Type", "wf_step_action", FieldType._Number, "", ((int)type).ToString())
                .AddField("Success", "wf_step_action", FieldType._String, "", successMsg.ToString())
                .AddField("failure", "wf_step_action", FieldType._String, "", failureMsg.ToString())
                .AddField("NextStep", "wf_step_action", FieldType._Number, "", NextStep.ToString())
                .AddField("criterials", "wf_step_action", FieldType._String, "", criterials.ToString())
                .AddField("LastUPD", "wf_step_action", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;                     
        }
        public bool Save(int actionID,int stepID, int workflowID,string name, ActionType type, string successMsg,
            string failureMsg, int NextStep, string criterials) {


            iQuery = new QueryBuilder(QueryType._Update)
                .AddField("Name", "wf_step_action", FieldType._String, "", name)
                .AddField("Type", "wf_step_action", FieldType._Number, "", ((int)type).ToString())
                .AddField("Success", "wf_step_action", FieldType._String, "", successMsg.ToString())
                .AddField("failure", "wf_step_action", FieldType._String, "", failureMsg.ToString())
                .AddField("NextStep", "wf_step_action", FieldType._Number, "", NextStep.ToString())
                .AddField("criterials", "wf_step_action", FieldType._String, "", criterials.ToString())
                .AddField("LastUPD", "wf_step_action", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "wf_step_action", "actionID", FieldType._Number, actionID.ToString(), Condition._And)
            .AddWhere(0, "wf_step_action", "stepid", FieldType._Number, stepID.ToString(), Condition._And)
            .AddWhere(0, "wf_step_action", "workflowid", FieldType._Number, workflowID.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;           
        }
            public bool Remove(int actionID) {
                iQuery = new QueryBuilder(QueryType._Delete)
                   .AddField("*", "wf_step_action")
                   .AddWhere(0, "wf_step_action", "actionID", FieldType._Number, actionID.ToString(), Condition._None);            
                if (rd.ExecuteQuery(iQuery).Result)
                {
                    return true;
                }
                else return false;                        
            }
        public DataTable GetAction(int actionID, int stepID,int workflowID) {
            iQuery = new QueryBuilder(QueryType._Select)                
                .AddField("ActionID", "wf_step_action")
                .AddField("StepID", "wf_step_action")
                .AddField("WorkflowID", "wf_step_action")
                .AddField("Name", "wf_step_action")
                .AddField("Type", "wf_step_action")
                .AddField("Success", "wf_step_action")
                .AddField("failure", "wf_step_action")
                .AddField("NextStep", "wf_step_action")
                .AddField("criterials", "wf_step_action")
                .AddField("LastUPD", "wf_step_action")
                  .AddWhere(0, "wf_step_action", "actionID", FieldType._Number, actionID.ToString(), Condition._And)
            .AddWhere(0, "wf_step_action", "stepid", FieldType._Number, stepID.ToString(), Condition._And)
            .AddWhere(0, "wf_step_action", "workflowid", FieldType._Number, workflowID.ToString());
            return rd.ExecuteQuery(iQuery).GetResult;
        }

       
    }
}
