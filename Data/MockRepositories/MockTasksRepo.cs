using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.MockRepositories
{
    public class MockTaskRepo : ITasksRepo
    {
        private List<Task> tasks;
        public MockTaskRepo()
        {
            tasks = new List<Task>
            {
                // new Task{ProjectId=1,TaskId=1,Name="Solidify domain knowledge", TaskStatus="Completed", TaskType="", TaskTypeId=, MilestoneId=}
            };
        }

        public Task GetTaskById(int projectId, int taskId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Task> GetTasksByMilestone(int projectId, int milestoneId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Task> GetTasksByProject(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Task> GetTasksByTaskStatus(int projectId, int taskStatus)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Task> GetTasksByTaskType(int projectId, int taskTypeId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Task> GetTasksByUrgency(int projectId, string urgency)
        {
            throw new System.NotImplementedException();
        }
    }
}