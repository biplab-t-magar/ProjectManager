using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.MockRepositories
{
    public class MockTaskUpdatesRepo : ITaskUpdatesRepo
    {
        private List<TaskUpdate> taskUpdates;
        public MockTaskUpdatesRepo()
        {
            taskUpdates = new List<TaskUpdate>
            {
                new TaskUpdate{},
            }
        }

        public TaskUpdate GetTaskUpdates(int projectId, int taskId)
        {
            throw new System.NotImplementedException();
        }
    }
}