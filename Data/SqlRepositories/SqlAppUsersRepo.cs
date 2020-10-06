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
            projectUser.Role = "Administrator";
            projectUser.TimeAdded = DateTime.Now;
            //finally, add the ProjectUserEntry
            _context.Add(projectUser);

            //return the created project
            return project;
        }

        public AppUser GetUserById(string userId)
        {
            return _context.AppUsers.Find(userId);
        }

        public AppUser GetUserByUserName(string userName)
        {
            var user = _context.AppUsers.Where(u => u.UserName == userName).ToList();
            if(user.Count == 0) 
            {
                return null;
            }
            //it is ensured that there is only one user per username
            return user[0];
        }

        public List<ProjectInvitation> GetUserProjectInvitations(string userId)
        {
            return _context.ProjectInvitations.Where(pi => pi.InviteeId == userId).ToList();
        }

        public List<AppUser> GetUserProjectInviters(string userId)
        {
            //first, get all the project invitations for the user
            var projectInvitations = _context.ProjectInvitations.Where(pi => pi.InviteeId == userId).ToList();

            List<AppUser> inviters = new List<AppUser>();

            //now, collect a list of all the AppUser objects corresponding to the InviterId of the project invitations
            for(int i = 0; i < projectInvitations.Count; i++)
            {
                inviters.Add(_context.AppUsers.Find(projectInvitations[i].InviterId));
            }

            return inviters;


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

        public List<Project> GetUserProjectsInvitedTo(string userId)
        {
            //first, get all the project invitations for the user
            var projectInvitations = _context.ProjectInvitations.Where(pi => pi.InviteeId == userId).ToList();

            List<Project> projectsInvitedTo = new List<Project>();

            //now, collect a list of all the AppUser objects corresponding to the InviterId of the project invitations
            for(int i = 0; i < projectInvitations.Count; i++)
            {
                projectsInvitedTo.Add(_context.Projects.Find(projectInvitations[i].ProjectId));
            }

            return projectsInvitedTo;
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

        public AppUser UpdateUser(AppUser user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userToUpdate = _context.Users.Find(user.Id);

            _context.Entry(userToUpdate).CurrentValues.SetValues(user);
            return user;

        }

        public bool SaveChanges()
        {
            //save all the changes to the database
            return _context.SaveChanges() >= 0;
        }

        public ProjectUser GetUserRoleInProject(string userId, int projectId)
        {
            return _context.ProjectUsers.Find(projectId, userId);
        }
    }
}