using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Workflow.Business
{
   public class RequestComment
    {
        public int RequestID;
        public int WorkflowID;
        public int StepID;
        public int NextStepID;
        public int ActionID;
        public int CommentedBy;
        public string Comments;
        public string Status;
        public DateTime CommentOn;
        public List<string> Attachments;
        private Data.RequestAction dataRequestAction;
        public RequestComment(int stepID, int actionID, int commentedBy, string comments)
        {
            StepID = stepID;
            NextStepID = -1;
            ActionID = actionID;
            CommentedBy = commentedBy;
            Comments = comments;
            CommentOn = DateTime.Now;
            dataRequestAction = new Data.RequestAction();
        }

        internal bool Save() {
          return dataRequestAction.Save(RequestID, this.WorkflowID, this.StepID, this.ActionID, this.NextStepID,
                this.Comments, this.CommentedBy,
                this.Status, String.Join(", ", this.Attachments.ToArray()));
        }

        internal bool Delete() {
            return dataRequestAction.Delete(this.RequestID, this.WorkflowID, this.StepID, this.ActionID);
        }
    }
}
