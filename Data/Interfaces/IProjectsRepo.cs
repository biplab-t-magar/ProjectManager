/* IProjectsRepo.cs
 This file contains the IProjectsRepo interface, which represents one of the four Repository interfaces in the ProjectManager project.
 A repository interface defines the list of all the public functions for a repository used by the web application. THe IProjectsRepo represents
 a repository that contains all information on the projects of the ProjectManager web application
*/

using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IProjectsRepo
    {
        Project GetProjectById(int projectId);
        List<AppUser> GetProjectUsers(int projectId);

        List<ProjectInvitation> GetProjectInvitations(int projectId);

        bool HasUserBeenInvited(int projectId, string userId);

        List<AppUser> GetProjectInvitees(int projectId);

        bool DeleteProjectInvite(int projectId, string inviteeId);

        void AddProjectInvite(ProjectInvitation projectInvitation);

        Task CreateTask(Task task, string creatorId);

        List<Task> GetProjectTasks(int projectId);
        List<Task> GetUserProjectTasks(int projectId, string userId);

        List<Task> GetProjectTasksByTaskType(int projectId, int taskTypeId);

        List<TaskType> GetProjectTaskTypes(int projectId);

        List<ProjectUser> GetProjectUserRoles(int projectId);

        List<TaskUpdate> GetTaskUpdatesByProject(int projectId);
        List<TaskUpdate> GetTaskUpdatesByUpdaterInProject(int projectId, string updaterId);

        List<TaskComment> GetProjectTaskComments(int projectId);
        List<TaskComment> GetProjectTaskCommentsByUser(int projectId, string userId);

        List<TaskUserUpdate> GetProjectTaskUserUpdates(int projectId);

        List<TaskUserUpdate> GetProjectTaskUserUpdatesByUpdater(int projectId, string updaterId);

        void AddUserToProject(ProjectUser projectUser);


        Project UpdateProject(Project project);

        ProjectUser SetProjectUserRole(ProjectUser projectUser, string role);


        void DeleteProject(int projectId);

        bool SaveChanges();


    }
}