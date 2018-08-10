﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Workflow.Core
{
  public class Workflow
    {
        public int ID;
        public string Name;
        public string Category;
        public string Description;
        public DateTime CreatedOn;
        public int Createdby;
        public List<Step> Steps;
        private Data.Workflow dataworkflow;
        public Workflow(string name, string category, string description, int createdby)
        {
            Name = name;
            Category = category;
            Description = description;
            Createdby = createdby;
            Steps = new List<Step>();
            dataworkflow = new Data.Workflow();
        }
        public Workflow() {
            Data.Workflow dataworkflow = new Data.Workflow();
            Steps = new List<Step>();
        }
        public Workflow(int iD)
        {
            ID = iD;
            Steps = new List<Step>();
            dataworkflow = new Data.Workflow();
        }

        public void Init() {
            DataTable dt = new DataTable();
            dt = dataworkflow.GetWorkflow(this.ID);
            foreach (DataRow dr in dt.Rows) {
                ID = dr.IsNull("WorkflowID") == true ? -1 : (int)dr["WorkflowID"];
                Name = dr.IsNull("Name") == true ? "" : (string)dr["Name"];
                Description = dr.IsNull("Description") == true ? "" : (string)dr["Description"];
                Category = dr.IsNull("Category") == true ? "" : (string)dr["Category"];
                Createdby = dr.IsNull("Createdby") == true ? -1 : (int)dr["Createdby"];
                CreatedOn = dr.IsNull("CreatedOn") == true ? DateTime.MinValue : (DateTime)dr["CreatedOn"];
            }
            LoadSteps();
            LoadStepActions();
        }

        internal bool Save()
        {
            if (this.ID > 0)
            {
                return dataworkflow.Save(this.ID, this.Name, this.Description, this.Category, this.Createdby);
            }
            else
            {
                this.ID = dataworkflow.Save(this.Name, this.Description, this.Category, this.Createdby);
                if (ID > 0)
                {
                    return true;
                }
                else return false;
            }
        }

        protected internal bool Remove()
        {
            if (dataworkflow.Remove(this.ID)) {
                foreach (Step s in this.Steps) {
                    s.Remove();                    
                }
                return true;
            }else return false;
        }
        private void LoadSteps() {
            DataTable dt = new DataTable();
           dt= dataworkflow.GetStepsByWorkflow(this.ID);
            Steps = dt.AsEnumerable().Select(dr => new Step {
            WorkflowID = dr.IsNull("WorkflowID") == true ? -1 : (int)dr["WorkflowID"],
            Name = dr.IsNull("Name") == true ? "" : (string)dr["Name"],
            Description = dr.IsNull("Description") == true ? "" : (string)dr["Description"],
            Type = dr.IsNull("Type") == true ? StepType._initator : (StepType)dr["Type"],
            IsMandatory = dr.IsNull("IsMandatory") == true ? false : (bool)dr["IsMandatory"],
            IsAutoApprove = dr.IsNull("IsAutoApprove") == true ? false : (bool)dr["IsAutoApprove"],
            DaysRequiredtoAuto = dr.IsNull("DaysRequiredtoAuto") == true ? -1 : (int)dr["DaysRequiredtoAuto"],
            IsEditable = dr.IsNull("IsEditable") == true ? false : (bool)dr["IsEditable"],
            StepOrder = dr.IsNull("StepOrder") == true ? -1 : (int)dr["StepOrder"],
        }).ToList();         
        }
        private void LoadStepActions() {
            DataTable dt = new DataTable();
            dt = dataworkflow.GetActionsbyWorkflow(this.ID);
            foreach (Step s in Steps) {
                DataTable _dtAction = new DataTable();
                dt.DefaultView.RowFilter = "StepID = " + s.StepID;
                _dtAction = dt.DefaultView.ToTable(true);
                dt.DefaultView.RowFilter = "";
                var _actions  =_dtAction.AsEnumerable().Select(dr => new Action
                {
                    Name = dr.IsNull("Name") == true ? "" : dr["Name"].ToString(),
                    Type = dr.IsNull("Type") == true ? ActionType._draft : (ActionType)dr["Type"],
                    SuccessMessage = dr.IsNull("Success") == true ? "" : dr["Success"].ToString(),
                    failureMessage = dr.IsNull("failure") == true ? "" : dr["failure"].ToString(),
                    NextStep = dr.IsNull("NextStep") == true ? 0 : (int)dr["NextStep"],
                    //    cr = dr.IsNull("criterials") == true ? "" : dr["criterials"].ToString();
                });
                foreach (Action a in _actions) {
                    s.AddAction(a);
                }                
            }          
        }

    }
}