/* ProjectController.cs
 This file contains the ProjectController class, which represents one of the three controller classes in the ProjectManager project.
 A controller class in ASP.NET Core is responsible for defining and handling HTTP requests to specific routes. The ProjectsController class handles
 routes and HTTP requests relating to everything specific to Projects in the Project Manager web application
*/

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
        private readonly ProjectActivity _projectActivity;


        /**/
        /*
        * NAME:
        *      ProjectsController - constructor for the ProjectsController class
        * SYNOPSIS:
                ProjectsController(IAppUsersRepo usersRepo, IProjectsRepo projectsRepo, ITasksRepo tasksRepo, ITaskTypesRepo
                                    UserManager userManager, ProjectActivity projectActivity)
        *           usersRepo --> the ProjectManager application users repository that is injected as a dependency injection
                    projectsRepo --> the ProjectManager application projects repository that is injected as a dependency injection
                    tasksRepo --> the ProjectManager application tasks repository that is injected as a dependency injection
                    taskTypesRepo --> the ProjectManager application task types repository that is injected as a dependency injection
                    userManager --> an instance of the UserManager class, which is a class provided by Asp.Net Core Identity to handle
                                    use registration and retrieval
                    validation --> an instance of the ProjectMemberValidation class that is injected as a dependency injection                
                    projectActivity --> an instance of the ProjectActivity class that is injected as a dependency injection
        * DESCRIPTION:
                Initializes the ProjectsController class
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/06/2020 
        * /
        /**/
        public ProjectsController(
            IProjectsRepo projectsRepo, 
            IAppUsersRepo usersRepo, 
            ITaskTypesRepo taskTypesRepo,
            ITasksRepo tasksRepo,
            UserManager<AppUser> userManager, 
            ProjectMemberValidation validation,
            ProjectActivity projectActivity
        )
        {
            _projectsRepo = projectsRepo;
            _usersRepo = usersRepo;
            _taskTypesRepo = taskTypesRepo;
            _tasksRepo = tasksRepo;
            _userManager = userManager;
            _validation = validation;
            _projectActivity = projectActivity;
        }

        /**/
        /*
        * NAME:
        *      GetProject
        * SYNOPSIS:
                GetProject(int projectId)
                    projectId --> the id of the project
        * DESCRIPTION:
                In response to a Get request, it returns an HTTP response with the info of the project with the given id
        * RETURNS
                an HTTP response containing the info on the project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/13/2020 
        * /
        /**/
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


        /**/
        /*
        * NAME:
        *      GetProjectUsers
        * SYNOPSIS:
                GetProjectUsers(int projectId)
                    projectId --> the id of the project
        * DESCRIPTION:
                Accepts a GET request and returns a list of all of the users that are members of the given project
        * RETURNS
                an HTTP response containing the list of all the users in a project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/13/2020 
        * /
        /**/
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
        
        /**/
        /*
        * NAME:
        *      GetProjectUserRoles
        * SYNOPSIS:
                GetProjectUserRoles(int projectId)
                    projectId --> the id of the project
        * DESCRIPTION:
                Accepts a GET request and returns a listing of the roles of the users in the project
        * RETURNS
                an HTTP response containing the listing of the roles of all the users in a project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/13/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetProjectTasks
        * SYNOPSIS:
                GetProjectTasks(int projectId)
                    projectId --> the id of the project whose tasks are to be returned
        * DESCRIPTION:
                Accepts a GET request and returns a listing of all the tasks in a project
        * RETURNS
                an HTTP response containing the listing of the tasks in the specified project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/13/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetProjectTasks
        * SYNOPSIS:
                GetProjectTasks(int projectId, int numOfTasks)
                    projectId --> the id of the project whose tasks are to be returned
                    numOfTasks --> the number of tasks to be returned
        * DESCRIPTION:
                Accepts a GET request and returns a listing of the specified number of tasks in a project
        * RETURNS
                an HTTP response containing the listing of the specified number of tasks in the specified project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/14/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetProjectTaskTypes
        * SYNOPSIS:
                GetProjectTaskTypes(int projectId)
                    projectId --> the id of the project whose task types are to be returned
        * DESCRIPTION:
                Accepts a GET request and returns a listing of all the task types in a project
        * RETURNS
                an HTTP response containing the listing of the task types in the the specified project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/14/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      CreateProject
        * SYNOPSIS:
                CreateProject(UtilityProjectModel projectModel)
                    projectModel --> an instance of the UtilityProjectModel class, which contains information
                                    on the project to be created
        * DESCRIPTION:
                Accepts a POST request, extracts values for the attributes of UtilityProjectModel from the post request
                    body, and then creates a new project entry in the database
        * RETURNS
                an HTTP response containing the newly created project object
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/14/2020 
        * /
        /**/
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


        /**/
        /*
        * NAME:
        *      UpdateProject
        * SYNOPSIS:
                UpdateProject(Project project)
                    project --> an instance of the Project class, which contains information
                                    on the project to be updated
        * DESCRIPTION:
                Accepts a POST request, extracts values for the attributes of the Project object from the post request
                    body, and then updates the project entry in the database
        * RETURNS
                an HTTP response containing the updated project object
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/14/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      DeleteProject
        * SYNOPSIS:
                DeleteProject(int projectId)
                    projectId --> the id of the project to be deleted
        * DESCRIPTION:
                Accepts a HTTP DELETE request and deletes the project with the given projet id
        * RETURNS
                an HTTP response indicating no content to be returned
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/14/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      InviteToProject
        * SYNOPSIS:
                InviteToProject(UtilityInviteModel inviteModel)
                    inviteModel --> an instance of the UtilityInviteModel class, which contains information
                                    on the project invite
        * DESCRIPTION:
                Accepts an HTTP POST request, extracts values for the attributes of the UtilityInviteModel object from the post request
                    body, and then creates the project invite
        * RETURNS
                an HTTP response containing the instance of the user who was invited
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/22/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      GetProjectInvitees
        * SYNOPSIS:
                GetProjectInvitees(int projecId)
                    projecId --> the id of the project whose list of invitees are to be returned
        * DESCRIPTION:
                Accepts an HTTP GET request, and returns the list of invitees to a project
        * RETURNS
                an HTTP response containing the list of invitees to a project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/22/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      CancelProjectInvite
        * SYNOPSIS:
                CancelProjectInvite(int projecId, string inviteeId)
                    projecId --> the id of the project whose project invitee is to be canceled
                    inviteeId --> the invitee to whom the invitation was sent
        * DESCRIPTION:
                Accepts an HTTP DELETE request, and deletes the project invitation sent to the given user 
        * RETURNS
                an HTTP response containing the updated list of invitees to a project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/23/2020 
        * /
        /**/
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


        /**/
        /*
        * NAME:
        *      SwitchUserRole
        * SYNOPSIS:
                SwitchUserRole(ProjectUser projectUser)
                    projecUser --> an instance of the project user object, which contains information on a 
                                project user's role in the project
        * DESCRIPTION:
                Accepts an HTTP POST request, extracts information for the ProjectUser instance, and uses that instance
                to determine the relevant user, and then switches the role for that user
        * RETURNS
                an HTTP response returning the ProjectUser object
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/23/2020 
        * /
        /**/
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
                //switch the user's role
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


        /**/
        /*
        * NAME:
        *      CreateTaskType
        * SYNOPSIS:
                CreateTaskType(UtilityTaskTypeModel taskType)
                    taskType --> an instance of the UtilityTaskTypeModel, which contains information on the task type
                                    to be created
        * DESCRIPTION:
                Accepts an HTTP POST request, extracts information for the UtilityTaskTypeModel instance, and uses that instance
                to create a new task type for the project
        * RETURNS
                an HTTP response returning the newly created TaskType object
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/19/2020 
        * /
        /**/
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


        /**/
        /*
        * NAME:
        *      DeleteTaskType
        * SYNOPSIS:
                DeleteTaskType(int projectId, int taskTypeId)
                    projectId --> the id of the project to which the task type belongs
                    taskTypeId --> the id of the task type to be deleted
        * DESCRIPTION:
                Accepts an HTTP DELETE request, and deletes the task type with the given id
        * RETURNS
                an HTTP response returning the updates list of task types in the project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/19/2020 
        */
        /**/
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

        /**/
        /*
        * NAME:
        *      CreateProjectTask
        * SYNOPSIS:
                CreateProjectTask(UtilityTaskCreateModel taskModel)
                    taskModel --> an instance of the UtilityTaskCreateModel, which contains information on the 
                                    task to be created
        * DESCRIPTION:
                Accepts an HTTP POST request, extracts information for the UtilityTaskCreateModel instance, and uses that instance
                to create a new task for the project
        * RETURNS
                an HTTP response returning the newly created task
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/19/2020 
        */
        /**/
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
            task.TimeCreated = DateTime.Now;
            //if task type was specified
            if(taskModel.TaskTypeId != -1)
            {
                task.TaskTypeId = taskModel.TaskTypeId;
            }
            task = _projectsRepo.CreateTask(task, user.Id);
            _projectsRepo.SaveChanges();

            return Ok(task);
        }   

        /**/
        /*
        * NAME:
        *      GetTaskDetails
        * SYNOPSIS:
                GetTaskDetails(int taskId)
                    taskId --> the id of the task whose details are to be returned
        * DESCRIPTION:
                Accepts an HTTP GET request, and returns the task with the given task id
        * RETURNS
                an HTTP response returning the Task object with the given id
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/19/2020 
        */
        /**/
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

        /**/
        /*
        * NAME:
        *      UpdateTask
        * SYNOPSIS:
                UpdateTask(UtilityTaskEditModel taskModel)
                    taskModel --> an instance of the UtilityTaskEditModel, which contains information on the 
                                    task to be edited
        * DESCRIPTION:
                Accepts an HTTP POST request, extracts information for the UtilityTaskEditModel instance, and uses that instance
                to make changes to the task
        * RETURNS
                an HTTP response returning the updated Task object
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/19/2020 
        */
        /**/
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
            task = _tasksRepo.UpdateTask(task, user.Id);
            _tasksRepo.SaveChanges();

            return Ok(task);
        }

        /**/
        /*
        * NAME:
        *      GetTaskUsers
        * SYNOPSIS:
                GetTaskUsers(int projectId, int taskId)
                    projectId --> the id of the project that the task belongs to
                    taskId --> the id of the task whose users are to be returned
        * DESCRIPTION:
                Accepts an HTTP GET request, and returns the list of users assigned to the given task
        * RETURNS
                an HTTP response containg the list of AppUser objects of users assigned to the given task
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/19/2020 
        */
        /**/
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
        
        /**/
        /*
        * NAME:
        *      GetTaskNonUsers
        * SYNOPSIS:
                GetTaskNonUsers(int projectId, int taskId)
                    projectId --> the id of the project that the task belongs to
                    taskId --> the id of the task 
        * DESCRIPTION:
                Accepts an HTTP GET request, and returns the list of users in the project not yet assigned to the given task
        * RETURNS
                an HTTP response containg the list of AppUser objects of users not assigned to the given task
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/19/2020 
        */
        /**/
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


        /**/
        /*
        * NAME:
        *      AssignTaskUser
        * SYNOPSIS:
                AssignTaskUser(TaskUser taskUser)
                    taskUser --> an instance of the TaskUser class, which holds the information on the relation between a user 
                                    and a task
        * DESCRIPTION:
                Accepts an HTTP POST request, extracts values for the attributes of the TaskUser object from the post request
                    body, and then uses the object to assign the given task to the given user
        * RETURNS
                an HTTP response containg the TaskUser object representing the assignment of the user to the task
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/19/2020 
        */
        /**/
        [Authorize]
        [HttpPost("assign-task-user")]
        public async Task<IActionResult> AssignTaskUser([FromBody]TaskUser taskUser)
        {   
            //retrieve the task from the database
            var task = _tasksRepo.GetTaskById(taskUser.TaskId);

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
            if(!_validation.userIsProjectMember(taskUser.AppUserId, task.ProjectId))
            {
                return BadRequest("This user is not a member of the project");
            }

            //make sure user is not already a task user
            if(_tasksRepo.IsAssignedToTask(taskUser.TaskId, taskUser.AppUserId) == true)
            {
                return BadRequest("This user has already been assigned to the task");
            }
            
            //add the task user entry
            _tasksRepo.AssignUserToTask(taskUser, user.Id);
            _tasksRepo.SaveChanges();

            return Ok(taskUser);
        }


        /**/
        /*
        * NAME:
        *      UnassignTaskUser
        * SYNOPSIS:
                UnassignTaskUser(TaskUser taskUser)
                    taskUser --> an instance of the TaskUser class, which holds the information on the relation between a user 
                                    and a task
        * DESCRIPTION:
                Accepts an HTTP POST request, extracts values for the attributes of the TaskUser object from the post request
                    body, and then uses the object to unassign the given user from the given task
        * RETURNS
                an HTTP response containg the updated list of task users
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/19/2020 
        */
        /**/
        [Authorize]
        [HttpPost("unassign-task-user")]
        public async Task<IActionResult> UnassignTaskUser([FromBody]TaskUser taskUser)
        {   
            //retrieve the task from the database
            var task = _tasksRepo.GetTaskById(taskUser.TaskId);

            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project administrator
            if(!_validation.userIsProjectMember(user.Id, task.ProjectId))
            {
                return Unauthorized("User is not a project administrator");
            }

            //make sure the user to be assigned is a part of the project
            if(!_validation.userIsProjectMember(taskUser.AppUserId, task.ProjectId))
            {
                return BadRequest("This user is not a member of the project");
            }

            //make sure user is currently a task user
            if(_tasksRepo.IsAssignedToTask(taskUser.TaskId, taskUser.AppUserId) == false)
            {
                return BadRequest("This user has not been assigned to the task");
            }

            _tasksRepo.RemoveUserFromTask(taskUser, user.Id);

            _tasksRepo.SaveChanges();

            return Ok(_tasksRepo.GetTaskUsers(taskUser.TaskId));
        }

        /**/
        /*
        * NAME:
        *      GetTaskComments
        * SYNOPSIS:
                GetTaskComments(int taskId)
                    taskId --> the id of the task of which the comments are to be returned
        * DESCRIPTION:
                Accepts an HTTP GET request, and returns the list of all task comments for the given task
        * RETURNS
                an HTTP response containg the list of all TaskComment objects associated with the given task
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/19/2020 
        */
        /**/
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

        /**/
        /*
        * NAME:
        *      GetRecentTaskComments
        * SYNOPSIS:
                GetRecentTaskComments(int taskId, int numOfComments)
                    taskId --> the id of the task of which the comments are to be returned
                    numOfComments --> the number of comments to return
        * DESCRIPTION:
                Accepts an HTTP GET request, and returns the list of the speciefied number of task comments for the given task
        * RETURNS
                an HTTP response containg the list of all TaskComment objects associated with the given task
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/19/2020 
        */
        /**/
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
            // taskComments.Reverse();

            if(taskComments.Count <= numOfComments)
            {
                return Ok(taskComments);
            }
            else{
                taskComments = taskComments.GetRange(0, numOfComments);
            }

            return Ok(taskComments);
        }


        /**/
        /*
        * NAME:
        *      AddCommentToTask
        * SYNOPSIS:
                AddCommentToTask(UtilityTaskCommentModel taskCommentModel)
                    taskCommentModel --> an instance of the UtilityTaskCommentModel class, which holds the information on the comment
                                        to be added to the task
        * DESCRIPTION:
                Accepts an HTTP POST request, extracts values for the attributes of the UtilityTaskCommentModel object from the post request
                    body, and then uses the object to add a comment to the given task
        * RETURNS
                an HTTP response containg the object for the newly created task comment
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/05/2020 
        */
        /**/
        
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

            //make sure the user who is making the request is a project member
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

        /**/
        /*
        * NAME:
        *      GetUserActivityInProject
        * SYNOPSIS:
                GetUserActivityInProject(int projectId, string userId)
                    projectId --> the id of the project for which the activity is to be returned
                    userId --> the id of the user whose activity in the projet is to be returned
        * DESCRIPTION:
                Accepts an HTTP GET request, and returns the list of ProjectActivity objects corresponding to all 
                    the activities by a user in a project 
        * RETURNS
                an HTTP response containg the list of ProjectActivity objects
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/05/2020 
        */
        /**/
        [Authorize]
        [HttpGet("{projectId}/user/{userId}/activity")]
        public async Task<IActionResult> GetUserActivityInProject(int projectId, string userId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project member
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }
            
            var userActivitiesInProject = _projectActivity.GenerateUserActivityInProject(projectId, userId);

            if(userActivitiesInProject == null)
            {
                return NotFound();
            }
            
            return Ok(userActivitiesInProject);

        }

        /**/
        /*
        * NAME:
        *      GetProjectActivity
        * SYNOPSIS:
                GetProjectActivity(int projectId)
                    projectId --> the id of the project for which the activity is to be returned
        * DESCRIPTION:
                Accepts an HTTP GET request, and returns the list of ProjectActivity objects corresponding to all 
                    the activities in a project 
        * RETURNS
                an HTTP response containg the list of ProjectActivity objects
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/05/2020 
        */
        /**/
        [Authorize]
        [HttpGet("{projectId}/activity")]
        public async Task<IActionResult> GetProjectActivity(int projectId)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project member
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }
            
            var projectActivities = _projectActivity.GenerateProjectActivity(projectId);

            if(projectActivities == null)
            {
                return NotFound();
            }
            
            return Ok(projectActivities);

        }

        /**/
        /*
        * NAME:
        *      GetProjectActivity
        * SYNOPSIS:
                GetProjectActivity(int projectId, int num)
                    projectId --> the id of the project for which the activity is to be returned
                    num --> the number of activities to return
        * DESCRIPTION:
                Accepts an HTTP GET request, and returns the list of ProjectActivity objects corresponding to the given number of 
                     activities in a project 
        * RETURNS
                an HTTP response containg the list of ProjectActivity objects
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/05/2020 
        */
        /**/
        [Authorize]
        [HttpGet("{projectId}/activity/{num}")]
        public async Task<IActionResult> GetProjectActivity(int projectId, int num)
        {
            //get the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            //make sure the user who is making the request is a project member
            if(!_validation.userIsProjectMember(user.Id, projectId))
            {
                return Unauthorized();
            }
            
            var projectActivities = _projectActivity.GenerateProjectActivity(projectId);

            if(projectActivities == null)
            {
                return NotFound();
            }
            
            if(projectActivities.Count <= num) 
            {
                return Ok(projectActivities);
            } 
            else
            {
                //take only as many entries as specified in numOfTasks
                projectActivities = projectActivities.GetRange(0, num);
                return Ok(projectActivities); 
            }

        }


    }
}