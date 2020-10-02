using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Controllers 
{
    [Route("/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAppUsersRepo _usersRepo;

        public UsersController( IAppUsersRepo usersRepo)
        {
            _usersRepo = usersRepo;
        }

        // [Authorize]
        [HttpGet("{userid}/projects")]
        public ActionResult <List<Project>> GetUserProjects(string userid) 
        {
            var userProjects = _usersRepo.GetUserProjects(userid);
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