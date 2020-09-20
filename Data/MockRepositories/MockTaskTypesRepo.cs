using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.MockRepositories
{
    public class MockTaskTypesRepo : ITaskTypesRepo
    {
        private List<TaskType> taskTypes;
        public MockTaskTypesRepo() 
        {
            taskTypes = new List<TaskType>
            {
                new TaskType{TaskTypeId=1, ProjectId=1, Name="Logistics", Urgency="Medium", },
                new TaskType{TaskTypeId=2, ProjectId=1, Name="Design Related", Urgency="High"},
                new TaskType{TaskTypeId=3, ProjectId=1, Name="Bug", Urgency="Medium"},
                new TaskType{TaskTypeId=1, ProjectId=2, Name="Tax Issues", Urgency="Low"},
                new TaskType{TaskTypeId=2, ProjectId=2, Name="Logistics", Urgency="Medium"}
            };
        }

        public TaskType GetTaskTypeById(int projectId, int taskTypeId)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<TaskType> GetTaskTypesByProject(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TaskType> GetTaskTypesByUrgency(string urgency)
        {
            throw new System.NotImplementedException();
        }
    }
}