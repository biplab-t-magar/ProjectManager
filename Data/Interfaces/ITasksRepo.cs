using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITasksRepo
    {
        Task GetTaskById(int taskId);

        List<AppUser> GetTaskUsers(int taskId);

        TaskUpdate GetTaskUpdate(int taskUpdateId);
        
        List<TaskUpdate> GetTaskUpdatesByTask(int taskId);

        TaskUserUpdate GetTaskUserUpdate(int taskUserUpdateId);
        List<TaskUserUpdate> GetTaskUserUpdatesByTask(int taskId);

        bool IsAssignedToTask(int taskId, string userId);

        void AssignUserToTask(TaskUser taskUser, string updaterId);

        void RemoveUserFromTask(TaskUser taskUser, string updaterId);

        List<TaskComment> GetTaskComments(int taskId);

        void AddTaskComment(TaskComment taskComment);

        Task UpdateTask(Task task, string updaterId);

        void AddTaskUpdate(TaskUpdate taskUpdate);
        void AddTaskUserUpdate(TaskUserUpdate taskUserUpdate);
        bool SaveChanges();
    

    }
}