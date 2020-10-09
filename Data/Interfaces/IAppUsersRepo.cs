/* IAppUsersRepo.cs
 This file contains the IAppUsersRepo interface, which represents one of the four Repository interfaces in the ProjectManager project.
 A repository interface defines the list of all the public functions for a repository used by the web application. THe IAppUsersRepo represents
 a repository that contains all information on the users of the ProjectManager web application
*/

using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IAppUsersRepo
    {
        AppUser GetUserById(string userId);   

        AppUser UpdateUser(AppUser user);

        AppUser GetUserByUserName(string userName);     
        List<Project> GetUserProjects(string userId);

        ProjectUser GetUserRoleInProject(string userId, int projectId);

        List<Task> GetUserTasks(string userId);

        List<Task> GetUserTasksByTaskStatus(string userId, string taskStatus);
        List<Task> GetUserTasksByUrgency(string userId, string urgency);
        List<Task> GetUserTasksByTaskType(string userId, int taskTypeId);

        List<TaskUpdate> GetTaskUpdatesByUpdater(string updaterId);
        List<TaskUserUpdate> GetTaskUserUpdatesByUpdater(string updaterId);
        // List<TaskUserUpdate> GetTaskUserUpdatesForUser(int projectId, int taskId, int userId);

        // AppUser GetUser
        Project CreateUserProject(Project project, AppUser user);

        List<ProjectInvitation> GetUserProjectInvitations(string userId);

        List<AppUser> GetUserProjectInviters(string userId);

        List<Project> GetUserProjectsInvitedTo(string userId);

        List<TaskComment> GetCommentsByUser(string userId);

        
        bool SaveChanges();
    }
}