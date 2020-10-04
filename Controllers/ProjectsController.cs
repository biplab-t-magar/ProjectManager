using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Models;
using ProjectManager.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ProjectManager.Models.UtilityModels;
using ProjectManager.Data.Services;
using System.Threading.Tasks;

namespace ProjectManager.Controllers 
{
    [Route("/project")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepo _projectsRepo;
        private readonly IAppUsersRepo _usersRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly ProjectMemberValidation _validation;

        public ProjectsController(IProjectsRepo projectsRepo, IAppUsersRepo usersRepo, UserManager<AppUser> userManager, ProjectMemberValidation validation)
        {
            _projectsRepo = projectsRepo;
            _usersRepo = usersRepo;
            _userManager = userManager;
            _validation = validation;
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<Project>> GetProject(int projectId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }
            var project = _projectsRepo.GetProjectById(projectId);

            if(project == null) 
            {
                return NotFound();
            }

            return Ok(project);            
        }


        [Authorize]
        [HttpGet("{projectId}/users")]
        public async Task<ActionResult<List<AppUser>>> GetProjectUsers(int projectId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }
            var projectUsers = _projectsRepo.GetProjectUsers(projectId);
            return Ok(projectUsers);
        }
        
        [Authorize]
        [HttpGet("{projectId}/roles")]
        public async Task<ActionResult<List<ProjectUser>>> GetProjectUserRoles(int projectId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }
            var projectUserRoles = _projectsRepo.GetProjectUserRoles(projectId);
            return projectUserRoles;
        }

        [Authorize]
        [HttpGet("{projectId}/tasks")]
        public async Task<ActionResult<List<Models.Task>>> GetProjectTasks(int projectId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }
            var projectTasks = _projectsRepo.GetProjectTasks(projectId);
            return projectTasks;
        }

        [Authorize]
        [HttpGet("{projectId}/tasks/{numOfTasks:int}")]
        public async Task<ActionResult<List<Models.Task>>> GetProjectTasks(int projectId, [FromQuery(Name = "numOfTasks")] int numOfTasks)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }

            var projectTasks = _projectsRepo.GetProjectTasks(projectId);
            return projectTasks;
        }

        [HttpGet("{projectId}/taskTypes")]
        public async Task<ActionResult<List<TaskType>>> GetProjectTaskTypes(int projectId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }

            var projectTaskTypes = _projectsRepo.GetProjectTaskTypes(projectId);
            return projectTaskTypes;
        }

        [Authorize]
        [HttpPost("new")]
        public async Task<IActionResult> CreateProject([FromBody]UtilityProjectModel projectModel)
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

        [Authorize]
        [HttpPost("edit")]
        public async Task<IActionResult> UpdateProject([FromBody] Project project)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectAdministrator(user.Id, project.ProjectId))
            {
                return Unauthorized();
            }

            project = _projectsRepo.UpdateProject(project);
            _projectsRepo.SaveChanges();


            return Ok(project);
        }

        [Authorize]
        [HttpDelete("{projectId}/delete")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectAdministrator(user.Id, projectId))
            {
                return Unauthorized();
            }

            _projectsRepo.DeleteProject(projectId);
            _projectsRepo.SaveChanges();

            return NoContent();
        } 

        
    }
}