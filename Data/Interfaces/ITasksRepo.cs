/* ITasksRepo.cs
 This file contains the ITasksRepo interface, which represents one of the four Repository interfaces in the ProjectManager project.
 A repository interface defines the list of all the public functions for a repository used by the web application. THe ITasksRepo represents
 a repository that contains all information on the tasks of the ProjectManager web application
*/

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