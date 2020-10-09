/* SqlTasksRepo.cs
 This file contains the SqlTasksRepo class. The SqlTasksRepo class is an implementation of the ITasksRepo interface. It represents 
 an implementation of the interface by using an SQL database to store and retrieve data. So this repository class communicates with an SQL database
 (a PostgreSQL database, specifically),while providing all the functions to retrieve and manipulate the entries in the database
 that are listed in the interface it implements.
*/
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
        //the database context
        private readonly ProjectManagerContext _context;

        /**/
        /*
        * NAME:
        *      SqlTasksRepo - constructor for SqlTasksRepo
        * SYNOPSIS:
                SqlTasksRepo(ProjectManagerContext context)
        *           context --> the database context that is injected into the class through dependency injection
        * DESCRIPTION:
                The constructor implements the SqlTasksRepo class, which represents an implementation of the ITasksRepo interface
                It initializes the _context member variable, which will be used by all the functions in this class for data access
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/02/2020 
        * /
        /**/
        public SqlTasksRepo(ProjectManagerContext context)
        {   
            _context = context;
        }

        /**/
        /*
        * NAME:
        *      GetTaskById - gets the task object with the given id
        * SYNOPSIS:
                GetTaskById(int taskId)
        *           taskId --> the id of the task to be returned
        * DESCRIPTION:
                Accesses the database context in order to find the task of the given id
        * RETURNS
                A Task object with the given id, taken from the database
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/04/2020 
        * /
        /**/
        public Task GetTaskById(int taskId)
        {
            return _context.Tasks.Find(taskId);
        }

        /**/
        /*
        * NAME:
        *      GetTaskUsers - gets the users assigned to the given task 
        * SYNOPSIS:
                GetTaskUsers(int taskId)
        *           taskId --> the id of the task
        * DESCRIPTION:
                Accesses the database context in order to find the users that are assigned to a task
        * RETURNS
                A Task object with the given id, taken from the database
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/04/2020 
        * /
        /**/
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


        /**/
        /*
        * NAME:
        *      GetTaskUpdate - gets a task update with the given task update id
        * SYNOPSIS:
                GetTaskUpdate(int taskUpdateId)
        *           taskUpdateId --> the id of the TaskUpdate object to be returned
        * DESCRIPTION:
                Accesses the database context in order to find the needed TaskUpdate entry
        * RETURNS
                A TaskUpdate object with the given id, taken from the database
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/04/2020 
        * /
        /**/
        public TaskUpdate GetTaskUpdate(int taskUpdateId)
        {
            return _context.TaskUpdates.Find(taskUpdateId);
        }

        /**/
        /*
        * NAME:
        *      GetTaskUpdatesByTask - returns a list of all task updates for the given task
        * SYNOPSIS:
                GetTaskUpdatesByTask(int taskId)
        *           taskId --> the id of the task whose task updates are to be returned
        * DESCRIPTION:
                Accesses the database context in order to find the needed task updates
        * RETURNS
                A list of TaskUpdate objects that are associated with the given task
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/04/2020 
        * /
        /**/
        public List<TaskUpdate> GetTaskUpdatesByTask(int taskId)
        {
            return _context.TaskUpdates.Where(tu => tu.TaskId == taskId).ToList();
        }

        /**/
        /*
        * NAME:
        *      GetTaskUserUpdate - returns a taskUserUpdate object with the given id
        * SYNOPSIS:
                GetTaskUserUpdate(int taskUserUpdateId)
        *           taskUserUpdateId --> the id of the taskUserUpdate to be returned
        * DESCRIPTION:
                Accesses the database context in order to find the needed task updates
        * RETURNS
                The TaskUserUpdate object with the given id
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/04/2020 
        * /
        /**/
        public TaskUserUpdate GetTaskUserUpdate(int taskUserUpdateId)
        {
            return _context.TaskUserUpdates.Find(taskUserUpdateId);
        }

        /**/
        /*
        * NAME:
        *      GetTaskUserUpdatesByTask - returns a list of taskUserUpdate objects associated with the given task
        * SYNOPSIS:
                GetTaskUserUpdatesByTask(int taskId)
        *           taskId --> the id of the task whose associated taskUserUpdate objects are to be returned
        * DESCRIPTION:
                Accesses the database context in order to find the needed task user updates
        * RETURNS
                A list of TaskUserUpdate objects that are associated with the given task
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/04/2020 
        * /
        /**/
        public List<TaskUserUpdate> GetTaskUserUpdatesByTask(int taskId)
        {
            return _context.TaskUserUpdates.Where(tuu => tuu.TaskId == taskId).ToList();
        }

        /**/
        /*
        * NAME:
        *      UpdateTask - updates a task and records the task update too
        * SYNOPSIS:
                UpdateTask(Task task, string updaterId)
        *           task --> the task that is to be updated (it must contain the updated values; taskId must stay the same)
                    updaterId --> the id of the user who made the update
        * DESCRIPTION:
                Accesses the database context in order to update the task. Simultaneouslyk this function generates a TaskUpdate
                entry from the given information, so that the program can keep track of activity
        * RETURNS
                The updated task object
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/06/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      IsAssignedToTask - checks if a user is assigned to a task
        * SYNOPSIS:
                IsAssignedToTask(int taskId, string userId)
        *           taskId --> the id of the task to be checked
                    userId --> the id of the user to be checked
        * DESCRIPTION:
                Accesses the database context in order to see if the given user is assigned to the given task
        * RETURNS
                true if the user is assigned to the task, false otherwise
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/06/2020 
        * /
        /**/
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


        /**/
        /*
        * NAME:
        *      AssignUserToTask - assigns a user to a task
        * SYNOPSIS:
                AssignUserToTask(TaskUser taskUser, string updaterId)
        *           taskUser --> the TaskUser object that represents the user's assignment to a task
                    updaterId --> the id of the user who assigned the user to the task
        * DESCRIPTION:
                Accesses the database context in order to assign the user to the task
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/06/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      RemoveUserFromTask - unassigns a user from a task
        * SYNOPSIS:
                RemoveUserFromTask(TaskUser taskUser, string updaterId)
        *           taskUser --> the TaskUser object that represents the user's assignment to a task
                    updaterId --> the id of the user who is removing the user from the task
        * DESCRIPTION:
                Accesses the database context in order to unassign the user from the task
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/06/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetTaskComments - gets all the comment made under a task
        * SYNOPSIS:
                GetTaskComments(int taskId)
        *           taskId --> the id of the task whose comments are to be returned
        * DESCRIPTION:
                Accesses the database context in order to return all the task comments under a task
        * RETURNS
                A list of all the TaskComment objects under the given task
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/06/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      AddTaskComment - add a comment to a task
        * SYNOPSIS:
                AddTaskComment(TaskComment taskComment)
        *           taskComment --> the taskComment object to be added to the database
        * DESCRIPTION:
                Accesses the database context in order to add a new TaskComment entry
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/12/2020 
        * /
        /**/
        public void AddTaskComment(TaskComment taskComment)
        {
            if(taskComment == null)
            {
                throw new ArgumentNullException(nameof(taskComment));
            }
            _context.Add(taskComment);
        }

        /**/
        /*
        * NAME:
        *      AddTaskUpdate - adds a TaskUpdate entry to the database
        * SYNOPSIS:
                AddTaskUpdate(TaskUpdate taskUpdate)
        *           taskUpdate --> the taskUpdate object to be added to the database
        * DESCRIPTION:
                Accesses the database context in order to add the new TaskUpdate object
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/12/2020 
        * /
        /**/
        public void AddTaskUpdate(TaskUpdate taskUpdate)
        {
            if(taskUpdate == null)
            {
                throw new ArgumentNullException(nameof(taskUpdate));
            }
            _context.Add(taskUpdate);
        }

        /**/
        /*
        * NAME:
        *      AddTaskUserUpdate - adds a TaskUserUpdate entry to the database
        * SYNOPSIS:
                AddTaskUserUpdate(TaskUserUpdate taskUserUpdate)
        *           taskUserUpdate --> the taskUserUpdate object to be added to the database
        * DESCRIPTION:
                Accesses the database context in order to add the new TaskUserUpdate object
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/12/2020 
        * /
        /**/
        public void AddTaskUserUpdate(TaskUserUpdate taskUserUpdate)
        {
            if(taskUserUpdate == null)
            {
                throw new ArgumentNullException(nameof(taskUserUpdate));
            }
            _context.Add(taskUserUpdate);
        }

        /**/
        /*
        * NAME:
        *      SaveChanges - saves all changes made so far using the context into the database
        * SYNOPSIS:
                SaveChanges()
        * DESCRIPTION:
                Accesses the database context in order save changes to it
        * RETURNS
                true if savechanges was successful, false if not
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/02/2020 
        * /
        /**/
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }


    }
}