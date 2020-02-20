using System;
using System.Collections.Generic;

namespace Pixel.Core.Workflow
{
    public interface ILoggingMethods
    {
        Boolean LogWorkflowToDatabase(int workflowId, DateTime workflowExecutionTime, string actionName, string executionId, DateTime actionExecutionTime, string status, string statusValue, string error = null);
        Boolean LogWorkflowToDatabase(int workflowId, DateTime workflowExecutionTime, string actionName, string executionId, DateTime actionExecutionTime, string status, string statusValue, object error = null);
        Boolean LogWorkflowToDatabase(int workflowId, DateTime workflowExecutionTime, string actionName, string executionId, DateTime actionExecutionTime, string status, string statusValue, List<object> error = null);



        List<object> GetWorkflowLogsById(int workflowId);
        object GetDetailedWorkflowLogsById(int workflowId);
    }
}
