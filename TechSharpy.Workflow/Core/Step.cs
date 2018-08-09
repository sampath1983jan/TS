using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Workflow.Core
{
    public enum StepType {
        _initator=1,
        _approver=2,
        _processer=3
    }
    public class Step
    {
        private List<Action> _actions;
        public int StepID;
        public int WorkflowID;
        public string Name;
        public string Description;
        public StepType Type;
        public bool IsMandatory;
        public bool IsAutoApprove;
        public int DaysRequiredtoAuto;
        public bool IsEditable;
        public List<Action> Actions { get => _actions; }
        public int StepOrder;
        private Data.Step dataStep;
        public Step(int workflowID, string name, string description, StepType type, bool isMandatory,
            bool isAutoApprove, int daysRequiredtoAuto, bool isEditable, List<Action> actions, int stepOrder)
        {
            WorkflowID = workflowID;
            Name = name;
            Description = description;
            Type = type;
            IsMandatory = isMandatory;
            IsAutoApprove = isAutoApprove;
            DaysRequiredtoAuto = daysRequiredtoAuto;
            IsEditable = isEditable;
            _actions = actions;
            StepOrder = stepOrder;
            dataStep = new Data.Step();
        }
        public Step() {
            this.StepID = -1;
            this.WorkflowID = -1;
            Name = "";
            Description = "";
            Type = StepType._initator;
            _actions = new List<Action>();
            dataStep = new Data.Step();
        }
        public Step(int stepID, int workflowID)
        {
            this.StepID = stepID;
            WorkflowID = workflowID;
            _actions = new List<Action>();
            dataStep = new Data.Step();
        }

        public void Init() {
            DataTable dt = new DataTable();
            dataStep.GetStep(this.WorkflowID, this.StepID);
            foreach (DataRow dr in dt.Rows) {
                WorkflowID = dr.IsNull("WorkflowID") == true ? -1 : (int)dr["WorkflowID"];
                Name = dr.IsNull("Name") == true ? "" : (string)dr["Name"];
                Description = dr.IsNull("Description") == true ? "" : (string)dr["Description"];
                Type = dr.IsNull("Type") == true ? StepType._initator : (StepType)dr["Type"];
                IsMandatory = dr.IsNull("IsMandatory") == true ? false : (bool)dr["IsMandatory"];
                IsAutoApprove = dr.IsNull("IsAutoApprove") == true ? false : (bool)dr["IsAutoApprove"];
                DaysRequiredtoAuto = dr.IsNull("DaysRequiredtoAuto") == true ? -1 : (int)dr["DaysRequiredtoAuto"];
                IsEditable = dr.IsNull("IsEditable") == true ? false : (bool)dr["IsEditable"];
                StepOrder = dr.IsNull("StepOrder") == true ? -1 : (int)dr["StepOrder"];
            }
        }

        public void AddAction(Action action) {
            this.Actions.Add(action);
        }

        public bool Save() {
            this.StepID= dataStep.Save(this.WorkflowID, this.Name, this.Description, this.Type, this.IsMandatory,
                this.IsAutoApprove,this.IsEditable, this.StepOrder, this.DaysRequiredtoAuto);
            if (this.StepID > 0) { return true; } else return false;
        }

        public bool Remove() {
            if (dataStep.Remove(this.StepID))
            {
                return dataStep.RemoveStepActions(this.StepID);
            }
            else return false;
        }   
        public bool RemoveAction(int actionid) {
            return this.Actions.Where(a => a.ActionID == actionid).FirstOrDefault().Delete();
        }
        public bool DuplicateAction(int actionID) {
            return this.Actions.Where(a => a.ActionID == actionID).FirstOrDefault().Duplicate();
        }
        public bool Duplicate() {
            this.StepID = -1;
            return Save();
        }       

    }
}
