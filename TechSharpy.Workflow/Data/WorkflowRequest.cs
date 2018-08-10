using System;
using System.Data;
using TechSharpy.Data;
using TechSharpy.Data.ABS;

namespace TechSharpy.Workflow.Data
{
   public class WorkflowRequest
    {
        private DataTable Result;
        DataBase rd;
        Query iQuery;

        public WorkflowRequest()
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

        public int Save(string requestNo,string name,int requestedby,int requestedfor,int wfid,
            int currentStep,int requestStatus,string actionStatus,int lastActionby,int lastAction)
        {
            int nextid = rd.getNextID("wfrequest");
            iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("RequestID", "wf_request", FieldType._Number, "", nextid.ToString())
                .AddField("RequestNo", "wf_request", FieldType._String, "", requestNo.ToString())
                .AddField("RequestName", "wf_request", FieldType._String, "", name.ToString())
                .AddField("RequestedBy", "wf_request", FieldType._Number, "", requestedby.ToString())
                .AddField("RequestedFor", "wf_request", FieldType._Number, "", requestedfor.ToString())
                .AddField("wfID", "wf_request", FieldType._Number, "", wfid.ToString())
                .AddField("CurrentStep", "wf_request", FieldType._Number, "", currentStep.ToString())
                .AddField("RequestStatus", "wf_request", FieldType._Number, "", requestStatus.ToString())
                .AddField("ActionSatus", "wf_request", FieldType._String, "", actionStatus.ToString())
                .AddField("RequestedOn", "wf_request", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddField("LastActionBy", "wf_request", FieldType._Number, "", lastActionby.ToString())
                .AddField("LastAction", "wf_request", FieldType._Number, "", lastAction.ToString())               
                .AddField("lastUPD", "wf_request", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return nextid;
            }
            else return -1;
        }
        public bool ChangeStep(int requestID,int currentStep, int requestStatus, string actionStatus, int lastActionby,
            int lastAction)
        {
            // int nextid = rd.getNextID("wfrequest");
            iQuery = new QueryBuilder(QueryType._Update)
                .AddField("CurrentStep", "wf_request", FieldType._Number, "", currentStep.ToString())
               .AddField("RequestStatus", "wf_request", FieldType._Number, "", requestStatus.ToString())
                .AddField("ActionSatus", "wf_request", FieldType._String, "", actionStatus.ToString())
            //    .AddField("RequestedOn", "wf_request", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddField("LastActionBy", "wf_request", FieldType._Number, "", lastActionby.ToString())
                .AddField("LastAction", "wf_request", FieldType._Number, "", lastAction.ToString())
                .AddField("lastUPD", "wf_request", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "wf_request", "requestID", FieldType._Number, requestID.ToString());
                
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

        public bool CloseRequest(int requestID, int requestStatus, string actionStatus, int lastActionby,
            int lastAction)
        {
            // int nextid = rd.getNextID("wfrequest");
            iQuery = new QueryBuilder(QueryType._Update)
              //  .AddField("CurrentStep", "wf_request", FieldType._Number, "", currentStep.ToString())
               .AddField("RequestStatus", "wf_request", FieldType._Number, "", requestStatus.ToString())
                .AddField("ActionSatus", "wf_request", FieldType._String, "", actionStatus.ToString())
            //    .AddField("RequestedOn", "wf_request", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddField("LastActionBy", "wf_request", FieldType._Number, "", lastActionby.ToString())
                .AddField("LastAction", "wf_request", FieldType._Number, "", lastAction.ToString())
                .AddField("lastUPD", "wf_request", FieldType._DateTime, "", DateTime.Now.ToString())
               .AddWhere(0, "wf_request", "requestID", FieldType._Number, requestID.ToString());

            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

        public bool Remove(int requestID) {
            iQuery = new QueryBuilder(QueryType._Delete)
              .AddField("*", "wf_request")
              .AddWhere(0, "wf_request", "requestID", FieldType._Number, requestID.ToString(), Condition._None);
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

        public DataTable GetRequestNo(int wfID) {
            iQuery = new QueryBuilder(QueryType._Select)
             .AddField("*", "wf_RequestNo")
             .AddWhere(0, "wf_RequestNo", "wfID", FieldType._Number, wfID.ToString(), Condition._None);
            return rd.ExecuteQuery(iQuery).GetResult;
        }

        public bool UpdateRequestNo(int WorkflowID,int RequestNo)
        {
            // int nextid = rd.getNextID("wfrequest");
            iQuery = new QueryBuilder(QueryType._Update)
               //  .AddField("CurrentStep", "wf_request", FieldType._Number, "", currentStep.ToString())
               .AddField("RequestNo", "wf_RequestNo", FieldType._Number, "", RequestNo.ToString())               
                .AddField("lastUPD", "wf_RequestNo", FieldType._DateTime, "", DateTime.Now.ToString())
               .AddWhere(0, "wf_RequestNo", "wfID", FieldType._Number, WorkflowID.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

        public DataTable GetRequestFlow(int requestID) {
            iQuery = new QueryBuilder(QueryType._Select)
             .AddField("*", "wf_request_flow")
             .AddWhere(0, "wf_request_flow", "requestID", FieldType._Number, requestID.ToString(), Condition._None);
            return rd.ExecuteQuery(iQuery).GetResult;
        }

    }
}
