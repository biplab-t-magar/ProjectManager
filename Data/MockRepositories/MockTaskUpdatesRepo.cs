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
                new TaskUpdate{ProjectId=1, TaskId=1, Status="Started", Time="demo time"},
                new TaskUpdate{ProjectId=1, TaskId=1, Status="Under Review", Time="another demo time"},
                new TaskUpdate{ProjectId=1, TaskId=1, Status="Completed", Time="demo time"},
                new TaskUpdate{ProjectId=1, TaskId=2, Status="Started", Time="time"},
            };
        }

        public IEnumerable<TaskUpdate> GetTaskUpdates(int projectId, int taskId)
        {
            List<TaskUpdate> taskUpdateForATask = new List<TaskUpdate>();
            for(int i = 0; i < taskUpdates.Count; i++) {
                if(taskUpdates[i].ProjectId == projectId && taskUpdates[i].TaskId == taskId) {
                    taskUpdateForATask.Add(taskUpdates[i]);
                }
            }
            return taskUpdateForATask;
        }
    }
}