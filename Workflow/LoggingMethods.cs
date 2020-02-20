using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Pixel.Core.Workflow
{
    public class LoggingMethods : ILoggingMethods
    {
        private ILogDbService _logDbService;
        public LoggingMethods(ILogDbService logDbService)
        {
            this._logDbService = logDbService;
        }

        public bool LogWorkflowToDatabase(int workflowId, DateTime workflowExecutionTime, string actionName, string executionId, DateTime actionExecutionTime, string status, string statusValue, string error)
        {
            dynamic objToLog = new ExpandoObject();
            objToLog.workflowId = workflowId.ToString();
            objToLog.workflowExecutionTime = workflowExecutionTime;
            objToLog.actionName = actionName;
            objToLog.executionId = executionId;
            objToLog.actionExecutionTime = actionExecutionTime;
            objToLog.status = status;
            objToLog.statusValue = statusValue;
            objToLog.error = error;

            var response = this._logDbService.LogWorkflowByAction(objToLog);
            return response;
        }

        public bool LogWorkflowToDatabase(int workflowId, DateTime workflowExecutionTime, string actionName, string executionId, DateTime actionExecutionTime, string status, string statusValue, object error = null)
        {
            dynamic objToLog = new ExpandoObject();
            objToLog.workflowId = workflowId.ToString();
            objToLog.workflowExecutionTime = workflowExecutionTime;
            objToLog.actionName = actionName;
            objToLog.executionId = executionId;
            objToLog.actionExecutionTime = actionExecutionTime;
            objToLog.status = status;
            objToLog.statusValue = statusValue;
            objToLog.error = error;

            var response = this._logDbService.LogWorkflowByAction(objToLog);
            return response;
        }

        public bool LogWorkflowToDatabase(int workflowId, DateTime workflowExecutionTime, string actionName, string executionId, DateTime actionExecutionTime, string status, string statusValue, List<object> error = null)
        {
            dynamic objToLog = new ExpandoObject();
            objToLog.workflowId = workflowId.ToString();
            objToLog.workflowExecutionTime = workflowExecutionTime;
            objToLog.actionName = actionName;
            objToLog.executionId = executionId;
            objToLog.actionExecutionTime = actionExecutionTime;
            objToLog.status = status;
            objToLog.statusValue = statusValue;
            objToLog.error = error;

            var response = this._logDbService.LogWorkflowByAction(objToLog);
            return response; ;
        }

        public List<object> GetWorkflowLogsById(int workflowId)
        {
            var response = this._logDbService.GetWorkflowLogById(workflowId);
            return response;
        }

        public object GetDetailedWorkflowLogsById(int workflowId)
        {
            var response = this._logDbService.GetDetailedWorkflowLogsById(workflowId);
            return response;
        }
    }
}
