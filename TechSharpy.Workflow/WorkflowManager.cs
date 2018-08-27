using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Workflow.Core
{

    public abstract class IWorkflowManagerFactory
    {
        public abstract IWorkflow Create(int iD);
        public abstract IWorkflow Create(string name, string category, string description, int createdby, int formid);
    }

    public class WorkflowManagerFactory : IWorkflowManagerFactory
    {
        public override IWorkflow Create(int workflowid)
        {
            return new Workflow(workflowid);
        }

        public override IWorkflow Create(string name, string category, string description, int createdby,
            int formid)
        {
            return new Workflow(name, category, description, createdby, formid);
        }
    }


    public class WorkflowManager
    {
        public static IWorkflow Create(WorkflowManagerFactory factory, int formID)
        {
            return factory.Create(formID);

        }
        public static IWorkflow Create(WorkflowManagerFactory factory, string name, string category, string description, int createdby,
            int formid)
        {
            return factory.Create(name, category, description, createdby, formid);
        }
    }
}
