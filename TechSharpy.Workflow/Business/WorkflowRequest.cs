using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Workflow.Business
{
    public enum RequestStatus {
        _draft=0,
        _open=1,
        _close=2,
        _abort=3
    }
   public class WorkflowRequest:Workflow.Core.Workflow
    {
        public int RequestID;
        public string RequestNo;
       // public string Name;
        public int RequestedBy;
        public int RequestedFor;
        public int LastActionBy;
        private int LastAction;
     //   public int WorkflowID;
        public int CurrentStep;
        public RequestStatus Status;
        public string ActionSatus;
        public DateTime RequestedOn;
        public string RecentComment;
        private Core.Action UserAction;
        public List<RequestComment> RequestTracks;
        private Data.WorkflowRequest dataworkflowRequest;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestID"></param>
        public WorkflowRequest(int requestID):base ()
        {
            RequestID = requestID;
            dataworkflowRequest = new Data.WorkflowRequest();
            UserAction = new Core.Action();
            RequestTracks = new List<RequestComment>();
            base.Init();
            LoadComments();
        }
        /// <summary>
        /// 
        /// </summary>
        public WorkflowRequest()
        {
            Name = "";
            RequestNo = "";           
            RequestedBy = -1;
            RequestedFor = -1;
            RecentComment = "";
            dataworkflowRequest = new Data.WorkflowRequest();
            UserAction = new Core.Action();
            RequestTracks = new List<RequestComment>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workflowID"></param>
        public void NewRequest(int workflowID)  {
            this.ID = workflowID;
            this.Status = RequestStatus._open;
            base.Init();             
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadComments() {
            DataTable dt = new DataTable();
           dt= dataworkflowRequest.GetRequestFlow(this.RequestID);
            foreach (DataRow dr in dt.Rows) {
                int _step,actionid,commentedby = 0;
                string comments = "";
                actionid = dr.IsNull("ActionID") == true ? -1 : (int)dr["ActionID"];
                commentedby = dr.IsNull("CommentedBy") == true ? -1 : (int)dr["CommentedBy"];
                _step = dr.IsNull("StepID") == true ? -1 : (int)dr["stepID"];
                comments = dr.IsNull("comments") == true ? "" : (string)dr["comments"];

                RequestComment rc = new RequestComment(_step, actionid, commentedby, comments);
                rc.RequestID = this.RequestID;
                rc.WorkflowID = this.ID;
                rc.CommentOn = dr.IsNull("CommentsOn") == true ? DateTime.MinValue : (DateTime)dr["CommentsOn"];                
                rc.NextStepID= dr.IsNull("NextStepID") == true ? -1 : (int)dr["NextStepID"];
                rc.Status = dr.IsNull("Status") == true ? "" : (string)dr["Status"];
                rc.Attachments = dr.IsNull("attachment") == true ? new List<string> (): dr["attachment"].ToString().Split(',').ToList();
                this.RequestTracks.Add(rc);
            }           
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetRequestNo()
        {
            DataTable dt = new DataTable();
            dt = dataworkflowRequest.GetRequestNo(this.ID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int _reqNo = 0;
                    _reqNo = ((int)dr["RequestNo"] + 1);
                    if (dataworkflowRequest.UpdateRequestNo(this.ID, _reqNo)) this.RequestNo = _reqNo.ToString();
                    else this.RequestNo = "-1";                   
                }
            }
            else this.RequestNo = "-1";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepID"></param>
        /// <param name="actionID"></param>
        /// <returns></returns>
        private Core.Action GetAction(int stepID, int actionID) {
            return this.Steps.Where(a => a.StepID == stepID).FirstOrDefault().Actions.Where(b => b.ActionID == actionID).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedBy"></param>
        /// <param name="requestFor"></param>
        /// <param name="requestAction"></param>
        /// <returns></returns>
        public bool RaiseRequest(int requestedBy,int requestFor, RequestComment requestAction) {
            Core.Action _action = new Core.Action();
            _action = GetAction(requestAction.StepID, requestAction.ActionID);

            this.CurrentStep = _action.GetNextStep(); 
            this.RequestedBy = requestedBy;
            this.RequestedFor = requestFor;
            this.LastAction = requestAction.ActionID;
            this.LastActionBy = requestAction.CommentedBy;
            this.RecentComment = requestAction.Comments;
            this.ActionSatus = _action.GetStatusAsText();
            requestAction.Status = _action.GetStatusAsText();
            
            if (this.RequestID > 0)
            {
                SetRequestNo();
                if (this.RequestNo == "-1") return false;                
                this.RequestID = dataworkflowRequest.Save(this.RequestNo, Name, RequestedBy, RequestedFor, ID, CurrentStep, (int)Status,
                     ActionSatus, LastActionBy, LastAction);
                if (this.RequestID > 0)
                {
                    return true;
                }
                else return false;                 
            }
            else {
                return false;
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool RemoveRequest()
        {
            dataworkflowRequest.Remove(this.RequestID);
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestAction"></param>
        private void SetCurrentAction(RequestComment requestAction) {
            UserAction = GetAction(requestAction.StepID, requestAction.ActionID);
            this.LastAction = requestAction.ActionID;
            this.LastActionBy = requestAction.CommentedBy;
            this.RecentComment = requestAction.Comments;
            this.CurrentStep = UserAction.GetNextStep();            
            this.ActionSatus = UserAction.GetStatusAsText();
            requestAction.Status = UserAction.GetStatusAsText();
            requestAction.NextStepID = this.CurrentStep;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestAction"></param>
        /// <returns></returns>
        public bool MoveToNextStep(RequestComment requestAction) {
            SetCurrentAction(requestAction);                              
            if (dataworkflowRequest.ChangeStep(this.RequestID, this.CurrentStep, (int)this.Status, this.ActionSatus, this.LastActionBy, this.LastAction)){
                requestAction.Save();
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestAction"></param>
        /// <returns></returns>
        public bool Retract(RequestComment requestAction) {
            RequestComment rc= this.RequestTracks.Where(a => a.NextStepID == this.CurrentStep && a.WorkflowID == this.ID).FirstOrDefault();
            if (dataworkflowRequest.ChangeStep(this.RequestID, rc.StepID, (int)this.Status, "Retracted", this.LastActionBy, this.LastAction))
            {
                if (rc.Delete())
                {
                    return true;
                }
                else return false;                
            }
            else return false;
        }
    }
}
