using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskUpdatesRepo
    {
        List<TaskUpdate> GetTaskUpdates(int projectId, int taskId);
    }
}