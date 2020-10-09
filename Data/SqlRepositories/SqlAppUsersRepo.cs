/* SqlAppUsersRepo.cs
 This file contains the SqlAppUsersRepo class. The SqlAppUsersRepo class is an implementation of the IAppUsersRepo interface. It represents 
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
    public class SqlUsersRepo : IAppUsersRepo
    {
        //the database context
        private readonly ProjectManagerContext _context;

        /**/
        /*
        * NAME:
        *      SqlUsersRepo - constructor for SqlUsersRepo
        * SYNOPSIS:
                SqlUsersRepo(ProjectManagerContext context)
        *           context --> the database context that is injected into the class through dependency injection
        * DESCRIPTION:
                The constructor implements the SqlUsersRepo class, which represents an implementation of the IUsersRepo interface
                It initializes the _context member variable, which will be used by all the functions in this class for data access
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/02/2020 
        * /
        /**/
        public SqlUsersRepo(ProjectManagerContext context)
        {   
            _context = context;
        }

        /**/
        /*
        * NAME:
        *      CreateUserProject - creates a new project for the user, making him/her an administrator for the project
        * SYNOPSIS:
                CreateUserProject(Project project, AppUser user)
        *           project --> the Projectobject representing the project to be added 
                    user --> the object representing the user who is creating the project
        * DESCRIPTION:
                Accesses the database context in order to add a new Project entry to the database. It also adds a ProjectUser entry
                    that describes the relationship of the user the newly created project
        * RETURNS
                the Project object that was added
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/21/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetUserById - gets the AppUser entry associated with the given id
        * SYNOPSIS:
                GetUserById(string userId)
        *           userId --> the id of the user whose entry is to be returned
        * DESCRIPTION:
                Accesses the database context in order to find and return the user entry with the given id
        * RETURNS
                the User object with the given id
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/06/2020 
        * /
        /**/
        public AppUser GetUserById(string userId)
        {
            return _context.AppUsers.Find(userId);
        }

        /**/
        /*
        * NAME:
        *      GetUserByUserName - gets the AppUser entry associated with the given username
        * SYNOPSIS:
                GetUserByUserName(string userName)
        *           userName --> the username of the user whose entry is to be returned
        * DESCRIPTION:
                Accesses the database context in order to find and return the user entry with the given username
        * RETURNS
                the User object with the given user name
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/06/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetUserProjectInvitations - gets the list of all ProjectInvitation entries where the given user is an invitee
        * SYNOPSIS:
                GetUserProjectInvitations(string userId)
        *           userId --> the id of the user whose project invitations are to be returned
        * DESCRIPTION:
                Accesses the database context in order to find and return the list of all project invitation where the given user is an invitee
        * RETURNS
                the list of all ProjectInvitation objects where the given user is an invitee
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/21/2020 
        * /
        /**/
        public List<ProjectInvitation> GetUserProjectInvitations(string userId)
        {
            return _context.ProjectInvitations.Where(pi => pi.InviteeId == userId).ToList();
        }

        /**/
        /*
        * NAME:
        *      GetUserProjectInviters - gets the list of all users who have invited the given user to projects
        * SYNOPSIS:
                GetUserProjectInviters(string userId)
        *           userId --> the id of the user whose project invitaters are to be returned
        * DESCRIPTION:
                Accesses the database context in order to find and return the list of all users who have invited the given user is a project
        * RETURNS
                the list of all AppUser objects who have invited the given user to a project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/21/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetUserProjects - gets the list of all project associated with a user
        * SYNOPSIS:
                GetUserProjects(string userId)
        *           userId --> the id of the user whose projects are to be returned
        * DESCRIPTION:
                Accesses the database context in order to find and return the list of all projects a user is involved in
        * RETURNS
                the list of all Project objects associated with a user
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/21/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetUserProjectsInvitedTo - gets the list of all projects a user has been invited to
        * SYNOPSIS:
                GetUserProjectsInvitedTo(string userId)
        *           userId --> the id of the user
        * DESCRIPTION:
                Accesses the database context in order to find and return the list of all projects to which the given user
                    has been invited to
        * RETURNS
                the list of all Project objects a user has been invited to
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/21/2020 
        * /
        /**/
        public List<Project> GetUserProjectsInvitedTo(string userId)
        {
            //first, get all the project invitations for the user
            var projectInvitations = _context.ProjectInvitations.Where(pi => pi.InviteeId == userId).ToList();

            List<Project> projectsInvitedTo = new List<Project>();

            //now, collect a list of all the Project objects corresponding to the project invitations
            for(int i = 0; i < projectInvitations.Count; i++)
            {
                projectsInvitedTo.Add(_context.Projects.Find(projectInvitations[i].ProjectId));
            }

            return projectsInvitedTo;
        }

        /**/
        /*
        * NAME:
        *      GetUserTasks - gets the list of all tasks assigned to a user
        * SYNOPSIS:
                GetUserTasks(string userId)
        *           userId --> the id of the user
        * DESCRIPTION:
                Accesses the database context in order to find and return the list of all the tasks to which a user has been assigned
        * RETURNS
                the list of all Task objects a user has been assigned to
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/06/2020 
        * /
        /**/
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


        /**/
        /*
        * NAME:
        *      UpdateUser - updates a AppUser entry in the database using the given  App User object
        * SYNOPSIS:
                UpdateUser(AppUser user)
        *           user --> the AppUser object with the updated attributes
        * DESCRIPTION:
                Accesses the database context in order to replace the old AppUser entry with the updated one
        * RETURNS
                the updated AppUser entry
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/29/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetUserRoleInProject - gets the role of a user in a project
        * SYNOPSIS:
                GetUserRoleInProject(string userId, int projectId)
        *           userId --> the id of the user 
                    projectId --> the id of the project
        * DESCRIPTION:
                Accesses the database context in order to get the ProjectUser entry for the given project and user and returns it
        * RETURNS
                the ProjectUser object representing the project-relationship
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/29/2020 
        * /
        /**/
        public ProjectUser GetUserRoleInProject(string userId, int projectId)
        {
            return _context.ProjectUsers.Find(projectId, userId);
        }


        /**/
        /*
        * NAME:
        *      GetCommentsByUser - gets the list of all the comments by a user
        * SYNOPSIS:
                GetCommentsByUser(string userId)
        *           userId --> the id of the user 
        * DESCRIPTION:
                Accesses the database context in order to get the all the TaskComment entries associated with the given user
        * RETURNS
                the list of all TaskComment objects associated with the given user
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/04/2020 
        * /
        /**/       
        public List<TaskComment> GetCommentsByUser(string userId)
        {
            return _context.TaskComments.Where(tc => tc.AppUserId == userId).ToList();
        }

        /**/
        /*
        * NAME:
        *      GetTaskUpdatesByUpdater - gets the list of all the TaskUpdate entries where the given user is the updater
        * SYNOPSIS:
                GetTaskUpdatesByUpdater(string updaterId)
        *           updaterId --> the id of the user who carried out the task updates
        * DESCRIPTION:
                Accesses the database context in order to get the all the TaskUpdate entries associated with the given user
        * RETURNS
                the list of all TaskUpdate objects where the given user is the updater
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/04/2020 
        * /
        /**/  
        public List<TaskUpdate> GetTaskUpdatesByUpdater(string updaterId)
        {
            return _context.TaskUpdates.Where(tu => tu.UpdaterId == updaterId).ToList();
        }

        /**/
        /*
        * NAME:
        *      GetTaskUserUpdatesByUpdater - gets the list of all the TaskUserUpdate entries where the given user is the updater
        * SYNOPSIS:
                GetTaskUserUpdatesByUpdater(string updaterId)
        *           updaterId --> the id of the user who carried out the task user updates
        * DESCRIPTION:
                Accesses the database context in order to get the all the TaskUserUpdate entries associated with the given user
        * RETURNS
                the list of all TaskUserUpdate objects where the given user is the updater
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/04/2020 
        * /
        /**/ 
        public List<TaskUserUpdate> GetTaskUserUpdatesByUpdater(string updaterId)
        {
            return _context.TaskUserUpdates.Where(tuu => tuu.UpdaterId == updaterId).ToList();
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
            //save all the changes to the database
            return _context.SaveChanges() >= 0;
        }

    }
}