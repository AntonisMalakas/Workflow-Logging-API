using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Core.Workflow
{
    public interface IWorkflowDBService
    {
        string SaveWorkflowAsync(string collectionName, object workflowPayload);
        bool UpdateWorkflowAsync(string collectionName, object workflowPayload);

        object GetWorkflowById(string collectionName, int workflowId);
        bool DeleteWorkflowById(string collectionName, int workflowId);
        List<object> GetAllWorkflows(string collectionName);
        List<object> GetAllWorkflowsComplete(string collectionName);

        List<object> GetAllWorkflowsToExecute(string collectionName);

    }
}
