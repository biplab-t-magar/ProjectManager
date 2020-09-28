using System.Collections.Generic;
using System.Linq;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlTaskUsersRepo : ITaskUsersRepo
    {
        private readonly ProjectManagerContext _context;

        public SqlTaskUsersRepo(ProjectManagerContext context)
        {
            _context = context;
        }
        public List<TaskUser> GetTasksByUser(int userId)
        {
            return _context.TaskUsers.Where(tu => tu.UserId == userId).ToList();
        }

        public List<TaskUser> GetTasksInProjectByUser(int projectId, int userId)
        {
            return _context.TaskUsers.Where(tu => tu.ProjectId == projectId && tu.UserId == userId).ToList();
        }

        public List<User> GetUsersByTask(int projectId, int taskId)
        {
            //first get a list of all TaskUsers entries that have the given project id and task id
            var taskUsers = _context.TaskUsers.Where(tu => tu.ProjectId == projectId && tu.TaskId == taskId).ToList();

            List<User> users = new List<User>();
            //now loop through all the taskUsers objects and find User objects with matching UserId attribute
            for(int i = 0; i < taskUsers.Count; i++)
            {
                users.Add(_context.Users.Find(taskUsers[i]));
            }
            return users;
        }
    }
}