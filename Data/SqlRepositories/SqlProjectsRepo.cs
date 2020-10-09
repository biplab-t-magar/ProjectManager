/* SqlProjectsRepo.cs
 This file contains the SqlProjectsRepo class. The SqlProjectsRepo class is an implementation of the IProjectsRepo interface. It represents 
 an implementation of the interface by using an SQL database to store and retrieve data. So this repository class communicates with an SQL database
 (a PostgreSQL database, specifically),while providing all the functions to retrieve and manipulate the entries in the database
 that are listed in the interface it implements.
*/

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

        /**/
        /*
        * NAME:
        *      SqlProjectsRepo - constructor for SqlProjectsRepo
        * SYNOPSIS:
                SqlProjectsRepo(ProjectManagerContext context)
        *           context --> the database context that is injected into the class through dependency injection
        * DESCRIPTION:
                The constructor implements the SqlProjectsRepo class, which represents an implementation of the IProjectsRepo interface
                It initializes the _context member variable, which will be used by all the functions in this class for data access
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/02/2020 
        * /
        /**/
        public SqlProjectsRepo(ProjectManagerContext context)
        {   
            _context = context;
        }


        /**/
        /*
        * NAME:
        *      GetProjectById - gets the Project object with the given id
        * SYNOPSIS:
                GetProjectById(int projectId)
        *           projectId --> the id of the project to be returned
        * DESCRIPTION:
                Accesses the database context in order to return the project of the given id
        * RETURNS
                The Project object with the given id
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/02/2020 
        * /
        /**/
        public Project GetProjectById(int projectId)
        {
            return _context.Projects.Find(projectId);

        }

        /**/
        /*
        * NAME:
        *      GetProjectTasks - gets the list of Task objects associated with the given project
        * SYNOPSIS:
                GetProjectTasks(int projectId)
        *           projectId --> the id of the project whose tasks are to be returned
        * DESCRIPTION:
                Accesses the database context in order to return the list of Task object associated with the given project
        * RETURNS
                The list of Task objects associated with the given project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/02/2020 
        * /
        /**/
        public List<Task> GetProjectTasks(int projectId)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId).ToList();
        }

        /**/
        /*
        * NAME:
        *      GetProjectTasksByTaskType - gets the list of tasks associated with the given project and of the given task type
        * SYNOPSIS:
                GetProjectTasksByTaskType(int projectId, int taskTypeId)
        *           projectId --> the id of the project whose tasks are to be returned
                    taskTypeId --> the id of the task type 
        * DESCRIPTION:
                Accesses the database context in order to return the list of Task objects associated with the given project and of the given task type
        * RETURNS
                The list of Task objects associated with the given project and of the given task type
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/10/2020 
        * /
        /**/
        public List<Task> GetProjectTasksByTaskType(int projectId, int taskTypeId)
        {
            var tasks = _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskTypeId == taskTypeId);
            if(tasks == null) {
                return new List<Task>();
            } else {
                return tasks.ToList();
            }
        }

        /**/
        /*
        * NAME:
        *      GetProjectTaskTypes - gets the list of task types of a project
        * SYNOPSIS:
                GetProjectTaskTypes(int projectId)
        *           projectId --> the id of the project whose task types are to be returned
        * DESCRIPTION:
                Accesses the database context in order to return the list of Task types of a project
        * RETURNS
                The list of TaskType objects of a project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/10/2020 
        * /
        /**/
        public List<TaskType> GetProjectTaskTypes(int projectId)
        {
            return _context.TaskTypes.Where(tt => tt.ProjectId == projectId).ToList();
        }

        /**/
        /*
        * NAME:
        *      GetProjectUsers - gets the list of all the users in the project
        * SYNOPSIS:
                GetProjectUsers(int projectId)
        *           projectId --> the id of the project whose list of users is to be returned
        * DESCRIPTION:
                Accesses the database context in order to return the list of users in the given project
        * RETURNS
                The list of AppUser objects of a project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/10/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetProjectUserRoles - gets the list of all the ProjectUser entries asociated with a project
        * SYNOPSIS:
                GetProjectUserRoles(int projectId)
        *           projectId --> the id of the project whose list of project-users relationships is to be returned
        * DESCRIPTION:
                Accesses the database context in order to return the list of ProjectUsers entries associated with the given projet
        * RETURNS
                The list of ProjectUser objects of a project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/10/2020 
        * /
        /**/
        public List<ProjectUser> GetProjectUserRoles(int projectId)
        {
            var projectUsers = _context.ProjectUsers.Where(pu => pu.ProjectId == projectId).ToList();
            

            return projectUsers;

        }

        /**/
        /*
        * NAME:
        *      GetTaskUpdatesByProject - gets the list of all the TaskUpdate entries asociated with a project
        * SYNOPSIS:
                GetTaskUpdatesByProject(int projectId)
        *           projectId --> the id of the project whose task updates are to be returned
        * DESCRIPTION:
                Accesses the database context in order to return the list of TaskUpdate entries associated with the given projet
        * RETURNS
                The list of TaskUpdate objects of a project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/10/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetUserProjectTasks - gets the list of all the tasks assigned to a user in the given project
        * SYNOPSIS:
                GetUserProjectTasks(int projectId, string userId)
        *           projectId --> the id of the project whose user tasks are to be returned
                    userId --> the id of the user whose tasks in the project are to be returned
        * DESCRIPTION:
                Accesses the database context in order to return the list of Task entries associated with the given project
        * RETURNS
                The list of TaskUpdate objects of a project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/10/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      AddUserToProject - adds a user to a project
        * SYNOPSIS:
                AddUserToProject(ProjectUser projectUser)
        *           projectUser --> the ProjectUser object that represents the project-user relationship
        * DESCRIPTION:
                Accesses the database context in order to add the ProjectUser entry representing the user's 
                    membership in the project
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/10/2020 
        * /
        /**/
        public void AddUserToProject(ProjectUser projectUser)
        {
            if(projectUser == null) 
            {
                throw new ArgumentNullException(nameof(projectUser));
            }
            
            _context.Add(projectUser);
        }

        /**/
        /*
        * NAME:
        *      UpdateProject - update a Project entry in the database
        * SYNOPSIS:
                UpdateProject(Project project)
        *           project --> the updated Project object 
        * DESCRIPTION:
                Accesses the database context in order to find and update the Project entry whose projectId attribute 
                    matches the id in the given Project object. The attribute values in the new Project object replaces 
                    the previous attribute values in the database
        * RETURNS
                The object representing the updated project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/10/2020 
        * /
        /**/
        public Project UpdateProject(Project project)
        {
            if(project == null) 
            {
                throw new ArgumentNullException(nameof(project));
            }


            var projectToUpdate = _context.Projects.Find(project.ProjectId);
            // //make changes to entry

            _context.Entry(projectToUpdate).CurrentValues.SetValues(project);

            return project;
        }

        /**/
        /*
        * NAME:
        *      AddProjectInvite - adds a ProjectInvitatoin object to the database
        * SYNOPSIS:
                AddProjectInvite(ProjectInvitation projectInvitation)
        *           projectInvitation --> the projectInvitation object to be added to the database as an entry
        * DESCRIPTION:
                Accesses the database context in order to add the given ProjectInvitation entry
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/10/2020 
        * /
        /**/
        public void AddProjectInvite(ProjectInvitation projectInvitation)
        {
            if(projectInvitation == null)
            {
                throw new ArgumentNullException(nameof(projectInvitation));
            }
            _context.Add(projectInvitation);

        }

        /**/
        /*
        * NAME:
        *      DeleteProject - deletes a Project entry from the database
        * SYNOPSIS:
                DeleteProject(int projectId)
        *           projectId --> the id of the project to be deleted
        * DESCRIPTION:
                Accesses the database context in order to remove the Project entry associated with the given projectId
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/10/2020 
        * /
        /**/
        public void DeleteProject(int projectId)
        {
            var project = GetProjectById(projectId);
            _context.Projects.Remove(project);
        }


        /**/
        /*
        * NAME:
        *      GetProjectInvitations - gets the list of all the proejct invitations associated with a project
        * SYNOPSIS:
                GetProjectInvitations(int projectId)
        *           projectId --> the id of the project to be deleted
        * DESCRIPTION:
                Accesses the database context in order to find and return the list of all ProjectInvitation entries associated with the project
        * RETURNS
                A list of all the ProjectInvitaton objects associated with the given project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/14/2020 
        * /
        /**/
        public List<ProjectInvitation> GetProjectInvitations(int projectId)
        {
            var projectInvitations = _context.ProjectInvitations.Where(pi => pi.ProjectId == projectId).ToList();
            return projectInvitations;
        }

        /**/
        /*
        * NAME:
        *      GetProjectInvitees - gets the list of all the proejct invitees associated with a project
        * SYNOPSIS:
                GetProjectInvitees(int projectId)
        *           projectId --> the id of the project whose invitees are to be returned
        * DESCRIPTION:
                Accesses the database context in order to find and return all the invitees associated with the given project
        * RETURNS
                A list of all the ProjectInvitaton objects associated with the given project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/14/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      DeleteProjectInvite - deletes a project invite send to a user
        * SYNOPSIS:
                DeleteProjectInvite(int projectId, string inviteeId)
        *           projectId --> the id of the project 
                    inviteeId --> the id of the user who was invited
        * DESCRIPTION:
                Accesses the database context in order to delete the ProjectInvitation entry for the project and the user
        * RETURNS
                True if deletion was successful, false otherwise
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/14/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      HasUserBeenInvited - checks whether a user has already been invited to a project
        * SYNOPSIS:
                HasUserBeenInvited(int projectId, string userId)
        *           projectId --> the id of the project 
                    inviteeId --> the id of the user 
        * DESCRIPTION:
                Accesses the database context in order to find out whether a ProjectInvitation entry exists for the user and the project
        * RETURNS
                true if a ProjectInvitation entry exists, false otherwise
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/15/2020 
        * /
        /**/
        public bool HasUserBeenInvited(int projectId, string userId)
        {
            var userInvites = _context.ProjectInvitations.Where(pi => pi.ProjectId == projectId && pi.InviteeId == userId).ToList();
            if(userInvites.Count == 0)
            {
                return false;
            }
            return true;
            
        }

        /**/
        /*
        * NAME:
        *      SetProjectUserRole - sets the role of a project user to the given value
        * SYNOPSIS:
                SetProjectUserRole(ProjectUser projectUser, string role)
        *           projectUser --> the ProjectUser entry representing the relationship between a user and the project
                    role --> the role of the user in the project
        * DESCRIPTION:
                Accesses the database context in order to set the role of a user in a project to the specified role
        * RETURNS
                the ProjectUser entry with the updated role
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/15/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      CreateTask - adds a task entry to the database
        * SYNOPSIS:
                CreateTask(Task task, string creatorId)
        *           task --> the task to be added 
                    creatorId --> the id of the user who created the task
        * DESCRIPTION:
                Accesses the database context in order to add a new task entry. It also adds a TaskUpdate object
                    that describes the creation of the Task, so the web application can keep track of all the created tasks
        * RETURNS
                the Task object that was added
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/15/2020 
        * /
        /**/
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

        
        /**/
        /*
        * NAME:
        *      GetProjectTaskComments - gets all the task comments associated with a project
        * SYNOPSIS:
                GetProjectTaskComments(int projectId)
        *           projectId --> the id of the project whose task comments are to be returned
        * DESCRIPTION:
                Accesses the database context in order to find and return all the TaskComment entries in the database
                    associated with the given projecct
        * RETURNS
                the list of TaskComment objects associated with the given project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/18/2020 
        * /
        /**/
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

         /**/
        /*
        * NAME:
        *      GetProjectTaskCommentsByUser - gets all the task comments by a user in a project
        * SYNOPSIS:
                GetProjectTaskCommentsByUser(int projectId, string userId)
        *           projectId --> the id of the project whose user's task comments are to be returned
                    userId --> the id of the user whose task comments in the project are to be returned
        * DESCRIPTION:
                Accesses the database context in order to find and return all the TaskComment entries in the database
                    associated with the given project and the given user
        * RETURNS
                the list of TaskComment objects associated with the given project and user
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/18/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetTaskUpdatesByUpdaterInProject - gets all the task updates instigated by a user in a project
        * SYNOPSIS:
                GetTaskUpdatesByUpdaterInProject(int projectId, string userId)
        *           projectId --> the id of the relevant project
                    updaterId --> the id of the user who carried out the updates
        * DESCRIPTION:
                Accesses the database context in order to find and return all the task updates by the specified user
                    in the specified project
        * RETURNS
                the list of TaskUpdate objects associated with the given user and the given project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/18/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetProjectTaskUserUpdates - gets all the task user updates in a project
        * SYNOPSIS:
                GetProjectTaskUserUpdates(int projectId)
        *           projectId --> the id of the relevant project
        * DESCRIPTION:
                Accesses the database context in order to find and return all the task user updated associated with the given
                    project
        * RETURNS
                the list of TaskUserUpdate objects associated with the given project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/18/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetProjectTaskUserUpdatesByUpdater - gets all the task user updates instigated by a user in a project
        * SYNOPSIS:
                GetProjectTaskUserUpdatesByUpdater(int projectId, string updaterId)
        *           projectId --> the id of the relevant project
                    updaterId --> the id of the user who carried out the updates
        * DESCRIPTION:
                Accesses the database context in order to find and return all the task user updates by the specified user
                    in the specified project
        * RETURNS
                the list of TaskUserUpdate objects associated with the given user and the given project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/18/2020 
        * /
        /**/
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