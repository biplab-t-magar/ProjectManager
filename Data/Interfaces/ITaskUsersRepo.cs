using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskUsersRepo
    {
        IEnumerable<TaskUser> GetUsersByTask(int projectId, int taskId);
        IEnumerable<TaskUser> GetTasksByUser(int userId);
        IEnumerable<TaskUser> GetTasksInProjectByUser(int projectId, int userId);
    }
}