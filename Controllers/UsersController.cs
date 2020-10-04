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
        // private readonly IProjectsRepo _projectsRepo;

        //the manager for handling user creation, deletion, etc..
        private readonly UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager, IAppUsersRepo usersRepo)
        {
            _usersRepo = usersRepo;
            _userManager  = userManager;
        }

        [Authorize]
        [HttpGet("projects")]
        public async Task<IActionResult> GetUserProjects(string userid) 
        {
             //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userProjects = _usersRepo.GetUserProjects(user.Id);
            return Ok(userProjects);
        }

        

        // [HttpGet("{userid}/projects")]
        // public ActionResult <List<Project>> GetUserProjects(int userid) 
        // {
        //     var userProjects = _usersRepo.GetUserProjects(userid);
        //     return Ok(userProjects);
        // }

        

        

    }
}