using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IProjectsRepo
    {
        Project GetProjectById(int projectId);
        List<User> GetProjectUsers(int projectId);

        List<User> GetProjectUsersByRole(int projectId, string role);


        List<Task> GetProjectTasks(int projectId);
        List<Task> GetUserProjectTasks(int projectId, int userId);

        List<Task> GetProjectTasksByTaskStatus(int projectId, string taskStatus);
        List<Task> GetProjectTasksByUrgency(int projectId, string urgency);
        List<Task> GetProjectTasksByTaskType(int projectId, int taskTypeId);

        List<TaskType> GetProjectTaskTypes(int projectId);
        List<TaskType> GetProjectTaskTypesByDefaultUrgency(int projectId, string defaultUrgency);

        List<TaskUpdate> GetTaskUpdatesByProject(int projectId);
    }
}