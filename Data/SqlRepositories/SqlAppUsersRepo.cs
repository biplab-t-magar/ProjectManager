using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlUsersRepo : IAppUsersRepo
    {
        private readonly ProjectManagerContext _context;

        public SqlUsersRepo(ProjectManagerContext context)
        {   
            _context = context;
        }

        public Project CreateUserProject(Project project, AppUser user)
        {
            if(project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            //add the date created attribute to the project
            project.TimeCreated = DateTime.Now;

            //create the new project
            _context.Add(project);

            //create a new Project user entry to assign the user to the project as a manager
            ProjectUser projectUser = new ProjectUser{Project = project};
            projectUser.AppUserId = user.Id;
            projectUser.Role = "Manager";
            projectUser.TimeAdded = DateTime.Now;
            //finally, add the ProjectUserEntry
            _context.Add(projectUser);
            //return the created project
            return project;
        }

        public AppUser GetUserById(string userId)
        {
            return _context.Users.Find(userId);
        }

        public List<Project> GetUserProjects(string userId)
        {
            //first get all the ProjectUser entries that are paired with the given user id
            var projectUsers = _context.ProjectUsers.Where(p => p.AppUserId == userId).ToList();

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

        public List<Task> GetUserTasks(string userId)
        {
            //first get all the TaskUser entries that are paired with the given user id
            var taskUsers = _context.TaskUsers.Where(tu => tu.AppUserId == userId).ToList();

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

        public List<Task> GetUserTasksByTaskStatus(string userId, string taskStatus)
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

        public List<Task> GetUserTasksByTaskType(string userId, int taskTypeId)
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

        public List<Task> GetUserTasksByUrgency(string userId, string urgency)
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

        public bool SaveChanges()
        {
            //save all the changes to the database
            return _context.SaveChanges() >= 0;
        }
    }
}