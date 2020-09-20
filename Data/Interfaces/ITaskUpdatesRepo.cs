using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskUpdatesRepo
    {
        IEnumerable<TaskUpdate> GetTaskUpdates(int projectId, int taskId);
    }
}