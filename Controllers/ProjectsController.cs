using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Models;
using System.Linq;
using ProjectManager.Data.Interfaces;

namespace ProjectManager.Controllers 
{
    [Route("/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepo _repository;

        public ProjectsController(IProjectsRepo repository)
        {
            _repository = repository;
        }


        [HttpPost("{id}")]
        public ActionResult <Project> GetProjectById(int projectId)
        {
            var project = _repository.GetProjectById(projectId);

            
            return Ok(project);

            
        }
        
        // [HttpPost("{userid}/{projectid}")]
        // public ActionResult <Project> GetProjectsByUser(int projectid, int userid) 
        // {
        //     var userProjects = _rep.GetProjectsByUser(userid);
        //     //find the project 
        //     for(var i = 0; i < userProjects.Count; i++) 
        //     {
        //         if(userProjects[i].ProjectId == projectid) {
        //             return Ok(userProjects[i]);
        //         }
        //     }
        //     return BadRequest();
        // }

    }
}