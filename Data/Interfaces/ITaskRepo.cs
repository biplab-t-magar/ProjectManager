using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskRepo
    {
        Task GetTaskById(int projectId, int taskId);
        IEnumerable<Task> GetTasksByProject(int projectId);
        IEnumerable<Task> GetTasksByMilestone(int projectId, int milestoneId);
        IEnumerable<Task>GetTasksByTaskStatus(int projectId, int taskStatus);
        IEnumerable<Task> GetTasksByUrgency(int projectId, string urgency);
        IEnumerable<Task> GetTasksByTaskType(int projectId, int taskTypeId);

        
    }
}