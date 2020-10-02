using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IAppUsersRepo
    {
        AppUser GetUserById(string userId);        
        List<Project> GetUserProjects(string userId);

        List<Task> GetUserTasks(string userId);

        List<Task> GetUserTasksByTaskStatus(string userId, string taskStatus);
        List<Task> GetUserTasksByUrgency(string userId, string urgency);
        List<Task> GetUserTasksByTaskType(string userId, int taskTypeId);

        // List<TaskUpdate> GetTaskUpdatesByUpdater(int projectId, int taskId, int updaterId);
        // List<TaskUserUpdate> GetTaskUserUpdatesByUpdater(int projectId, int taskId, int updaterId);
        // List<TaskUserUpdate> GetTaskUserUpdatesForUser(int projectId, int taskId, int userId);

    }
}