using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IUsersRepo
    {
        User GetUserById(int userId);        
        List<Project> GetUserProjects(int userId);

        List<Task> GetUserTasks(int userId);

        List<Task> GetUserTasksByTaskStatus(int userId, string taskStatus);
        List<Task> GetUserTasksByUrgency(int userId, string urgency);
        List<Task> GetUserTasksByTaskType(int userId, int taskTypeId);

        // List<TaskUpdate> GetTaskUpdatesByUpdater(int projectId, int taskId, int updaterId);
        // List<TaskUserUpdate> GetTaskUserUpdatesByUpdater(int projectId, int taskId, int updaterId);
        // List<TaskUserUpdate> GetTaskUserUpdatesForUser(int projectId, int taskId, int userId);

    }
}