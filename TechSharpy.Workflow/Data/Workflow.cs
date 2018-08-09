using System;
using System.Data;
using TechSharpy.Data;
using TechSharpy.Data.ABS;

namespace TechSharpy.Workflow.Data
{
    public class Workflow
    {
        private DataTable Result;
        DataBase rd;
        Query iQuery;
        public Workflow()
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
        public int Save(string name, string description, string category, int createdby) {
            int nextid = rd.getNextID("wf");
            iQuery = new QueryBuilder(QueryType._Insert)
                .AddField("Workflowid", "wf", FieldType._Number, "", nextid.ToString())
                .AddField("name", "wf", FieldType._String, "", name.ToString())
                .AddField("description", "wf", FieldType._String, "", description.ToString())
                .AddField("category", "wf", FieldType._String, "", category.ToString())
                .AddField("createdby", "wf", FieldType._Number, "", createdby.ToString())
                .AddField("createdon", "wf", FieldType._DateTime, "", DateTime.Now.ToString())
            .AddField("lastUPD", "wf", FieldType._DateTime, "", DateTime.Now.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return nextid;
            }
            else return -1;
        }
        public bool Save(int wfid, string name, string description, string category, int createdby)
        {
           // int nextid = rd.getNextID("wf");
            iQuery = new QueryBuilder(QueryType._Insert)                
            .AddField("name", "wf", FieldType._String, "", name.ToString())
            .AddField("description", "wf", FieldType._String, "", description.ToString())
            .AddField("category", "wf", FieldType._String, "", category.ToString())
            .AddField("createdby", "wf", FieldType._Number, "", createdby.ToString())
            .AddField("createdon", "wf", FieldType._DateTime, "", DateTime.Now.ToString())
            .AddField("lastUPD", "wf", FieldType._DateTime, "", DateTime.Now.ToString())
            .AddWhere(0, "wf", "Workflowid", FieldType._Number, wfid.ToString());
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }
        public bool Remove(int wfid)
        {
            iQuery = new QueryBuilder(QueryType._Delete)
               .AddField("*", "wf")
               .AddWhere(0, "wf", "workflowid", FieldType._Number, wfid.ToString(), Condition._None);
            if (rd.ExecuteQuery(iQuery).Result)
            {
                return true;
            }
            else return false;
        }

        public DataTable GetWorkflow(int wfid)
        {
            iQuery = new QueryBuilder(QueryType._Select) 
                .AddField("WorkflowID", "wf")
                .AddField("name", "wf")
                .AddField("description", "wf")
                .AddField("category", "wf")
                .AddField("createdby", "wf")
                .AddField("createdon", "wf")    
                .AddField("LastUPD", "wf")                 
            .AddWhere(0, "wf", "workflowid", FieldType._Number, wfid.ToString());
            return rd.ExecuteQuery(iQuery).GetResult;
        }

        public DataTable GetActionsbyWorkflow(int workflowID)
        {
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
            .AddWhere(0, "wf_step_action", "workflowid", FieldType._Number, workflowID.ToString());
            return rd.ExecuteQuery(iQuery).GetResult;
        }

        public DataTable GetStepsByWorkflow(int workflowID)
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
                 .AddWhere(0, "wf_step", "WorkflowID", FieldType._Number, workflowID.ToString());
            return rd.ExecuteQuery(iQuery).GetResult;
        }
    }
}
