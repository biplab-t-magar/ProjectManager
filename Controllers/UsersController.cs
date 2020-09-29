using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Controllers 
{
    [Route("/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _usersRepo;

        public UsersController( IUsersRepo usersRepo)
        {
            _usersRepo = usersRepo;
        }

        [HttpGet("{userid}/projects")]
        public ActionResult <List<Project>> GetUserProjects(int userid) 
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