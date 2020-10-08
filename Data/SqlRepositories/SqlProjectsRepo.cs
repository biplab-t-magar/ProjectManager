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
            var tasks = _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskTypeId == taskTypeId);
            if(tasks == null) {
                return new List<Task>();
            } else {
                return tasks.ToList();
            }
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

            //to collect all the task updates in all the tasks of the project
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


            var projectToUpdate = _context.Projects.Find(project.ProjectId);
            // //make changes to entry

            _context.Entry(projectToUpdate).CurrentValues.SetValues(project);

            // _context.Attach(project);
            // _context.Entry(project).Property("Name").IsModified = true;
            // _context.Entry(project).Property("Description").IsModified = true;
            return project;
        }

        public void AddProjectInvite(ProjectInvitation projectInvitation)
        {
            if(projectInvitation == null)
            {
                throw new ArgumentNullException(nameof(projectInvitation));
            }
            _context.Add(projectInvitation);

        }

        public void DeleteProject(int projectId)
        {
            var project = GetProjectById(projectId);
            _context.Projects.Remove(project);
        }



        public List<ProjectInvitation> GetProjectInvitations(int projectId)
        {
            var projectInvitations = _context.ProjectInvitations.Where(pi => pi.ProjectId == projectId).ToList();
            return projectInvitations;
        }

        public List<AppUser> GetProjectInvitees(int projectId)
        {
            var projectInvitations = _context.ProjectInvitations.Where(pi => pi.ProjectId == projectId).ToList();
            var users = new List<AppUser>();
            
            //store all the invitees
            for(int i = 0; i < projectInvitations.Count; i++)
            {
                users.Add(_context.AppUsers.Find(projectInvitations[i].InviteeId));
            }

            return users;
        }

        public bool DeleteProjectInvite(int projectId, string inviteeId)
        {
            var projectInvitation = _context.ProjectInvitations.Where(pi => pi.ProjectId == projectId && pi.InviteeId == inviteeId).ToList();
            if(projectInvitation.Count != 0)
            {
                _context.ProjectInvitations.Remove(projectInvitation[0]); 
                return true;
            }
            return false;
            
        }

        public bool HasUserBeenInvited(int projectId, string userId)
        {
            var userInvites = _context.ProjectInvitations.Where(pi => pi.ProjectId == projectId && pi.InviteeId == userId).ToList();
            if(userInvites.Count == 0)
            {
                return false;
            }
            return true;
            
        }

        public ProjectUser SetProjectUserRole(ProjectUser projectUser, string role)
        {

            if(projectUser == null) 
            {
                throw new ArgumentNullException(nameof(projectUser));
            }

            projectUser.Role = role;

            var projectUserToUpdate = _context.ProjectUsers.Find(projectUser.ProjectId, projectUser.AppUserId);
            // //make changes to entry

            _context.Entry(projectUserToUpdate).CurrentValues.SetValues(projectUser);
            
            // _context.Entry(projectUser).Property("Role").IsModified = true;
            return projectUser;
        }

        public Task CreateTask(Task task, string creatorId)
        {
            if(task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            //add the new task to the context
            _context.Add(task);

            //build a TaskUpdate model
            TaskUpdate taskUpdate = new TaskUpdate{
                Task = task,
                TimeStamp = DateTime.Now,
                UpdaterId = creatorId,
            };
            
            _context.Add(taskUpdate);

            return task;

        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public List<TaskComment> GetProjectTaskComments(int projectId)
        {
            //first, get all the tasks in a project
            var projectTasks = GetProjectTasks(projectId);

            //to store all the task comments for the project
            List<TaskComment> projectTaskComments = new List<TaskComment>();

            //loop through all the project tasks
            for(int i = 0; i < projectTasks.Count; i++)
            {
                //add all the comments for each task in the project
                projectTaskComments.AddRange(_context.TaskComments.Where(tc => tc.TaskId == projectTasks[i].TaskId).ToList());
            }

            return projectTaskComments;
        }

        public List<TaskComment> GetProjectTaskCommentsByUser(int projectId, string userId)
        {
            //first, get all the task comments in a project
            var projectTaskComments = GetProjectTaskComments(projectId);

            //to store all the task comments
            List<TaskComment> projectTaskCommentsByUser = new List<TaskComment>();

            //loop through all the task comments in the project
            for(int i = 0; i < projectTaskComments.Count; i++)
            {
                if(projectTaskComments[i].AppUserId == userId)
                {
                    projectTaskCommentsByUser.Add(projectTaskComments[i]);
                }
            }

            return projectTaskCommentsByUser;
        }

        public List<TaskUpdate> GetTaskUpdatesByUpdaterInProject(int projectId, string updaterId)
        {
            //first, get all the task updates in the project
            var projectTaskUpdates = GetTaskUpdatesByProject(projectId);

            //to store all the task updates
            List<TaskUpdate> projectTaskUpdatesByUser = new List<TaskUpdate>();

            //loop through all the task updates
            for(int i = 0; i < projectTaskUpdates.Count; i++)
            {
                if(projectTaskUpdates[i].UpdaterId == updaterId)
                {
                    projectTaskUpdatesByUser.Add(projectTaskUpdates[i]);
                }
            }

            return projectTaskUpdatesByUser;
        }

        public List<TaskUserUpdate> GetProjectTaskUserUpdates(int projectId)
        {
            //get all the tasks in the project
            var projectTasks = GetProjectTasks(projectId);

            //collect all the task user update records corresponding to the above tasks
            List<TaskUserUpdate> projectTaskUserUpdates = new List<TaskUserUpdate>();

            for(int i = 0; i < projectTasks.Count; i++)
            {
                projectTaskUserUpdates.AddRange(_context.TaskUserUpdates.Where(tuu => tuu.TaskId == projectTasks[i].TaskId).ToList());
            }

            return projectTaskUserUpdates;
        }

        public List<TaskUserUpdate> GetProjectTaskUserUpdatesByUpdater(int projectId, string updaterId)
        {
            //get all the task user updates in the project
            var projectTaskUserUpdates = GetProjectTaskUserUpdates(projectId);

            //to store all the task user updates
            List<TaskUserUpdate> projectTaskUpdatesByUpdater = new List<TaskUserUpdate>();

            //loop through all the task updates
            for(int i = 0; i < projectTaskUserUpdates.Count; i++)
            {
                if(projectTaskUserUpdates[i].UpdaterId == updaterId)
                {
                    projectTaskUpdatesByUpdater.Add(projectTaskUserUpdates[i]);
                }
            }

            return projectTaskUpdatesByUpdater;

        }
    }
}