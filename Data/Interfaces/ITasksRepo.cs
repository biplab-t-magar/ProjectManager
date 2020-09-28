using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITasksRepo
    {
        Task GetTaskById(int projectId, int taskId);

        List<User> GetTaskUsers(int projectId, int taskId);

        TaskUpdate GetTaskUpdate(int projectId, int taskId, int taskUpdateId);
        
        List<TaskUpdate> GetTaskUpdatesByTask(int projectId, int taskId);

        TaskUserUpdate GetTaskUserUpdate(int projectId, int taskId, int userId, int taskUserUpdateId);
        List<TaskUserUpdate> GetTaskUserUpdatesByTask(int projectId, int taskId);
    

    }
}