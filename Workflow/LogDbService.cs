using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Pixel.Core.Workflow
{
    public class LogDbService : ILogDbService
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _logDatabase;
        private IConfiguration _configuration;
        private string _collectionName;
        public LogDbService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDbConnection");
            _client = new MongoClient(connectionString);
            _logDatabase = _client.GetDatabase("workflowDb");
            this._collectionName = "workflowLogger";
        }


        public bool LogWorkflowByAction(object payload)
        {
            var jsonDoc = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var bsonDoc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(jsonDoc);

            var tagetCollection = _logDatabase.GetCollection<BsonDocument>(_collectionName);
            tagetCollection.InsertOne(bsonDoc);
            return true;
        }
        public List<object> GetWorkflowLogById(int workflowId)
        {
            var targetCollection = _logDatabase.GetCollection<BsonDocument>(_collectionName);
            //var filter = Builders<BsonDocument>.Filter.Regex("workflowId", new BsonRegularExpression(workflowId.ToString()));
            var filter = Builders<BsonDocument>.Filter.And(
                           Builders<BsonDocument>.Filter.Eq("workflowId", new BsonRegularExpression(workflowId.ToString())),
                           Builders<BsonDocument>.Filter.Eq("actionName", "ExecuteWorkflow"));

            var result = targetCollection.Find(filter)
                 .Project(Builders<BsonDocument>.Projection.Exclude("_id"))

                 .ToList()
                 .ToJson();
            var deserializedWorkflowsList = JsonConvert.DeserializeObject<List<object>>(result);
            List<object> workflowsList = new List<object>();

            foreach (dynamic workflow in deserializedWorkflowsList)
            {

                dynamic obj = new ExpandoObject();
                obj.workflowId = Convert.ToInt32(workflow.workflowId);
                obj.workflowExecutionTime = workflow.workflowExecutionTime;
                obj.actionName = workflow.actionName;
                obj.executionId = workflow.executionId;
                obj.actionExecutionTime = workflow.actionExecutionTime;
                obj.status = workflow.status;
                obj.statusValue = workflow.statusValue;
                obj.error = workflow.error;



                workflowsList.Add(obj);

            }
            return workflowsList;
        }


        public object GetDetailedWorkflowLogsById(int workflowId)
        {
            var targetCollection = _logDatabase.GetCollection<BsonDocument>(_collectionName);
            //var filter = Builders<BsonDocument>.Filter.Regex("workflowId", new BsonRegularExpression(workflowId.ToString()));
            var filter = Builders<BsonDocument>.Filter.And(
                           Builders<BsonDocument>.Filter.Eq("workflowId", new BsonRegularExpression(workflowId.ToString())),
                           Builders<BsonDocument>.Filter.Not(Builders<BsonDocument>.Filter.Eq("actionName", "ExecuteWorkflow")));

            var result = targetCollection.Find(filter)
                 .Project(Builders<BsonDocument>.Projection.Exclude("_id"))

                 .ToList()
                 .ToJson();
            var deserializedWorkflowsList = JsonConvert.DeserializeObject<List<object>>(result);
            List<dynamic> workflowsList = new List<dynamic>();

            foreach (dynamic workflow in deserializedWorkflowsList)
            {

                dynamic obj = new ExpandoObject();
                obj.workflowId = Convert.ToInt32(workflow.workflowId);
                obj.workflowExecutionTime = workflow.workflowExecutionTime;
                obj.actionName = workflow.actionName;
                obj.executionId = workflow.executionId;
                obj.actionExecutionTime = workflow.actionExecutionTime;
                obj.status = workflow.status;
                obj.statusValue = workflow.statusValue;
                obj.error = workflow.error;



                workflowsList.Add(obj);

            }

            var orderedListByExecutionId = workflowsList
                                            .GroupBy(x => new { x.executionId })
                                            .Select(g => g.ToList())
                                            .ToList();
            return orderedListByExecutionId;
        }
    }
}
