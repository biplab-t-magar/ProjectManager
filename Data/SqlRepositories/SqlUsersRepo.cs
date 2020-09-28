using System.Collections.Generic;
using System.Linq;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlUsersRepo : IUsersRepo
    {
        private readonly ProjectManagerContext _context;

        public SqlUsersRepo(ProjectManagerContext context)
        {   
            _context = context;
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public List<Project> GetUserProjects(int userId)
        {
            //first get all the ProjectUser entries that are paired with the given user id
            var projectUsers = _context.ProjectUsers.Where(p => p.UserId == userId).ToList();

            //to store all projects with the returned return user entries
            List<Project> projects = new List<Project>();

            //loop through all user ids and store corresponding project entries
            for(int i = 0; i < projectUsers.Count; i++)
            {
                projects.Add(_context.Projects.Find(projectUsers[i].ProjectId));
            }

            //return the queried values
            return projects;
        }

        public List<Task> GetUserTasks(int userId)
        {
            //first get all the TaskUser entries that are paired with the given user id
            var taskUsers = _context.TaskUsers.Where(tu => tu.UserId == userId).ToList();

            //to store all tasks with the returned user ids
            List<Task> tasks = new List<Task>();

            //loop through all user ids and store corresponding project entries
            for(int i = 0; i < taskUsers.Count; i++)
            {
                tasks.Add(_context.Tasks.Find(taskUsers[i].TaskId));
            }

            //return the queried values
            return tasks;
        }

        public List<Task> GetUserTasksByTaskStatus(int userId, string taskStatus)
        {
            //first get all the task entries that are paried with the given user id and taskStatus
            List<Task> tasks = GetUserTasks(userId);

            //now, output only those tasks that have the given taskStatus
            List<Task> tasksBytaskStatus = new List<Task>();

            for(int i = 0; i < tasks.Count; i++)
            {
                if(tasks[i].TaskStatus == taskStatus)
                {
                    tasksBytaskStatus.Add(tasks[i]);
                }
            }
            return tasksBytaskStatus;
        }

        public List<Task> GetUserTasksByTaskType(int userId, int taskTypeId)
        {
            //first get all the task entries that are paried with the given user id and taskStatus
            List<Task> tasks = GetUserTasks(userId);

            //now, output only those tasks that have the given task type
            List<Task> tasksBytaskType = new List<Task>();

            for(int i = 0; i < tasks.Count; i++)
            {
                if(tasks[i].TaskTypeId == taskTypeId)
                {
                    tasksBytaskType.Add(tasks[i]);
                }
            }
            return tasksBytaskType;
        }

        public List<Task> GetUserTasksByUrgency(int userId, string urgency)
        {
            //first get all the task entries that are paried with the given user id and taskStatus
            List<Task> tasks = GetUserTasks(userId);

            //now, output only those tasks that have the given task type
            List<Task> tasksByUrgency = new List<Task>();

            for(int i = 0; i < tasks.Count; i++)
            {
                if(tasks[i].Urgency == urgency)
                {
                    tasksByUrgency.Add(tasks[i]);
                }
            }
            return tasksByUrgency;
        }
    }
}