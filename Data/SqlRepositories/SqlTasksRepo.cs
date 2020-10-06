using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlTasksRepo : ITasksRepo
    {
        private readonly ProjectManagerContext _context;

        public SqlTasksRepo(ProjectManagerContext context)
        {   
            _context = context;
        }
        public Task GetTaskById(int taskId)
        {
            return _context.Tasks.Find(taskId);
        }

        public List<AppUser> GetTaskUsers(int taskId)
        {
            //first get all the TaskUser entries with the matching taskId
            var taskUsers = _context.TaskUsers.Where(tu => tu.TaskId == taskId).ToList();

            List<AppUser> users = new List<AppUser>();

            //loop through all taskUser entries and store their corresponding User entries
            for(int i = 0; i < taskUsers.Count; i++)
            {
                users.Add(_context.Users.Find(taskUsers[i].AppUserId));
            }

            return users;
        }

        public TaskUpdate GetTaskUpdate(int taskUpdateId)
        {
            return _context.TaskUpdates.Find(taskUpdateId);
        }

        public List<TaskUpdate> GetTaskUpdatesByTask(int taskId)
        {
            return _context.TaskUpdates.Where(tu => tu.TaskId == taskId).ToList();
        }

        public TaskUserUpdate GetTaskUserUpdate(int taskUserUpdateId)
        {
            return _context.TaskUserUpdates.Find(taskUserUpdateId);
        }

        public List<TaskUserUpdate> GetTaskUserUpdatesByTask(int taskId)
        {
            return _context.TaskUserUpdates.Where(tuu => tuu.TaskId == taskId).ToList();
        }

        public Task UpdateTask(Task task, string updaterId)
        {
            if(task == null) 
            {
                throw new ArgumentNullException(nameof(task));
            }


            var taskToUpdate = _context.Tasks.Find(task.TaskId);

            //now, for each changed value, store a TaskUpdate record with the updated value 
            
            //time of task update
            var timeStamp = DateTime.Now;            

            //for every updateable field in the Task object, if there was a change, then add a TaskUpdate entry            
            //if the name of the task was changed
            if(task.Name != taskToUpdate.Name)
            {
                //add the updated name to the taskUpdate object
                var taskNameUpdate = new TaskUpdate{TaskId = task.TaskId, TimeStamp = timeStamp, UpdaterId = updaterId};
                taskNameUpdate.Name = task.Name;
                _context.Add(taskNameUpdate);
            }
            //if the taskstatus of the task was changed
            if(task.TaskStatus != taskToUpdate.TaskStatus)
            {
                //add the updated TaskStatus to the taskUpdate object
                var taskStatusUpdate = new TaskUpdate{TaskId = task.TaskId, TimeStamp = timeStamp, UpdaterId = updaterId};
                taskStatusUpdate.TaskStatus = task.TaskStatus;
                _context.Add(taskStatusUpdate);
            }
            //if the Urgency of the task was changed
            if(task.Urgency != taskToUpdate.Urgency)
            {
                //add the updated Urgency to the taskUpdate object
                var taskUrgencyUpdate = new TaskUpdate{TaskId = task.TaskId, TimeStamp = timeStamp, UpdaterId = updaterId};
                taskUrgencyUpdate.Urgency = task.Urgency;
                _context.Add(taskUrgencyUpdate);
            }
            //if the TaskTypeId of the task was changed
            if(task.TaskTypeId != taskToUpdate.TaskTypeId)
            {
                //add the updated TaskTypeId to the taskUpdate object
                var taskTypeUpdate = new TaskUpdate{TaskId = task.TaskId, TimeStamp = timeStamp, UpdaterId = updaterId};
                taskTypeUpdate.TaskTypeId = task.TaskTypeId;
                _context.Add(taskTypeUpdate);
            }

             //update the task
            _context.Entry(taskToUpdate).CurrentValues.SetValues(task);

            return task;
        }

        public bool IsAssignedToTask(int taskId, string userId)
        {
            var taskUsers = _context.TaskUsers.Where(tu => tu.TaskId == taskId && tu.AppUserId == userId).ToList();
            if(taskUsers.Count == 0)
            {
                return false;
            }
            _context.Entry(taskUsers[0]).State = EntityState.Detached;
            return true;
        }

        public void AssignUserToTask(TaskUser taskUser, string updaterId)
        {
            if(taskUser == null)
            {   
                throw new ArgumentNullException(nameof(taskUser)); 
            }

            //create a TaskUserUpdate entry as well
            var TaskUserUpdate = new TaskUserUpdate{
                TaskId = taskUser.TaskId,
                AppUserId = taskUser.AppUserId,
                UpdaterId = updaterId,
                TimeAdded = DateTime.Now
            };

            _context.Add(taskUser);
            AddTaskUserUpdate(TaskUserUpdate);


        }

        public void RemoveUserFromTask(TaskUser taskUser, string updaterId)
        {
            
            //create a TaskUserUpdate entry
            var taskUserUpdate = new TaskUserUpdate{
                TaskId = taskUser.TaskId,
                AppUserId = taskUser.AppUserId,
                UpdaterId = updaterId,
                TimeRemoved = DateTime.Now
            };
            _context.TaskUsers.Remove(taskUser); 
            
            AddTaskUserUpdate(taskUserUpdate);

        }

        public List<TaskComment> GetTaskComments(int taskId)
        {
            var taskComments = _context.TaskComments.Where(tc => tc.TaskId == taskId).ToList();
            //if no taskcomments were found, return an empty list
            if(taskComments == null)
            {
                return new List<TaskComment>();
            } 
            else 
            {
                return taskComments;
            }
        }

        public void AddTaskComment(TaskComment taskComment)
        {
            if(taskComment == null)
            {
                throw new ArgumentNullException(nameof(taskComment));
            }
            _context.Add(taskComment);
        }

        public void AddTaskUpdate(TaskUpdate taskUpdate)
        {
            if(taskUpdate == null)
            {
                throw new ArgumentNullException(nameof(taskUpdate));
            }
            _context.Add(taskUpdate);
        }

        public void AddTaskUserUpdate(TaskUserUpdate taskUserUpdate)
        {
            if(taskUserUpdate == null)
            {
                throw new ArgumentNullException(nameof(taskUserUpdate));
            }
            _context.Add(taskUserUpdate);
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }


        // public List<Task> GetTasksByProject(int projectId)
        // {
        //     return _context.Tasks.Where(t => t.ProjectId == projectId).ToList();
        // }


        // public List<Task> GetTasksByTaskStatus(int projectId, string taskStatus)
        // {
        //     return _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskStatus == taskStatus).ToList();
        // }

        // public List<Task> GetTasksByTaskType(int projectId, int taskTypeId)
        // {
        //     return _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskTypeId == taskTypeId).ToList();
        // }

        // public List<Task> GetTasksByUrgency(int projectId, string urgency)
        // {
        //     return _context.Tasks.Where(t => t.ProjectId == projectId && t.Urgency == urgency).ToList();
        // }




    }
}