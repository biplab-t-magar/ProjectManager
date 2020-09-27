using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITasksRepo
    {
        Task GetTaskById(int projectId, int taskId);
        List<Task> GetTasksByProject(int projectId);
        List<Task> GetTasksByTaskStatus(int projectId, string taskStatus);
        List<Task> GetTasksByUrgency(int projectId, string urgency);
        List<Task> GetTasksByTaskType(int projectId, int taskTypeId);

        
    }
}