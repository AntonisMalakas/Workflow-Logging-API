using System;
using System.Collections.Generic;

namespace Pixel.Core.Workflow
{
    public interface ILogDbService
    {
        Boolean LogWorkflowByAction(object payload);
        List<Object> GetWorkflowLogById(int workflowId);
        object GetDetailedWorkflowLogsById(int workflowId);
    }
}
