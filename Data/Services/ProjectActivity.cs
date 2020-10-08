using System;
using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;
using ProjectManager.Models.UtilityModels;

namespace ProjectManager.Data.Services
{
    public class ProjectActivity
    {
        private readonly ITasksRepo _tasksRepo;
        private readonly IAppUsersRepo _usersRepo;
        private readonly IProjectsRepo _projectsRepo;
        private readonly ITaskTypesRepo _taskTypesRepo;

        public ProjectActivity(ITasksRepo tasksRepo, IAppUsersRepo usersRepo, IProjectsRepo projectsRepo, ITaskTypesRepo taskTypesRepo)
        {
            _tasksRepo = tasksRepo;
            _usersRepo = usersRepo;
            _projectsRepo = projectsRepo;
            _taskTypesRepo = taskTypesRepo;
        }

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

        private string GetUserFullName(string userId)
        {
            var user = _usersRepo.GetUserById(userId);
            var userFullName = user.FirstName + " " + user.LastName;
            return userFullName;
        }

        private string GetTaskNameById(int taskId)
        {
            return _tasksRepo.GetTaskById(taskId).Name;
        }

    }
}