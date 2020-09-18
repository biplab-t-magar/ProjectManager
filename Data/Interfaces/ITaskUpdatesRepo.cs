using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskUpdatesRepo
    {
        TaskUpdate GetTaskUpdates(int projectId, int taskId);
    }
}