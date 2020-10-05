using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IAppUsersRepo
    {
        AppUser GetUserById(string userId);   

        AppUser GetUserByUserName(string userName);     
        List<Project> GetUserProjects(string userId);

        List<Task> GetUserTasks(string userId);

        List<Task> GetUserTasksByTaskStatus(string userId, string taskStatus);
        List<Task> GetUserTasksByUrgency(string userId, string urgency);
        List<Task> GetUserTasksByTaskType(string userId, int taskTypeId);

        // List<TaskUpdate> GetTaskUpdatesByUpdater(int projectId, int taskId, int updaterId);
        // List<TaskUserUpdate> GetTaskUserUpdatesByUpdater(int projectId, int taskId, int updaterId);
        // List<TaskUserUpdate> GetTaskUserUpdatesForUser(int projectId, int taskId, int userId);

        // AppUser GetUser
        Project CreateUserProject(Project project, AppUser user);
        bool SaveChanges();
    }
}