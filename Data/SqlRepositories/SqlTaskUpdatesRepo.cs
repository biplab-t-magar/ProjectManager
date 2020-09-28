using System.Collections.Generic;
using System.Linq;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlTaskUpdatesRepo : ITaskUpdatesRepo
    {
        private readonly ProjectManagerContext _context;

        public SqlTaskUpdatesRepo(ProjectManagerContext context)
        {
            _context = context;
        }
        public List<TaskUpdate> GetTaskUpdates(int projectId, int taskId)
        {
            return _context.TaskUpdates.Where(tu => tu.ProjectId == projectId && tu.TaskId == taskId).ToList();
        }
    }
}