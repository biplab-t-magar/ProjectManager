using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskTypesRepo
    {
        TaskType GetTaskTypeById(int taskType);
        IEnumerable<TaskType> GetTaskTypesByUrgency(string urgency);
        IEnumerable<TaskType> GetTaskTypesByProject(int projectId);
        IEnumerable<TaskType> GetTaskTypesByOrganization(int organizationId);
        IEnumerable<TaskType> GetTaskTypesByUser(int userId);

    }
}