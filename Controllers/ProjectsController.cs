using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data.MockRepositories;
using ProjectManager.Models;
using System.Linq;

namespace ProjectManager.Controllers 
{
    [Route("/api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly MockProjectsRepo _repository  =  new MockProjectsRepo();
        private readonly MockProjectUsersRepo _rep = new MockProjectUsersRepo();
        [HttpPost]
        public ActionResult <List<Project>> GetAllProjectsByUser(int userid)
        {
            var projects = new List<Project>
            {
                new Project{ProjectId=1, Name="Biplab's Senior Project", DateCreated="06/31/2020 09:43:32", Description="asdfaskjdf a sdf sf sdf ", DateTerminated=null, OrganizationId=1},
                new Project{ProjectId=2, Name="Business Startup", DateCreated="08/31/2020 09:43:32", Description="asdfaskjdf a sdf sf sdf ", DateTerminated=null, OrganizationId=2},
                new Project{ProjectId=3, Name="Indie Game Project", DateCreated="03/31/2020 09:43:32", Description="asdfaskjdf a sdf sf sdf ", DateTerminated=null}
            };
            return Ok(projects);

            
        }
        
        [HttpPost("{userid}/{projectid}")]
        public ActionResult <Project> GetUserProjectById(int userid, int projectid) 
        {
            var userProjects = _rep.GetProjectsByUser(userid);
            //find the project 
            for(var i = 0; i < userProjects.Count; i++) 
            {
                if(userProjects[i].ProjectId == projectid) {
                    return Ok(userProjects[i]);
                }
            }
            return BadRequest();
        }

    }
}