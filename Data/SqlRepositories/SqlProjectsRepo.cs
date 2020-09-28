using System.Collections.Generic;
using System.Linq;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlProjectsRepo : IProjectsRepo
    {
        //The database context
        private readonly ProjectManagerContext _context;

        public SqlProjectsRepo(ProjectManagerContext context)
        {   
            _context = context;
        }


        public Project GetProjectById(int projectId)
        {
            return _context.Projects.Find(projectId);

        }

        public List<Task> GetProjectTasks(int projectId)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId).ToList();
        }

        public List<Task> GetProjectTasksByTaskStatus(int projectId, string taskStatus)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskStatus == taskStatus).ToList();
        }

        public List<Task> GetProjectTasksByTaskType(int projectId, int taskTypeId)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskTypeId == taskTypeId).ToList();
        }

        public List<Task> GetProjectTasksByUrgency(int projectId, string urgency)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId && t.Urgency == urgency).ToList();
        }

        public List<TaskType> GetProjectTaskTypes(int projectId)
        {
            return _context.TaskTypes.Where(tt => tt.ProjectId == projectId).ToList();
        }

        public List<TaskType> GetProjectTaskTypesByDefaultUrgency(int projectId, string defaultUrgency)
        {
            return _context.TaskTypes.Where(tt => tt.ProjectId == projectId && tt.DefaultUrgency == defaultUrgency).ToList();
        }

        public List<User> GetProjectUsers(int projectId)
        {
            //first get all the ProjectUser entries that are paired with the given project id
            var projectUsers = _context.ProjectUsers.Where(p => p.ProjectId == projectId).ToList();

            //to store all users with the returned project entries
            List<User> users = new List<User>();

            //loop through all user ids and store corresponding user entries
            for(int i = 0; i < projectUsers.Count; i++)
            {
                users.Add(_context.Users.Find(projectUsers[i].UserId));
            }

            //return the queried values
            return users;
        }

        public List<User> GetProjectUsersByRole(int projectId, string role)
        {
            var projectUsers = _context.ProjectUsers.Where(p => (p.ProjectId == projectId && p.Role == role)).ToList();

            //store all users corresponding to the given projectId and role
            List<User> users = new List<User>();

            //loop through all users and store corresponding user entries 
            for(int i = 0; i < projectUsers.Count; i++)
            {
                users.Add(_context.Users.Find(projectUsers[i].UserId));
            }

            //return the queried values
            return users;
        }

        public List<TaskUpdate> GetTaskUpdatesByProject(int projectId)
        {
            return _context.TaskUpdates.Where(tu => tu.ProjectId == projectId).ToList();
        }

        public List<Task> GetUserProjectTasks(int projectId, int userId)
        {
            //first get all the taskUsers entry with the given projectId and userId
            var taskUsers = _context.TaskUsers.Where(t => t.ProjectId == projectId && t.UserId == userId).ToList();

            //store all tasks by corresponding to the taskUsers entry
            List<Task> tasks = new List<Task>();

            //loop through all tasks and store corresponding Task entries
            for(int i = 0; i < taskUsers.Count; i++)
            {
                tasks.Add(_context.Tasks.Find(taskUsers[i].ProjectId, taskUsers[i].TaskId));
            }

            return tasks;
        }
    }
}