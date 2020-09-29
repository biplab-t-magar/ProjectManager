using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Models;
using System.Linq;
using ProjectManager.Data.Interfaces;
using System;

namespace ProjectManager.Controllers 
{
    [Route("/project")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepo _projectsRepo;

        public ProjectsController(IProjectsRepo projectsRepo)
        {
            _projectsRepo = projectsRepo;
        }

        [HttpGet("{projectId}")]
        public ActionResult <Project> GetProject(int projectId)
        {
            var project = _projectsRepo.GetProjectById(projectId);
            if(project == null) 
            {
                return NotFound();
            }

            return Ok(project);            
        }

        [HttpGet("{projectId}/users")]
        public ActionResult <List<User>> GetProjectUsers(int projectId)
        {
            var projectUsers = _projectsRepo.GetProjectUsers(projectId);
            return Ok(projectUsers);
        }
        
        [HttpGet("{projectId}/roles")]
        public ActionResult <List<ProjectUser>> GetProjectUserRoles(int projectId)
        {
            var projectUserRoles = _projectsRepo.GetProjectUserRoles(projectId);
            return projectUserRoles;
        }

    }
}