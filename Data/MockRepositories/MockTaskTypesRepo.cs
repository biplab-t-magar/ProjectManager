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
                new TaskType{},
            }
        }

        public TaskType GetTaskTypeById(int taskTypeId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TaskType> GetTaskTypesByOrganization(int organizationId)
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

        public IEnumerable<TaskType> GetTaskTypesByUser(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}