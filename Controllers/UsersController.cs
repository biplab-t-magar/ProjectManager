using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Controllers 
{
    [Route("/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //data repositories
        private readonly IAppUsersRepo _usersRepo;
        private readonly IProjectsRepo _projectsRepo;

        // private readonly IProjectsRepo _projectsRepo;

        //the manager for handling user creation, deletion, etc..
        private readonly UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager, IAppUsersRepo usersRepo, IProjectsRepo projectsRepo)
        {
            _usersRepo = usersRepo;
            _projectsRepo = projectsRepo;
            _userManager  = userManager;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetUser()
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public IActionResult GetUserById(string userId)
        {
            var user = _usersRepo.GetUserById(userId);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("projects")]
        public async Task<IActionResult> GetUserProjects() 
        {
             //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userProjects = _usersRepo.GetUserProjects(user.Id);

            return Ok(userProjects);
        }

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
        
        [Authorize]
        [HttpGet("project-invitations")]
        public async Task<IActionResult> GetUserProjectInvitations()
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            var projectInvitations = _usersRepo.GetUserProjectInvitations(user.Id);

            return Ok(projectInvitations);
        }

        [Authorize]
        [HttpGet("project-inviters")]
        public async Task<IActionResult> GetUserProjectInviters()
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            var projectInviters = _usersRepo.GetUserProjectInviters(user.Id);

            return Ok(projectInviters);
        }

        [Authorize]
        [HttpGet("projects-invited-to")]
        public async Task<IActionResult> GetUserProjectsInvitedTo()
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            var projectsInvitedTo = _usersRepo.GetUserProjectsInvitedTo(user.Id);

            return Ok(projectsInvitedTo);
        }

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

            return Ok();
        }

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

        

    }
}