using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Models;
using ProjectManager.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ProjectManager.Models.UtilityModels;
using ProjectManager.Data.Services;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ProjectManager.Controllers 
{
    
    [Route("/project")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepo _projectsRepo;
        private readonly IAppUsersRepo _usersRepo;
        private readonly ITaskTypesRepo _taskTypesRepo;
        private readonly ITasksRepo _tasksRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly ProjectMemberValidation _validation;

        public ProjectsController(
            IProjectsRepo projectsRepo, 
            IAppUsersRepo usersRepo, 
            ITaskTypesRepo taskTypesRepo,
            ITasksRepo tasksRepo,
            UserManager<AppUser> userManager, 
            ProjectMemberValidation validation
        )
        {

            _projectsRepo = projectsRepo;
            _usersRepo = usersRepo;
            _taskTypesRepo = taskTypesRepo;
            _tasksRepo = tasksRepo;
            _userManager = userManager;
            _validation = validation;
        }

        [HttpGet("{projectId}")]
        [Authorize]
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
            projectTasks.Reverse();
            return Ok(projectTasks);
        }

        [Authorize]
        [HttpGet("{projectId}/tasks/recent/{numOfTasks}")]
        public async Task<IActionResult> GetProjectTasks(int projectId, int numOfTasks)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }

            var projectTasks = _projectsRepo.GetProjectTasks(projectId);
            projectTasks.Reverse();

            if(projectTasks.Count <= numOfTasks) 
            {
                return Ok(projectTasks);
            } 
            else
            {
                //take only as many entries as specified in numOfTasks
                projectTasks = projectTasks.GetRange(0, numOfTasks);
                return Ok(projectTasks); 
            }
            
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

        [Authorize]
        [HttpPost("create-task")]
        public async Task<IActionResult> CreateProjectTask([FromBody]UtilityTaskCreateModel taskModel)
        {   
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project member
            if(!_validation.userIsProjectMember(user.Id, taskModel.ProjectId))
            {
                return Unauthorized();
            }

            //create the task object
            Models.Task task = new Models.Task();
            task.ProjectId = taskModel.ProjectId;
            task.Name = taskModel.Name;
            task.Description = taskModel.Description;
            task.TaskStatus = taskModel.TaskStatus;
            task.Urgency = taskModel.Urgency;
            //if task type was specified
            if(taskModel.TaskTypeId != -1)
            {
                task.TaskTypeId = taskModel.TaskTypeId;
            }
            task = _projectsRepo.CreateTask(task);
            _projectsRepo.SaveChanges();

            return Ok(task);
        }   


        [Authorize]
        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> GetTaskDetails(int taskId)
        {   
            
            //retrieve the task from the database
            var task = _tasksRepo.GetTaskById(taskId);

            //now, check if the user has access to the task
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, task.ProjectId))
            {
                return Unauthorized();
            }
            //return the task
            return Ok(task);
        }

        [Authorize]
        [HttpPost("edit-task")]
        public async Task<IActionResult> UpdateTask([FromBody]UtilityTaskEditModel taskModel)
        {   
            
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project member
            if(!_validation.userIsProjectMember(user.Id, taskModel.ProjectId))
            {
                return Unauthorized();
            }

            //create the task object
            Models.Task task = new Models.Task();
            task.TaskId = taskModel.TaskId;
            task.ProjectId = taskModel.ProjectId;
            task.Name = taskModel.Name;
            task.Description = taskModel.Description;
            task.TaskStatus = taskModel.TaskStatus;
            task.Urgency = taskModel.Urgency;
            task.TimeCreated = taskModel.TimeCreated;

            //if task type was specified
            if(taskModel.TaskTypeId != -1)
            {
                task.TaskTypeId = taskModel.TaskTypeId;
            }
            task = _tasksRepo.UpdateTask(task);
            _tasksRepo.SaveChanges();

            return Ok(task);
        }

        [Authorize]
        [HttpGet("{projectId}/task/{taskId}/assigned-users")]
        public async Task<IActionResult> GetTaskUsers(int projectId, int taskId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project member
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }
            var taskUsers = _tasksRepo.GetTaskUsers(taskId);
            return Ok(taskUsers);
        }
        
        [Authorize]
        [HttpGet("{projectId}/task/{taskId}/unassigned-users")]
        public async Task<IActionResult> GetTaskNonUsers(int projectId, int taskId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project member
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }

            //get all the users in the project
            var projectUsers = _projectsRepo.GetProjectUsers(projectId);

            //get all the users assigned to the task
            var taskUsers = _tasksRepo.GetTaskUsers(taskId);

            //now, collect only those users from projectUsers that are not in taskUsers
            var taskNonUsers = new List<AppUser>();
            
            for(int i = 0; i < projectUsers.Count; i++)
            {
                if(!taskUsers.Contains(projectUsers[i])) 
                {
                    taskNonUsers.Add(projectUsers[i]);
                }
            }

            return Ok(taskNonUsers);

        }

        [Authorize]
        [HttpPost("assign-task-user")]
        public async Task<IActionResult> AssignTaskUser([FromBody]UtilityTaskUserModel taskUserModel)
        {   
            //retrieve the task from the database
            var task = _tasksRepo.GetTaskById(taskUserModel.TaskId);

            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if(task == null)
            {
                return NotFound("The given task id is invalid.");
            }

            //make sure the user who is making the request is a project member
            if(!_validation.userIsProjectMember(user.Id, task.ProjectId))
            {
                return Unauthorized("User is not a project member");
            }

            //make sure the user to be assigned is a part of the project
            if(!_validation.userIsProjectMember(taskUserModel.AppUserId, task.ProjectId))
            {
                return BadRequest("This user is not a member of the project");
            }

            //make sure user is not already a task user
            if(_tasksRepo.IsAssignedToTask(taskUserModel.TaskId, taskUserModel.AppUserId) == true)
            {
                return BadRequest("This user has already been assigned to the task");
            }

            //create task user object
            var taskUser = new TaskUser();
            taskUser.AppUserId = taskUserModel.AppUserId;
            taskUser.TaskId = taskUserModel.TaskId;
            taskUser.TimeAdded = DateTime.Now;

            _tasksRepo.AssignUserToTask(taskUser);
            _tasksRepo.SaveChanges();

            return Ok(taskUser);
        }

        [Authorize]
        [HttpPost("unassign-task-user")]
        public async Task<IActionResult> UnassignTaskUser([FromBody]UtilityTaskUserModel taskUserModel)
        {   
            //retrieve the task from the database
            var task = _tasksRepo.GetTaskById(taskUserModel.TaskId);

            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, task.ProjectId))
            {
                return Unauthorized("User is not a project administrator");
            }

            //make sure the user to be assigned is a part of the project
            if(!_validation.userIsProjectMember(taskUserModel.AppUserId, task.ProjectId))
            {
                return BadRequest("This user is not a member of the project");
            }

            //make sure user is currently a task user
            if(_tasksRepo.IsAssignedToTask(taskUserModel.TaskId, taskUserModel.AppUserId) == false)
            {
                return BadRequest("This user has not been assigned to the task");
            }

            _tasksRepo.RemoveUserFromTask(taskUserModel.TaskId, taskUserModel.AppUserId);

            _tasksRepo.SaveChanges();

            return Ok(_tasksRepo.GetTaskUsers(taskUserModel.TaskId));
        }

        [Authorize]
        [HttpGet("{taskId}/comments")]
        public async Task<IActionResult> GetTaskComments(int taskId)
        {
            //retrieve the task from the database
            var task = _tasksRepo.GetTaskById(taskId);

            if(task == null) 
            {
                return BadRequest("The task with the given task id does not exist");
            }

            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, task.ProjectId))
            {
                return Unauthorized();
            }
            
            return Ok(_tasksRepo.GetTaskComments(taskId));
        }

        [Authorize]
        [HttpGet("{taskId}/comments/recent/{numOfComments}")]
        public async Task<IActionResult> GetRecentTaskComments(int taskId, int numOfComments)
        {
            //retrieve the task from the database
            var task = _tasksRepo.GetTaskById(taskId);

            if(task == null) 
            {
                return BadRequest("The task with the given task id does not exist");
            }

            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, task.ProjectId))
            {
                return Unauthorized();
            }
            
            //get all tasks
            var taskComments = _tasksRepo.GetTaskComments(taskId);
            //reverse the order of tasks
            taskComments.Reverse();

            if(taskComments.Count <= numOfComments)
            {
                return Ok(taskComments);
            }
            else{
                taskComments = taskComments.GetRange(0, numOfComments);
            }

            return Ok(taskComments);
        }

        [Authorize]
        [HttpPost("task/{taskId}/comments")]
        public async Task<IActionResult> AddCommentToTask(int taskId)
        {
            //retrieve the task from the database
            var task = _tasksRepo.GetTaskById(taskId);

            if(task == null) 
            {
                return BadRequest("The task with the given task id does not exist");
            }

            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, task.ProjectId))
            {
                return Unauthorized();
            }
            
            return Ok(_tasksRepo.GetTaskComments(taskId));
        }

        [Authorize]
        [HttpPost("task/comment/add")]
        public async Task<IActionResult> AddCommentToTask(UtilityTaskCommentModel taskCommentModel)
        {
            //retrieve the task from the database
            var task = _tasksRepo.GetTaskById(taskCommentModel.TaskId);

            if(task == null) 
            {
                return BadRequest("The task with the given task id does not exist");
            }

            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, task.ProjectId))
            {
                return Unauthorized();
            }
            
            //build the true TaskCommend object
            TaskComment taskComment = new TaskComment();
            taskComment.TaskId = taskCommentModel.TaskId;
            taskComment.AppUserId = user.Id;
            taskComment.Comment = taskCommentModel.Comment;
            taskComment.TimeAdded = DateTime.Now;

            //add to repository
            _tasksRepo.AddTaskComment(taskComment);
            _tasksRepo.SaveChanges();

            return Ok(taskComment);

        }


    }
}