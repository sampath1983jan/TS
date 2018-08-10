using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Workflow.Core
{
    public enum ActionType {
        _draft=0, // before submit condition. submitter can modify before submit
        _submit=1, // once user submit workflow they cannot modify
        _approve=2, // approver do approve action and move to next step
        _reject=3, // reject the current step. suspend the request
        _assign=6, // assign some other user before process or close the request
        _retract=4, // before approve or reject approve or processer move to previous step
        _close=5, // close the workflow and update required information
        _custom=7,
    }
    /// <summary>
    /// Eighter action move to next step or check step criterial more based on the criterial success
    /// </summary>
    public class Action
    {
        private List<Stepcriterial> _stepcriterials;
        public int ActionID;
        public int StepID;
        public int WorkFlowID;
        public string Name;
        public ActionType Type;
        public string SuccessMessage;
        public string failureMessage;
        public int NextStep;
        public List<Stepcriterial> Stepcriterials { get =>_stepcriterials; } // optional param

        private Data.Action dataAction;

        public Action(int actionID, int stepID, int workFlowID)
        {
            StepID = stepID;
            WorkFlowID = workFlowID;
            ActionID = actionID;
            dataAction = new Data.Action();
            _stepcriterials = new List<Stepcriterial>();
        }

        public Action()
        {
            StepID = -1;
            WorkFlowID = -1;
            ActionID = -1;
            dataAction = new Data.Action();
            _stepcriterials = new List<Stepcriterial>();
        }

        public Action(int stepID, int workFlowID, string name, ActionType type, int nextStep) :
            this(-1, stepID, workFlowID)
        {
            Name = name;
            Type = type;
            NextStep = nextStep;
        }

        public void AddCriteria(Stepcriterial stepcriterial) {
            this._stepcriterials.Add(stepcriterial);
        }
        public void Init() {
            DataTable dt = new DataTable();
            dt = dataAction.GetAction(this.ActionID, this.StepID, this.WorkFlowID);
            foreach (DataRow dr in dt.Rows) {
                string cr = "";
                this.Name = dr.IsNull("Name") == true ? "" : dr["Name"].ToString();
                this.Type = dr.IsNull("Type") == true ?  ActionType._draft : (ActionType)dr["Type"];
                this.SuccessMessage = dr.IsNull("Success") == true ? "" : dr["Success"].ToString();
                this.failureMessage = dr.IsNull("failure") == true ? "" : dr["failure"].ToString();
                this.NextStep = dr.IsNull("NextStep") == true ? 0 : (int) dr["NextStep"];
                cr = dr.IsNull("criterials") == true ? "" : dr["criterials"].ToString();
            }
        }
        public bool Save() {
            if (this.ActionID > 0)
            {
                return dataAction.Save(this.ActionID,this.StepID, this.WorkFlowID, this.Name, this.Type, this.SuccessMessage, this.failureMessage, this.NextStep, this.getCriteria().ToString());
            }
            else {
                return dataAction.Save(this.StepID, this.WorkFlowID, this.Name, this.Type, this.SuccessMessage, this.failureMessage, this.NextStep, this.getCriteria().ToString());
            }          
        }
        public bool Delete() {
         return   dataAction.Remove(this.ActionID);            
        }
        private string getCriteria() {
            string sb ="" ;
            foreach (Stepcriterial sc in Stepcriterials) {
                sb = sb + ("," + sc.CRID);
            }
            if (sb.StartsWith(",")) {
                sb = sb.Substring(1);
            }
            return sb;
        }
        protected internal bool Duplicate() {
            this.ActionID = -1;
            return this.Save();            
        }
        public string GetStatusAsText() {
            if (this.Type == ActionType._draft)
            {
                return "Saved";
            }
            else if (this.Type == ActionType._submit)
            {
                return "Submitted";
            }
            else if (this.Type == ActionType._approve)
            {
                return "Approved";
            }
            else if (this.Type == ActionType._reject)
            {
                return "Rejected";
            }
            else if (this.Type == ActionType._assign)
            {
                return "Assigned to review";
            }
            else if (this.Type == ActionType._close)
            {
                return "Closed";
            }
            else if (this.Type == ActionType._retract)
            {
                return "retracted";
            }
            else return "";
        }
        public int GetNextStep() {
            return NextStep;
        }
    }
}
