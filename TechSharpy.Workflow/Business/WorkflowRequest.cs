using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Workflow.Business
{
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
        public int RequestStatus;
        public string ActionSatus;
        public DateTime RequestedOn;

        public WorkflowRequest(int requestID):base ()
        {
            RequestID = requestID;
            base.Init();
        }

        public WorkflowRequest()
        {
            Name = "";
            RequestNo = "";           
            RequestedBy = -1;
            RequestedFor = -1;            
        }

        public void CreateRequest(int workflowID)  {
            this.ID = workflowID;
            base.Init();            
        }

        public bool SaveRequest() {
            return true;
        }

        public bool RemoveRequest()
        {
            return true;
        }
        public bool MoveToNextStep(int actionID,string comments) {
            return true;
        }
    }
}
