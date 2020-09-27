using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskTypeUpdatesRepo
    {
        List<TaskTypeUpdate> GetTaskTypeUpdates(int projectId, int taskTypeId);

    }
}