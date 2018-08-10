using System;
using System.Data;
using TechSharpy.Data;
using TechSharpy.Data.ABS;
using TechSharpy.Workflow.Core;

namespace TechSharpy.Workflow.Data
{
   public class RequestAction
   {
        private DataTable Result;
        DataBase rd;
        Query iQuery;
        public RequestAction()
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

        public bool Save(int requestID,int wfID,int stepID,int actionID,int nextStepID,string comments,
            int commentedby,string status ,string attachments) {                        
            iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("requestID", "wf_request_flow", FieldType._Number, "", requestID.ToString())
                .AddField("ActionID", "wf_request_flow", FieldType._Number, "", actionID.ToString())
                .AddField("StepID", "wf_request_flow", FieldType._Number, "", (stepID).ToString())
                .AddField("WorkflowID", "wf_request_flow", FieldType._Number, "", wfID.ToString())
                .AddField("nextStepID", "wf_request_flow", FieldType._Number, "", nextStepID.ToString())
                .AddField("comments", "wf_request_flow", FieldType._String, "", (comments).ToString())
                .AddField("CommentedBy", "wf_request_flow", FieldType._String, "", commentedby.ToString())
                .AddField("Status", "wf_request_flow", FieldType._String, "", status.ToString())
                .AddField("attachment", "wf_request_flow", FieldType._Text, "", attachments.ToString())
                .AddField("CommentsOn", "wf_request_flow", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddField("LastUPD", "wf_request_flow", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

        public bool Delete(int requestID, int wfID,int stepID,int actionID) {

            iQuery = new QueryBuilder(QueryType._Delete)
             .AddField("*", "wf_request_flow")
             .AddWhere(0, "wf_request_flow", "requestID", FieldType._Number, requestID.ToString(), Condition._And)
            .AddWhere(0, "wf_request_flow", "workflowid", FieldType._Number, wfID.ToString(), Condition._And)
            .AddWhere(0, "wf_request_flow", "stepid", FieldType._Number, stepID.ToString(), Condition._And)
             .AddWhere(0, "wf_request_flow", "actionID", FieldType._Number, actionID.ToString(), Condition._None);
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
    }
}
