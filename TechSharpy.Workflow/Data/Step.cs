using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Data;
using TechSharpy.Data.ABS;
using TechSharpy.Workflow.Core;

namespace TechSharpy.Workflow.Data
{
    public class Step
    {

        private DataTable Result;
        DataBase rd;
        Query iQuery;
        public Step()
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

        public DataTable GetStep(int workflowID, int stepID)
        {
            iQuery = new QueryBuilder(QueryType._Select)
                 .AddField("StepID", "wf_step")
                 .AddField("WorkflowID", "wf_step")
                 .AddField("Name", "wf_step")
                 .AddField("Description", "wf_step")
                 .AddField("Type", "wf_step")
                 .AddField("IsMandatory", "wf_step")
                 .AddField("IsAutoApprove", "wf_step")
                 .AddField("DaysRequiredtoAuto", "wf_step")
                 .AddField("IsEditable", "wf_step")
                 .AddField("StepOrder", "wf_step")
                 .AddWhere(0, "wf_step", "StepID", FieldType._Number, stepID.ToString(), Condition._And)
                 .AddWhere(0, "wf_step", "WorkflowID", FieldType._Number, workflowID.ToString());
            return rd.ExecuteQuery(iQuery).GetResult;
        }

        public int Save(int wfID,string name,string description,StepType type,bool isMandatory,bool isAutoApprove,
            bool isEditable,int stepOrder, int DaysRequiredtoAuto) {
            int nextid =rd.getNextID("Step");
            iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("StepID", "wf_step",FieldType._Number,"",nextid.ToString())
                .AddField("WorkflowID", "wf_step", FieldType._Number, "", wfID.ToString())
                .AddField("Name", "wf_step", FieldType._String, "", name.ToString())
                .AddField("Description", "wf_step", FieldType._String, "", description.ToString())
                .AddField("Type", "wf_step", FieldType._Number, "", ((int)type).ToString())
                .AddField("IsMandatory", "wf_step", FieldType._Question, "", isMandatory.ToString())
                .AddField("IsAutoApprove", "wf_step", FieldType._Question, "", isAutoApprove.ToString())
                .AddField("DaysRequiredtoAuto", "wf_step", FieldType._Number, "", DaysRequiredtoAuto.ToString())
                .AddField("IsEditable", "wf_step", FieldType._Question, "", isEditable.ToString())
                .AddField("StepOrder", "wf_step", FieldType._Number, "", stepOrder.ToString())
                .AddField("lastUPD", "wf_step", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result ) {
                return nextid;
            }else             return -1;
        }

        public bool Save(int stepID,int wfID, string name, string description, StepType type, bool isMandatory, bool isAutoApprove,
            bool isEditable, int stepOrder, int DaysRequiredtoAuto)
        {
            
            iQuery = new QueryBuilder(QueryType._Update)
                //.AddField("StepID", "wf_step", FieldType._Number, "", nextid.ToString())
                //.AddField("WorkflowID", "wf_step", FieldType._Number, "", wfID.ToString())
                .AddField("Name", "wf_step", FieldType._String, "", name.ToString())
                .AddField("Description", "wf_step", FieldType._String, "", description.ToString())
                .AddField("Type", "wf_step", FieldType._Number, "", ((int)type).ToString())
                .AddField("IsMandatory", "wf_step", FieldType._Question, "", isMandatory.ToString())
                .AddField("IsAutoApprove", "wf_step", FieldType._Question, "", isAutoApprove.ToString())
                .AddField("DaysRequiredtoAuto", "wf_step", FieldType._Number, "", DaysRequiredtoAuto.ToString())
                .AddField("IsEditable", "wf_step", FieldType._Question, "", isEditable.ToString())
                .AddField("StepOrder", "wf_step", FieldType._Number, "", stepOrder.ToString())
                .AddField("lastUPD", "wf_step", FieldType._DateTime, "", DateTime.Now.ToString())
                .AddWhere(0, "wf_step", "StepID", FieldType._Number, stepID.ToString(), Condition._And)
                 .AddWhere(0, "wf_step", "WorkflowID", FieldType._Number, wfID.ToString()); 
            return rd.ExecuteQuery(iQuery).Result;
        }

        public bool RemoveStepActions(int stepid)
        {
            iQuery = new QueryBuilder(QueryType._Delete)
               .AddField("*", "wf_step_action")
               .AddWhere(0, "wf_step_action", "stepid", FieldType._Number, stepid.ToString(), Condition._None);
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

        public bool Remove(int stepID)
        {
            iQuery = new QueryBuilder(QueryType._Delete)
               .AddField("*", "wf_step")
               .AddWhere(0, "wf_step", "stepID", FieldType._Number, stepID.ToString(), Condition._None);
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

    }
    
}
