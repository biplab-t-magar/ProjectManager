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
                new Task{ProjectId=1,TaskId=1,Name="Solidify domain knowledge", TaskStatus="Completed", TaskTypeId=2, MilestoneId=1},
                new Task{ProjectId=1,TaskId=2,Name="Build UML Diagrams", TaskStatus="Started", TaskTypeId=2, MilestoneId=1},
                new Task{ProjectId=1,TaskId=3,Name="Send UML for approval", TaskStatus="Not Started", TaskTypeId=2, MilestoneId=1},
            };
        }

        public Task GetTaskById(int projectId, int taskId)
        {
            for(int i = 0; i < tasks.Count; i++) {
                if(tasks[i].ProjectId == projectId && tasks[i].TaskId == taskId) {
                    return tasks[i];
                }
            }
            return null;
        }

        public IEnumerable<Task> GetTasksByMilestone(int projectId, int milestoneId)
        {
            List<Task> tasksByMilestone = new List<Task>();
            for(int i = 0; i < tasksByMilestone.Count; i++) {
                if(tasks[i].ProjectId == projectId && tasks[i].MilestoneId == milestoneId) {
                    tasksByMilestone.Add(tasks[i]);
                }
            }
            return tasksByMilestone;
        }

        public IEnumerable<Task> GetTasksByProject(int projectId)
        {
            List<Task> tasksByMilestone = new List<Task>();
            for(int i = 0; i < tasksByMilestone.Count; i++) {
                if(tasks[i].ProjectId == projectId) {
                    tasksByMilestone.Add(tasks[i]);
                }
            }
            return tasksByMilestone;
        }

        public IEnumerable<Task> GetTasksByTaskStatus(int projectId, string taskStatus)
        {
            List<Task> tasksByMilestone = new List<Task>();
            for(int i = 0; i < tasksByMilestone.Count; i++) {
                if(tasks[i].ProjectId == projectId && tasks[i].TaskStatus == taskStatus) {
                    tasksByMilestone.Add(tasks[i]);
                }
            }
            return tasksByMilestone;
        }

        public IEnumerable<Task> GetTasksByTaskType(int projectId, int taskTypeId)
        {
            List<Task> tasksByMilestone = new List<Task>();
            for(int i = 0; i < tasksByMilestone.Count; i++) {
                if(tasks[i].ProjectId == projectId && tasks[i].TaskTypeId == taskTypeId) {
                    tasksByMilestone.Add(tasks[i]);
                }
            }
            return tasksByMilestone;
        }

        public IEnumerable<Task> GetTasksByUrgency(int projectId, string urgency)
        {
            List<Task> tasksByMilestone = new List<Task>();
            for(int i = 0; i < tasksByMilestone.Count; i++) {
                if(tasks[i].ProjectId == projectId && tasks[i].Urgency == urgency) {
                    tasksByMilestone.Add(tasks[i]);
                }
            }
            return tasksByMilestone;
        }
    }
}