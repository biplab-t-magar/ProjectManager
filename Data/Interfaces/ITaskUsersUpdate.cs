using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskUserUpdatesRepo
    {
        List<TaskUserUpdate> GetTaskUserUpdates(int projectId, int taskId);
    }
}