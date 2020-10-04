using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IProjectsRepo
    {
        Project GetProjectById(int projectId);
        List<AppUser> GetProjectUsers(int projectId);

        List<AppUser> GetProjectUsersByRole(int projectId, string role);


        List<Task> GetProjectTasks(int projectId);
        List<Task> GetUserProjectTasks(int projectId, string userId);

        List<Task> GetProjectTasksByTaskStatus(int projectId, string taskStatus);
        List<Task> GetProjectTasksByUrgency(int projectId, string urgency);
        List<Task> GetProjectTasksByTaskType(int projectId, int taskTypeId);

        List<TaskType> GetProjectTaskTypes(int projectId);

        List<ProjectUser> GetProjectUserRoles(int projectId);

        List<TaskUpdate> GetTaskUpdatesByProject(int projectId);
        // List<TaskUpdate> GetTaskUpdatesByUpdaterInProject(int projectId, int updaterId);

        void AddUserToProject(ProjectUser projectUser);

        Project UpdateProject(Project project);

        void DeleteProject(int projectId);

        bool SaveChanges();


    }
}