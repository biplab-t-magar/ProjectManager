using System.Collections.Generic;
using System.Linq;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlTaskUserUpdatesRepo : ITaskUserUpdatesRepo
    {
        private readonly ProjectManagerContext _context;

        public SqlTaskUserUpdatesRepo(ProjectManagerContext context)
        {
            _context = context;
        }
        public List<TaskUserUpdate> GetTaskUserUpdates(int projectId, int taskId)
        {
            return _context.TaskUserUpdates.Where(tuu => tuu.ProjectId == projectId && tuu.TaskId == taskId).ToList();
        }
    }
}