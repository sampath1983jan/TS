using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Workflow.Business
{

    public abstract class IWorkflowRequestManagerFactory
    {
        public abstract IWorkflowRequest CreateInstance(int requestID);
        public abstract IWorkflowRequest CreateRequest(int wfid);
    }

    public class WorkflowManagerFactory : IWorkflowRequestManagerFactory
    {
        public override IWorkflowRequest CreateInstance(int requestID)
        {
            return new WorkflowRequest(requestID);
        }

        public override IWorkflowRequest CreateRequest(int wfid)
        {
            return new WorkflowRequest().NewRequest(wfid);
        }
    }

    public  class WorkflowRequestManager
    {
        public static IWorkflowRequest Create(WorkflowManagerFactory factory, int requestID)
        {
            return factory.CreateInstance(requestID);

        }
        public static IWorkflowRequest CreateNewRequest(WorkflowManagerFactory factory, int wfid)
        {
            return factory.CreateRequest(wfid);
        }
    }
}
