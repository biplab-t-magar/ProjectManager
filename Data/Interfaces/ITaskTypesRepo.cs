using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskTypesRepo
    {
        TaskType GetTaskTypeById(int taskTypeId);
        

    }
}