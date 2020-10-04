using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlProjectsRepo : IProjectsRepo
    {
        //The database context
        private readonly ProjectManagerContext _context;

        public SqlProjectsRepo(ProjectManagerContext context)
        {   
            _context = context;
        }


        public Project GetProjectById(int projectId)
        {
            return _context.Projects.Find(projectId);

        }

        public List<Task> GetProjectTasks(int projectId)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId).ToList();
        }

        public List<Task> GetProjectTasksByTaskStatus(int projectId, string taskStatus)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskStatus == taskStatus).ToList();
        }

        public List<Task> GetProjectTasksByTaskType(int projectId, int taskTypeId)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskTypeId == taskTypeId).ToList();
        }

        public List<Task> GetProjectTasksByUrgency(int projectId, string urgency)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId && t.Urgency == urgency).ToList();
        }

        public List<TaskType> GetProjectTaskTypes(int projectId)
        {
            return _context.TaskTypes.Where(tt => tt.ProjectId == projectId).ToList();
        }

        public List<AppUser> GetProjectUsers(int projectId)
        {
            //first get all the ProjectUser entries that are paired with the given project id
            var projectUsers = _context.ProjectUsers.Where(p => p.ProjectId == projectId).ToList();

            //to store all users with the returned project entries
            List<AppUser> users = new List<AppUser>();

            //loop through all user ids and store corresponding user entries
            for(int i = 0; i < projectUsers.Count; i++)
            {
                users.Add(_context.Users.Find(projectUsers[i].AppUserId));
            }

            //return the queried values
            return users;
        }

        public List<AppUser> GetProjectUsersByRole(int projectId, string role)
        {
            var projectUsers = _context.ProjectUsers.Where(p => (p.ProjectId == projectId && p.Role == role)).ToList();

            //store all users corresponding to the given projectId and role
            List<AppUser> users = new List<AppUser>();

            //loop through all users and store corresponding user entries 
            for(int i = 0; i < projectUsers.Count; i++)
            {
                users.Add(_context.Users.Find(projectUsers[i].AppUserId));
            }

            //return the queried values
            return users;
        }

        public List<ProjectUser> GetProjectUserRoles(int projectId)
        {
            var projectUsers = _context.ProjectUsers.Where(pu => pu.ProjectId == projectId).ToList();
            

            return projectUsers;

        }

        public List<TaskUpdate> GetTaskUpdatesByProject(int projectId)
        {
            //first, get all the tasks in a project
            var projectTasks = _context.Tasks.Where(t => t.ProjectId == projectId).ToList();

            //now, collect all the task updates in all the tasks of the project
            List<TaskUpdate> taskUpdates = new List<TaskUpdate>();
            //loop through all the projectTasks and gather every task update associated with the each task
            for (int i = 0; i < projectTasks.Count; i++)
            {
                taskUpdates.AddRange(_context.TaskUpdates.Where(tu => tu.TaskId == projectTasks[i].TaskId).ToList());
            }

            return taskUpdates;
        }

        public List<Task> GetUserProjectTasks(int projectId, string userId)
        {
            //first, get all the tasks by the given user
            var userTasks = _context.TaskUsers.Where(tu => tu.AppUserId == userId).ToList();

            //now, collect only those tasks of the project that have the given user

            //to store the needed tasks
            List<Task> userProjectTasks = new List<Task>();

            //for all the tasks by the user, collect only those that are in the given project
            for(int i = 0; i < userTasks.Count; i++)
            {
                userProjectTasks.AddRange(_context.Tasks.Where(t => t.TaskId == userTasks[i].TaskId && t.ProjectId == projectId).ToList());
            }

            return userProjectTasks;

        }

        public void AddUserToProject(ProjectUser projectUser)
        {
            if(projectUser == null) 
            {
                throw new ArgumentNullException(nameof(projectUser));
            }
            
            _context.Add(projectUser);
        }

        public Project UpdateProject(Project project)
        {
            if(project == null) 
            {
                throw new ArgumentNullException(nameof(project));
            }

            // var projectToUpdate = _context.Projects.Find(project.ProjectId);
            // //make changes to entry
            // projectToUpdate = project.ProjectDe;
            _context.Attach(project);
            _context.Entry(project).Property("Name").IsModified = true;
            _context.Entry(project).Property("Description").IsModified = true;
            return project;
        }

        public void DeleteProject(int projectId)
        {
            var project = GetProjectById(projectId);
            _context.Projects.Remove(project);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        
    }
}