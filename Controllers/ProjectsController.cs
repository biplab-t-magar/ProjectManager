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
        private readonly ITaskTypesRepo _taskTypesRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly ProjectMemberValidation _validation;

        public ProjectsController(
            IProjectsRepo projectsRepo, 
            IAppUsersRepo usersRepo, 
            ITaskTypesRepo taskTypesRepo,
            UserManager<AppUser> userManager, 
            ProjectMemberValidation validation
        )
        {
            _projectsRepo = projectsRepo;
            _usersRepo = usersRepo;
            _taskTypesRepo = taskTypesRepo;
            _userManager = userManager;
            _validation = validation;
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProject(int projectId)
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
        public async Task<IActionResult> GetProjectUsers(int projectId)
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
        public async Task<ActionResult> GetProjectUserRoles(int projectId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project member
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }
            var projectUserRoles = _projectsRepo.GetProjectUserRoles(projectId);
            return Ok(projectUserRoles);
        }

        [Authorize]
        [HttpGet("{projectId}/tasks")]
        public async Task<IActionResult> GetProjectTasks(int projectId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }
            var projectTasks = _projectsRepo.GetProjectTasks(projectId);
            return Ok(projectTasks);
        }

        [Authorize]
        [HttpGet("{projectId}/tasks/{numOfTasks:int}")]
        public async Task<IActionResult> GetProjectTasks(int projectId, [FromQuery(Name = "numOfTasks")] int numOfTasks)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }

            var projectTasks = _projectsRepo.GetProjectTasks(projectId);
            return Ok(projectTasks);
        }

        [HttpGet("{projectId}/task-types")]
        public async Task<IActionResult> GetProjectTaskTypes(int projectId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }

            var projectTaskTypes = _projectsRepo.GetProjectTaskTypes(projectId);
            return Ok(projectTaskTypes);
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

        [Authorize]
        [HttpPost("invite")]
        public async Task<IActionResult> InviteToProject([FromBody]UtilityInviteModel inviteModel)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectAdministrator(user.Id, inviteModel.projectId))
            {
                return Unauthorized();
            }
            
            var invitee = _usersRepo.GetUserByUserName(inviteModel.inviteeUserName);

            //if no user with the username exists
            if(invitee == null)
            {
                return NotFound("User with the given username does not exist");
            } 

            //if the user is already part of the project
            if(_validation.userIsProjectMember(invitee.Id, inviteModel.projectId))
            {
                return BadRequest("This user is already a member of the project");
            }

            //if the user has already been invited to the project
            if(_projectsRepo.HasUserBeenInvited(inviteModel.projectId, invitee.Id) == true)
            {
                return BadRequest("This user has already been invited to join this project");
            }
            
            //build project invitation object
            ProjectInvitation projectInvitation = new ProjectInvitation();
            projectInvitation.ProjectId = inviteModel.projectId;
            projectInvitation.InviterId = user.Id;
            projectInvitation.InviteeId = invitee.Id;
            _projectsRepo.AddProjectInvite(projectInvitation);
            _projectsRepo.SaveChanges(); 

            return Ok(invitee);
        }   
        [Authorize]
        [HttpGet("{projectId}/invitees")]
        public async Task<IActionResult> GetProjectInvitees(int projectId)
        {   
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectAdministrator(user.Id, projectId))
            {
                return Unauthorized();
            }
            return Ok(_projectsRepo.GetProjectInvitees(projectId));

        }

        [Authorize]
        [HttpDelete("{projectId}/cancel/{inviteeId}")]
        public async Task<IActionResult> CancelProjectInvite(int projectId, string inviteeId)
        {   
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectAdministrator(user.Id, projectId))
            {
                return Unauthorized();
            }

            
            if(_projectsRepo.DeleteProjectInvite(projectId, inviteeId) == false)
            {
                return NotFound();
            }
            _projectsRepo.SaveChanges(); 

            return Ok(_projectsRepo.GetProjectInvitees(projectId));
        }   

        [Authorize]
        [HttpPost("switch-role")]
        public async Task<IActionResult> SwitchUserRole([FromBody]ProjectUser projectUser)
        {   
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectAdministrator(user.Id, projectUser.ProjectId))
            {
                return Unauthorized("User is not a project administrator");
            }

            //make sure the user is a part of the project
            if(!_validation.userIsProjectMember(projectUser.AppUserId, projectUser.ProjectId))
            {
                return BadRequest("This user is not a member of the project");
            }

            //make sure the user is not changing his/her own role
            if(user.Id == projectUser.AppUserId)
            {
                return BadRequest("You cannot change your own role");
            }

            if(projectUser.Role == "Administrator")
            {
                projectUser = _projectsRepo.SetProjectUserRole(projectUser, "Member");
                _projectsRepo.SaveChanges();
            }
            else 
            {
                projectUser =  _projectsRepo.SetProjectUserRole(projectUser, "Administrator");
                _projectsRepo.SaveChanges();
            }

            return Ok(projectUser);
        }   
        [Authorize]
        [HttpPost("create-task-type")]
        public async Task<IActionResult> CreateTaskType([FromBody]UtilityTaskTypeModel taskType)
        {   
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectAdministrator(user.Id, taskType.ProjectId))
            {
                return Unauthorized();
            }

            //create the task type
            var newTaskType = _taskTypesRepo.CreateTaskType(taskType.ProjectId, taskType.Name);
            _taskTypesRepo.SaveChanges();
            
            return Ok(newTaskType);
        }   

        [Authorize]
        [HttpDelete("{projectId}/task-types/{taskTypeId}")]
        public async Task<IActionResult> DeleteTaskType(int projectId, int taskTypeId)
        {   
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectAdministrator(user.Id, projectId))
            {
                return Unauthorized();
            }

            //check if the task type has been used for any task in the project
            var tasksUsingTaskType = _projectsRepo.GetProjectTasksByTaskType(projectId, taskTypeId);
            if(tasksUsingTaskType.Count == 0)
            {
                _taskTypesRepo.DeleteTaskType(taskTypeId);
                _taskTypesRepo.SaveChanges();
                //return the updated list of task types
                return Ok(_projectsRepo.GetProjectTaskTypes(projectId));
            }
            else 
            {
                return BadRequest("Cannot delete a task type that has already been assigned to a task");
            }
            
            
        }   
    }
}