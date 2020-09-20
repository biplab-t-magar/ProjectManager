using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskTypesRepo
    {
        TaskType GetTaskTypeById(int projectId, int taskTypeId);
        IEnumerable<TaskType> GetTaskTypesByUrgency(string urgency);
        IEnumerable<TaskType> GetTaskTypesByProject(int projectId);

    }
}