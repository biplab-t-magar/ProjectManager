using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;
using System.Linq;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlTasksRepo : ITasksRepo
    {
        private readonly ProjectManagerContext _context;

        public SqlTasksRepo(ProjectManagerContext context)
        {   
            _context = context;
        }
        public Task GetTaskById(int projectId, int taskId)
        {
            return _context.Tasks.Find(projectId, taskId);
        }

        public List<User> GetTaskUsers(int projectId, int taskId)
        {
            //first get all the TaskUser entries with the matching key
            var taskUsers = _context.TaskUsers.Where(tu => tu.ProjectId == projectId && tu.TaskId == taskId).ToList();

            List<User> users = new List<User>();

            //loop through all taskUser entries and store their corresponding User entries
            for(int i = 0; i < taskUsers.Count; i++)
            {
                users.Add(_context.Users.Find(taskUsers[i].UserId));
            }

            return users;
        }

        public TaskUpdate GetTaskUpdate(int projectId, int taskId, int taskUpdateId)
        {
            return _context.TaskUpdates.Find(projectId, taskId, taskUpdateId);
        }

        public List<TaskUpdate> GetTaskUpdatesByTask(int projectId, int taskId)
        {
            return _context.TaskUpdates.Where(tu => tu.ProjectId == projectId && tu.TaskId == taskId).ToList();
        }

        public TaskUserUpdate GetTaskUserUpdate(int projectId, int taskId, int userId, int taskUserUpdateId)
        {
            return _context.TaskUserUpdates.Find(projectId, taskId, userId, taskUserUpdateId);
        }

        public List<TaskUserUpdate> GetTaskUserUpdatesByTask(int projectId, int taskId)
        {
            return _context.TaskUserUpdates.Where(tuu => tuu.ProjectId == projectId && tuu.TaskId == taskId).ToList();
        }



        // public List<Task> GetTasksByProject(int projectId)
        // {
        //     return _context.Tasks.Where(t => t.ProjectId == projectId).ToList();
        // }


        // public List<Task> GetTasksByTaskStatus(int projectId, string taskStatus)
        // {
        //     return _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskStatus == taskStatus).ToList();
        // }

        // public List<Task> GetTasksByTaskType(int projectId, int taskTypeId)
        // {
        //     return _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskTypeId == taskTypeId).ToList();
        // }

        // public List<Task> GetTasksByUrgency(int projectId, string urgency)
        // {
        //     return _context.Tasks.Where(t => t.ProjectId == projectId && t.Urgency == urgency).ToList();
        // }

        
        
        

        

        
    }
}