/* UsersController.cs
 This file contains the UsersController class, which represents one of the three controller classes in the ProjectManager project.
 A controller class in ASP.NET Core is responsible for defining and handling HTTP requests to specific routes. The UsersController class handles
 routes and HTTP requests relating to everything specific to User in the Project Manager web application
*/
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data.Interfaces;
using ProjectManager.Data.Services;
using ProjectManager.Models;
using ProjectManager.Models.UtilityModels;

namespace ProjectManager.Controllers 
{
    [Route("/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //data repositories
        private readonly IAppUsersRepo _usersRepo;
        private readonly IProjectsRepo _projectsRepo;


        //the manager for handling user creation, deletion, etc..
        private readonly UserManager<AppUser> _userManager;
        //for the generation of user activites
        private readonly ProjectActivity _projectActivity;

        /**/
        /*
        * NAME:
        *      UsersController - constructor for the UsersController class
        * SYNOPSIS:
                UsersController(IAppUsersRepo usersRepo, IProjectsRepo projectsRepo, UserManager userManager, ProjectActivity projectActivity)
        *           usersRepo --> the ProjectManager application users repository that is injected as a dependency injection
                    projectsRepo --> the ProjectManager application projects repository that is injected as a dependency injection
                    userManager --> an instance of the UserManager class, which is a class provided by Asp.Net Core Identity to handle
                                    use registration and retrieval
                    projectActivity --> an instance of the ProjectActivity class that is injected as a dependency injection
        * DESCRIPTION:
                Initializes the UsersController class
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/06/2020 
        * /
        /**/
        public UsersController(UserManager<AppUser> userManager, IAppUsersRepo usersRepo, IProjectsRepo projectsRepo, ProjectActivity projectActivity )
        {
            _usersRepo = usersRepo;
            _projectsRepo = projectsRepo;
            _userManager  = userManager;
            _projectActivity = projectActivity;
        }

        /**/
        /*
        * NAME:
        *      GetUser - In response to a Get request, it returns an HTTP response with the info of the user who is currently using the web application
        * SYNOPSIS:
                GetUser()
        * DESCRIPTION:
                Figures out the identity of the current user and teturns an HTTP response with the info on the user
        * RETURNS
                an HTTP response containing the info on the user
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/24/2020 
        * /
        /**/
        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetUser()
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return Ok(user);
        }

        /**/
        /*
        * NAME:
        *      UpdateUserInfo - Accepts a Post request to update the information of a particular user
        * SYNOPSIS:
                UpdateUserInfo(UpdateUserModel userModel)
                    userModel --> contains the information on the user to be updated and the updated values
        * DESCRIPTION:
                Accepts a Post request to update the information of a particular user
        * RETURNS
                an HTTP response containing the info on the updateduser
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/30/2020 
        * /
        /**/
        [Authorize]
        [HttpPost("update-info")]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserModel userModel)
        {
            //get the current user object
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //update user values
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Bio = userModel.Bio;

            user = _usersRepo.UpdateUser(user);
            _usersRepo.SaveChanges();

            return Ok(user);
        }

        /**/
        /*
        * NAME:
        *      GetUserById - Accepts a GET request to return the information of a particular user
        * SYNOPSIS:
                GetUserById(string userId)
                    userId --> the id of the user whose information is to be sent to the client
        * DESCRIPTION:
                Accepts a GET request to retrieve the information on the user with the given id
        * RETURNS
                an HTTP response containing the info on the user with the given id

        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/30/2020 
        * /
        /**/
        [Authorize]
        [HttpGet("{userId}")]
        public IActionResult GetUserById(string userId)
        {
            var user = _usersRepo.GetUserById(userId);

            return Ok(user);
        }

        /**/
        /*
        * NAME:
        *      GetUserRoleInProject 
        * SYNOPSIS:
                GetUserRoleInProject(int project)
                    projectId --> the id of the project
        * DESCRIPTION:
                Accepts a GET request to return the information on the currently signed-in user's role in a project
        * RETURNS
                an HTTP response containing the info on the project-user relationship
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/30/2020 
        * /
        /**/
        [Authorize]
        [HttpGet("{projectId}/user-role")]
        public async Task<IActionResult> GetUserRoleInProject(int projectId)
        {
            //get the current user object
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var projectUser = _usersRepo.GetUserRoleInProject(user.Id, projectId);
            if(projectUser == null)
            {
                return BadRequest("User is not a part of project");
            }

            return Ok(projectUser);

        }

        /**/
        /*
        * NAME:
        *      GetUserProjects
        * SYNOPSIS:
                GetUserProjects()
        * DESCRIPTION:
                Accepts a GET request to return info on all the projects the currently sign-in user is a part of
        * RETURNS
                an HTTP response containing the info on the all the user's projects
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/24/2020 
        * /
        /**/
        [Authorize]
        [HttpGet("projects")]
        public async Task<IActionResult> GetUserProjects() 
        {
             //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userProjects = _usersRepo.GetUserProjects(user.Id);

            return Ok(userProjects);
        }

        /**/
        /*
        * NAME:
        *      GetUserTasks
        * SYNOPSIS:
                GetUserTasks()
        * DESCRIPTION:
                Accepts a GET request to return info on all the tasks the currently sign-in user is assigned to
        * RETURNS
                an HTTP response containing the info on the all the user's tasks
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/24/2020 
        * /
        /**/
        [Authorize]
        [HttpGet("tasks")]
        public async Task<IActionResult> GetUserTasks() 
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userTasks = _usersRepo.GetUserTasks(user.Id);

            //reverse the order so the new tasks are at the top
            userTasks.Reverse();

            return Ok(userTasks);
        }
        
        /**/
        /*
        * NAME:
        *      GetUserProjectInvitations
        * SYNOPSIS:
                GetUserProjectInvitations()
        * DESCRIPTION:
                Accepts a GET request to return info on all the project invitations for the currently sign-in user
        * RETURNS
                an HTTP response containing the info on the all the user's project invitations
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/27/2020 
        * /
        /**/
        [Authorize]
        [HttpGet("project-invitations")]
        public async Task<IActionResult> GetUserProjectInvitations()
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            var projectInvitations = _usersRepo.GetUserProjectInvitations(user.Id);

            return Ok(projectInvitations);
        }

        /**/
        /*
        * NAME:
        *      GetUserProjectInviters
        * SYNOPSIS:
                GetUserProjectInviters()
        * DESCRIPTION:
                Accepts a GET request to return info on all the users who have invited the signed in user to projects
        * RETURNS
                an HTTP response containing the info on all the user's project inviters
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/27/2020 
        * /
        /**/
        [Authorize]
        [HttpGet("project-inviters")]
        public async Task<IActionResult> GetUserProjectInviters()
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            var projectInviters = _usersRepo.GetUserProjectInviters(user.Id);

            return Ok(projectInviters);
        }

        /**/
        /*
        * NAME:
        *      GetUserProjectsInvitedTo
        * SYNOPSIS:
                GetUserProjectsInvitedTo()
        * DESCRIPTION:
                Accepts a GET request to return info on all the projects the signed in user has been invited to
        * RETURNS
                an HTTP response containing the info on all the projects the user has been invited to
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/27/2020 
        * /
        /**/
        [Authorize]
        [HttpGet("projects-invited-to")]
        public async Task<IActionResult> GetUserProjectsInvitedTo()
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            var projectsInvitedTo = _usersRepo.GetUserProjectsInvitedTo(user.Id);

            return Ok(projectsInvitedTo);
        }

        /**/
        /*
        * NAME:
        *      AcceptInvitation
        * SYNOPSIS:
                AcceptInvitation(ProjectInvitation projectInvitation)
                    projectInvitation --> the invitation to be accepted
        * DESCRIPTION:
                Accepts a POST request and handles the acceptance of the given project invitation by the signed-in user
        * RETURNS
                an HTTP response containing the newly formed ProjectUser entry
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/29/2020 
        * /
        /**/
        [Authorize]
        [HttpPost("accept-invite")]
        public async Task<IActionResult> AcceptInvitation(ProjectInvitation projectInvitation)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the invitee in the invitation is the current user
            if(projectInvitation.InviteeId != user.Id)
            {
                return Unauthorized("Cannot accept invitations if requester is not the invitee");
            }

            //create a project user object
            ProjectUser projectUser = new ProjectUser();
            projectUser.ProjectId = projectInvitation.ProjectId;
            projectUser.AppUserId = projectInvitation.InviteeId;
            projectUser.Role = "Member";
            projectUser.TimeAdded = DateTime.Now;

            _projectsRepo.AddUserToProject(projectUser);
            _projectsRepo.DeleteProjectInvite(projectInvitation.ProjectId, projectInvitation.InviteeId);
            _projectsRepo.SaveChanges();

            return Ok(projectUser);
        }

        /**/
        /*
        * NAME:
        *      DeclineInvitation
        * SYNOPSIS:
                DeclineInvitation(ProjectInvitation projectInvitation)
                    projectInvitation --> the invitation to be declined
        * DESCRIPTION:
                Accepts a POST request and handles the rejection of the given project invitation by the signed-in user
        * RETURNS
                an HTTP response status 200 response
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/29/2020 
        * /
        /**/
        [Authorize]
        [HttpPost("decline-invite")]
        public async Task<IActionResult> DeclineInvitation(ProjectInvitation projectInvitation)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the invitee in the invitation is the current user
            if(projectInvitation.InviteeId != user.Id)
            {
                return Unauthorized("Cannot decline invitations if requester is not the invitee");
            }
            
            _projectsRepo.DeleteProjectInvite(projectInvitation.ProjectId, projectInvitation.InviteeId);
            _projectsRepo.SaveChanges();

            return Ok();
        }

        /**/
        /*
        * NAME:
        *      GetUserActivity
        * SYNOPSIS:
                GetUserActivity(string userId)
                    userId --> the id of the user who activity is to be returned
        * DESCRIPTION:
                Accepts a GET request and returns a list of all of the signed-in user's activities
        * RETURNS
                an HTTP response containing the list of all the user's activities
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/06/2020 
        * /
        /**/
        [Authorize]
        [HttpGet("{userId}/activity")]
        public async Task<IActionResult> GetUserActivity(string userId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return BadRequest("User is not logged in");
            }
            
            var userActivities = _projectActivity.GenerateUserActivity(userId);

            if(userActivities == null)
            {
                return NotFound();
            }
            
            return Ok(userActivities);

        }

        /**/
        /*
        * NAME:
        *      GetRecentUserActivity
        * SYNOPSIS:
                GetRecentUserActivity(string userId, int num)
                    userId --> the id of the user who activity is to be returned
                    num --> the number of recent activities to be returned
        * DESCRIPTION:
                Accepts a GET request and returns a list of the signed-in user's recent activities
        * RETURNS
                an HTTP response containing the list of the user's recent activities
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/06/2020 
        * /
        /**/
        [Authorize]
        [HttpGet("{userId}/activity/{num}")]
        public async Task<IActionResult> GetRecentUserActivity(string userId, int num)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return BadRequest("User is not logged in");
            }
            
            var userActivities = _projectActivity.GenerateUserActivity(userId);

            if(userActivities == null)
            {
                return NotFound();
            }

            if(userActivities.Count <= num) 
            {
                return Ok(userActivities);
            } 
            else
            {
                //take only as many entries as specified in numOfTasks
                userActivities = userActivities.GetRange(0, num);
                return Ok(userActivities); 
            }

        }

    }
}