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
        public Task GetTaskById(int taskId)
        {
            return _context.Tasks.Find(taskId);
        }

        public List<AppUser> GetTaskUsers(int taskId)
        {
            //first get all the TaskUser entries with the matching taskId
            var taskUsers = _context.TaskUsers.Where(tu => tu.TaskId == taskId).ToList();

            List<AppUser> users = new List<AppUser>();

            //loop through all taskUser entries and store their corresponding User entries
            for(int i = 0; i < taskUsers.Count; i++)
            {
                users.Add(_context.Users.Find(taskUsers[i].AppUserId));
            }

            return users;
        }

        public TaskUpdate GetTaskUpdate(int taskUpdateId)
        {
            return _context.TaskUpdates.Find(taskUpdateId);
        }

        public List<TaskUpdate> GetTaskUpdatesByTask(int taskId)
        {
            return _context.TaskUpdates.Where(tu => tu.TaskId == taskId).ToList();
        }

        public TaskUserUpdate GetTaskUserUpdate(int taskUserUpdateId)
        {
            return _context.TaskUserUpdates.Find(taskUserUpdateId);
        }

        public List<TaskUserUpdate> GetTaskUserUpdatesByTask(int taskId)
        {
            return _context.TaskUserUpdates.Where(tuu => tuu.TaskId == taskId).ToList();
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