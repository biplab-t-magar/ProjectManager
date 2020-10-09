/* ProjectActivity.cs
 This file contains the ProjectActivity class. The ProjectActivity class is used to generate information that defines the activities in a project, 
 activities by a user, or activities by a user in a project. The ProjectActivity class gathers disparate data from the database and interprets the data 
 in order to generate easily comprehensible information regarding project activities
*/

using System;
using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;
using ProjectManager.Models.UtilityModels;

namespace ProjectManager.Data.Services
{
    public class ProjectActivity
    {
        //the ProjectManager application tasks repository
        private readonly ITasksRepo _tasksRepo;
        //the ProjectManager application users repository
        private readonly IAppUsersRepo _usersRepo;
        //the ProjectManager application projects repository
        private readonly IProjectsRepo _projectsRepo;
        //the ProjectManager application task types repository
        private readonly ITaskTypesRepo _taskTypesRepo;

        /**/
        /*
        * NAME:
        *      ProjectActivity - constructor for the ProjectActivity class
        * SYNOPSIS:
                ProjectActivity(ITasksRepo tasksRepo, IAppUsersRepo usersRepo, IProjectsRepo projectsRepo, ITaskTypesRepo taskTypesRepo)
        *           tasksRepo --> the ProjectManager application tasks repository that is injected as a dependency injection
                    usersRepo --> the ProjectManager application users repository that is injected as a dependency injection
                    projectsRepo --> the ProjectManager application projects repository that is injected as a dependency injection
                    taskTypesRepo --> the ProjectManager application taskTypes repository that is injected as a dependency injection
        * DESCRIPTION:
                Initializes the ProjectActivity class
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/04/2020 
        * /
        /**/
        public ProjectActivity(ITasksRepo tasksRepo, IAppUsersRepo usersRepo, IProjectsRepo projectsRepo, ITaskTypesRepo taskTypesRepo)
        {
            _tasksRepo = tasksRepo;
            _usersRepo = usersRepo;
            _projectsRepo = projectsRepo;
            _taskTypesRepo = taskTypesRepo;
        }

        /**/
        /*
        * NAME:
        *      GenerateUserActivity - generates UserActivity objects for the given user
        * SYNOPSIS:
                GenerateUserActivity(string userId)
        *           userId --> the id of the user whose activities are to be generated
        * DESCRIPTION:
                This function goes through the database and extracts all the TaskComment, TaskUpdate, and TaskUserUpdate entries
                    associated with the given user, and it interprests those entries to generate descriptions of all the user's activites
                    in the form of a list of UserActivity objects
        * RETURNS
                a List of the UserActivity objects associated with the user
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/04/2020 
        * /
        /**/
        public List<UserActivity> GenerateUserActivity(string userId)
        {
            //get the user's name
            string userFullName = GetUserFullName(userId);

            var userComments = _usersRepo.GetCommentsByUser(userId);
            var userTaskUpdates = _usersRepo.GetTaskUpdatesByUpdater(userId);
            var userTaskUserUpdates = _usersRepo.GetTaskUserUpdatesByUpdater(userId);

            //add comment activity
            var userActivities = new List<UserActivity>();
            string activity = new string("");

            //loop through all the user comments and make UserActivity objects out of them
            for(int i = 0; i < userComments.Count; i++)
            {
                activity = userFullName + " commented on " + GetTaskNameById(userComments[i].TaskId) + ":   " + userComments[i].Comment;
                //create userActivity object
                userActivities.Add(new UserActivity{
                    Activity=activity,
                    ProjectId = _tasksRepo.GetTaskById(userComments[i].TaskId).ProjectId,
                    TaskId=userComments[i].TaskId,
                    Time = userComments[i].TimeAdded
                });
            }

            //add task update activity
            List<string> updated;
            //loop through all the task updates by the user
            for(int i = 0; i < userTaskUpdates.Count; i++)
            {
                updated = GetUpdatedAttributeAndValue(userTaskUpdates[i]);
                if(updated == null)
                {
                    activity = userFullName + " created a new task: " + GetTaskNameById(userTaskUpdates[i].TaskId);
                }
                else 
                {
                    activity = userFullName + " changed the " + updated[0] + " for " + 
                    GetTaskNameById(userTaskUpdates[i].TaskId) + " to " + updated[1];
                }
                

                //create userActivity object
                userActivities.Add(new UserActivity{
                    Activity=activity,
                    ProjectId = _tasksRepo.GetTaskById(userTaskUpdates[i].TaskId).ProjectId,
                    TaskId=userTaskUpdates[i].TaskId,
                    Time = userTaskUpdates[i].TimeStamp
                });
            }

            //add task user update activity
            for(int i = 0; i < userTaskUserUpdates.Count; i++)
            {
                //if the task user update is about user being added to task

                if(userTaskUserUpdates[i].TimeAdded != null)
                {
                    activity = userFullName + " assigned " + GetUserFullName(userTaskUserUpdates[i].AppUserId) 
                        + " to " + GetTaskNameById(userTaskUserUpdates[i].TaskId);
                    //create userActivity object
                    userActivities.Add(new UserActivity{
                        Activity=activity,
                        ProjectId = _tasksRepo.GetTaskById(userTaskUpdates[i].TaskId).ProjectId,
                        TaskId=userTaskUserUpdates[i].TaskId,
                        Time = (DateTime) userTaskUserUpdates[i].TimeAdded
                    });
                } else 
                {
                    activity = userFullName + " unassigned " + GetUserFullName(userTaskUserUpdates[i].AppUserId) 
                        + " from " + GetTaskNameById(userTaskUserUpdates[i].TaskId);
                    //create userActivity object
                    userActivities.Add(new UserActivity{
                        Activity=activity,
                        ProjectId = _tasksRepo.GetTaskById(userTaskUpdates[i].TaskId).ProjectId,
                        TaskId=userTaskUserUpdates[i].TaskId,
                        Time = (DateTime) userTaskUserUpdates[i].TimeRemoved
                    });
                }
            }

            //now sort the list of user activity by date
            userActivities.Sort((activity1, activity2) => DateTime.Compare(activity2.Time, activity1.Time));
            return userActivities;
        }

        /**/
        /*
        * NAME:
        *      GenerateProjectActivity - generates UserActivity objects for all the users in a project
        * SYNOPSIS:
                GenerateProjectActivity(int projectId)
        *           projectId --> the id of the project whose user activities are to be generated
        * DESCRIPTION:
                This function goes through the database and extracts all the TaskComment, TaskUpdate, and TaskUserUpdate entries
                    associated with the given project, and it interprets those entries to generate descriptions of all the project's activites
                    in the form of a list of UserActivity objects
        * RETURNS
                a List of the UserActivity objects associated with the project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/04/2020 
        * /
        /**/
        public List<UserActivity> GenerateProjectActivity(int projectId)
        {
            var projectComments = _projectsRepo.GetProjectTaskComments(projectId);
            var projectTaskUpdates = _projectsRepo.GetTaskUpdatesByProject(projectId);
            var projectTaskUserUpdates = _projectsRepo.GetProjectTaskUserUpdates(projectId);

            var projectActivities = new List<UserActivity>();
            string activity = new string("");
            
            //loop through all project comments
            for(int i = 0; i < projectComments.Count; i++)
            {
                activity = GetUserFullName(projectComments[i].AppUserId) + " commented on " + GetTaskNameById(projectComments[i].TaskId) 
                    + ":   " + projectComments[i].Comment;
                //create userActivity object
                projectActivities.Add(new UserActivity{
                    Activity=activity,
                    ProjectId = projectId,
                    TaskId=projectComments[i].TaskId,
                    Time = projectComments[i].TimeAdded
                });
            }

            //loop through all project task updates
            List<string> updated;
            //loop through all the task updates by the user
            for(int i = 0; i < projectTaskUpdates.Count; i++)
            {
                updated = GetUpdatedAttributeAndValue(projectTaskUpdates[i]);
                if(updated == null)
                {
                    activity = GetUserFullName(projectTaskUpdates[i].UpdaterId) + " created a new task: " + GetTaskNameById(projectTaskUpdates[i].TaskId);
                }
                else
                {
                    activity = GetUserFullName(projectTaskUpdates[i].UpdaterId) + " changed the " + updated[0] + " for " + 
                    GetTaskNameById(projectTaskUpdates[i].TaskId) + " to " + updated[1];
                }
                

                //create userActivity object
                projectActivities.Add(new UserActivity{
                    Activity=activity,
                    ProjectId = projectId,
                    TaskId=projectTaskUpdates[i].TaskId,
                    Time = projectTaskUpdates[i].TimeStamp
                });
            }

            //add task user update activity
            for(int i = 0; i < projectTaskUserUpdates.Count; i++)
            {
                //if the task user update is about user being added to task

                if(projectTaskUserUpdates[i].TimeAdded != null)
                {
                    activity = GetUserFullName(projectTaskUserUpdates[i].UpdaterId) + " assigned " + GetUserFullName(projectTaskUserUpdates[i].AppUserId) 
                        + " to " + GetTaskNameById(projectTaskUserUpdates[i].TaskId);
                    //create userActivity object
                    projectActivities.Add(new UserActivity{
                        Activity=activity,
                        ProjectId = projectId,
                        TaskId= projectTaskUserUpdates[i].TaskId,
                        Time = (DateTime) projectTaskUserUpdates[i].TimeAdded
                    });
                } else 
                {
                    activity = GetUserFullName(projectTaskUserUpdates[i].UpdaterId) + " unassigned " + GetUserFullName(projectTaskUserUpdates[i].AppUserId) 
                        + " from " + GetTaskNameById(projectTaskUserUpdates[i].TaskId);

                    //create userActivity object
                    projectActivities.Add(new UserActivity{
                        Activity=activity,
                        ProjectId = projectId,
                        TaskId= projectTaskUserUpdates[i].TaskId,
                        Time = (DateTime) projectTaskUserUpdates[i].TimeRemoved
                    });
                }
                
            }

            //now sort the list of user activity by date
            projectActivities.Sort((activity1, activity2) => DateTime.Compare(activity2.Time, activity1.Time));
            return projectActivities;
        }

        /**/
        /*
        * NAME:
        *      GenerateUserActivityInProject - generates UserActivity objects for a user's activities in a project
        * SYNOPSIS:
                GenerateUserActivityInProject(int projectId, string userId)
        *           projectId --> the id of the project whose user's activities are to be generated
                    userId --> the id of the user whose activites in the project are to be generated
        * DESCRIPTION:
                This function goes through the database and extracts all the TaskComment, TaskUpdate, and TaskUserUpdate entries
                    by a user in a given project, and it interprets those entries to generate descriptions of all the user's project activites
                    in the form of a list of UserActivity objects
        * RETURNS
                a List of the UserActivity objects by a user in a project
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/04/2020 
        * /
        /**/
        public List<UserActivity> GenerateUserActivityInProject(int projectId, string userId)
        {
            var projectUserComments = _projectsRepo.GetProjectTaskCommentsByUser(projectId, userId);
            var projectUserTaskUpdates = _projectsRepo.GetTaskUpdatesByUpdaterInProject(projectId, userId);
            var projectUserTaskUserUpdates = _projectsRepo.GetProjectTaskUserUpdatesByUpdater(projectId, userId);

            var projectUserActivities = new List<UserActivity>();
            string activity = new string("");
            string userFullName = GetUserFullName(userId);
            
            //loop through all project comments
            for(int i = 0; i < projectUserComments.Count; i++)
            {
                activity = userFullName + " commented on " + GetTaskNameById(projectUserComments[i].TaskId) 
                    + ":   " + projectUserComments[i].Comment;
                //create userActivity object
                projectUserActivities.Add(new UserActivity{
                    Activity=activity,
                    ProjectId = projectId,
                    TaskId=projectUserComments[i].TaskId,
                    Time = projectUserComments[i].TimeAdded
                });
            }

            //loop through all project task updates
            List<string> updated;
            //loop through all the task updates by the user
            for(int i = 0; i < projectUserTaskUpdates.Count; i++)
            {
                updated = GetUpdatedAttributeAndValue(projectUserTaskUpdates[i]);
                if(updated == null)
                {
                    activity = userFullName + " created a new task: " + GetTaskNameById(projectUserTaskUpdates[i].TaskId);
                } else 
                {
                    activity = userFullName + " changed the " + updated[0] + " for " + 
                    GetTaskNameById(projectUserTaskUpdates[i].TaskId) + " to " + updated[1];
                }
                

                //create userActivity object
                projectUserActivities.Add(new UserActivity{
                    Activity=activity,
                    ProjectId = projectId,
                    TaskId=projectUserTaskUpdates[i].TaskId,
                    Time = projectUserTaskUpdates[i].TimeStamp
                });
            }

            //add task user update activity
            for(int i = 0; i < projectUserTaskUserUpdates.Count; i++)
            {
                //if the task user update is about user being added to task

                if(projectUserTaskUserUpdates[i].TimeAdded != null)
                {
                    activity = userFullName + " assigned " + GetUserFullName(projectUserTaskUserUpdates[i].AppUserId) 
                        + " to " + GetTaskNameById(projectUserTaskUserUpdates[i].TaskId);
                    //create userActivity object
                    projectUserActivities.Add(new UserActivity{
                        Activity=activity,
                        ProjectId = projectId,
                        TaskId=projectUserTaskUserUpdates[i].TaskId,
                        Time = (DateTime) projectUserTaskUserUpdates[i].TimeAdded
                    });
                } else 
                {
                    activity = userFullName + " unassigned " + GetUserFullName(projectUserTaskUserUpdates[i].AppUserId) 
                        + " from " + GetTaskNameById(projectUserTaskUserUpdates[i].TaskId);

                    //create userActivity object
                    projectUserActivities.Add(new UserActivity{
                        Activity=activity,
                        ProjectId = projectId,
                        TaskId=projectUserTaskUserUpdates[i].TaskId,
                        Time = (DateTime) projectUserTaskUserUpdates[i].TimeRemoved
                    });
                }
                
            }

            //sort in descending order of time
            projectUserActivities.Sort((activity1, activity2) => DateTime.Compare(activity2.Time, activity1.Time));
            return projectUserActivities;
        }

        /**/
        /*
        * NAME:
        *      GetUpdatedAttributeAndValue - takes a TaskUpdate object and determines what attributes were updated 
        * SYNOPSIS:
                GetUpdatedAttributeAndValue(TaskUpdate taskUpdate)
        *           taskUpdate --> the TaskUpdate object used to determine what attributes of the task were updated
        * DESCRIPTION:
                This function goes through all the attributes in the TaskUpdate object and figures out whether an attribute was 
                updated or not and also figures out the updated value
        * RETURNS
                a list containin the updated attribute in the first index and the value of the attribute in the second index
                returns null if the TaskUpdate represent the creation of a task
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/04/2020 
        * /
        /**/
        private List<string> GetUpdatedAttributeAndValue(TaskUpdate taskUpdate)
        {
            // this list stores two strings, the first one holding the attribute changed and the second one holding 
            //the value of the attribute
            var updated = new List<string>();

            //first check what task attribute was updated in the taskUpdate
            if(taskUpdate.Name != null)
            {
                updated.Add("name");
                updated.Add(taskUpdate.Name);
            } else if(taskUpdate.TaskStatus != null)
            {
                updated.Add("task status");
                updated.Add(taskUpdate.TaskStatus);
            } else if(taskUpdate.Urgency != null)
            {
                updated.Add("urgency");
                updated.Add(taskUpdate.Urgency);
            } else if(taskUpdate.TaskTypeId != null)
            {
                updated.Add("task type");
                updated.Add(_taskTypesRepo.GetTaskTypeById((int)taskUpdate.TaskTypeId).Name);
            } else {
                updated = null;
            }
            return updated;
        }

        /**/
        /*
        * NAME:
        *      GetUserFullName - returns the name of the user with the given id
        * SYNOPSIS:
                GetUserFullName(string userId)
        *           userId --> the id of the user whose full name is to be returned
        * DESCRIPTION:
                This function returns the full name of the user with the given id
        * RETURNS
                a string containing the user's full name
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/04/2020 
        * /
        /**/
        private string GetUserFullName(string userId)
        {
            var user = _usersRepo.GetUserById(userId);
            var userFullName = user.FirstName + " " + user.LastName;
            return userFullName;
        }

        /**/
        /*
        * NAME:
        *      GetTaskNameById - returns the name of the task with the given id
        * SYNOPSIS:
                GetTaskNameById(int taskId)
        *           taskId --> the id of the task whose name is to be returned
        * DESCRIPTION:
                This function returns the name of the task with the given id
        * RETURNS
                a string containing the task's full name
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/04/2020 
        * /
        /**/
        private string GetTaskNameById(int taskId)
        {
            return _tasksRepo.GetTaskById(taskId).Name;
        }

    }
}