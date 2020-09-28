using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskUsersRepo
    {
        List<User> GetUsersByTask(int projectId, int taskId);
        List<TaskUser> GetTasksByUser(int userId);
        List<TaskUser> GetTasksInProjectByUser(int projectId, int userId);
    }
}