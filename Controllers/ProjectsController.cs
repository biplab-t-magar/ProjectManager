using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Models;
using ProjectManager.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ProjectManager.ProjectManagarUtilities.UtilityModels;

namespace ProjectManager.Controllers 
{
    [Route("/project")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepo _projectsRepo;
        private readonly IAppUsersRepo _usersRepo;
        private readonly UserManager<AppUser> _userManager;

        public ProjectsController(IProjectsRepo projectsRepo, IAppUsersRepo usersRepo, UserManager<AppUser> userManager)
        {
            _projectsRepo = projectsRepo;
            _usersRepo = usersRepo;
            _userManager = userManager;
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
        public ActionResult <List<AppUser>> GetProjectUsers(int projectId)
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

        [HttpGet("{projectId}/tasks")]
        public ActionResult <List<Task>> GetProjectTasks(int projectId)
        {
            var projectTasks = _projectsRepo.GetProjectTasks(projectId);
            return projectTasks;
        }

        
        [HttpGet("{projectId}/tasks/{numOfTasks:int}")]
        public ActionResult <List<Task>> GetProjectTasks(int projectId, [FromQuery(Name = "numOfTasks")] int numOfTasks)
        {
            var projectTasks = _projectsRepo.GetProjectTasks(projectId);
            return projectTasks;
        }

        [HttpGet("{projectId}/taskTypes")]
        public ActionResult <List<TaskType>> GetProjectTaskTypes(int projectId)
        {
            var projectTaskTypes = _projectsRepo.GetProjectTaskTypes(projectId);
            return projectTaskTypes;
        }

        [Authorize]
        [HttpPost("new")]
        public async System.Threading.Tasks.Task<IActionResult> CreateProject([FromBody]UtilityProjectModel projectModel)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            //convert from a UtilityProjectModel to a projectModel object
            var project = new Project();

            project.Name = projectModel.name;
            project.Description = projectModel.description;

            //create a new project for the user
            project = _usersRepo.CreateUserProject(project, user);
            _usersRepo.SaveChanges();
            return Ok(project);

        }

        
    }
}