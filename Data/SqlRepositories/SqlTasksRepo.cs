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

        public List<Task> GetTasksByProject(int projectId)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId).ToList();
        }

        public List<Task> GetTasksByTaskStatus(int projectId, string taskStatus)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskStatus == taskStatus).ToList();
        }

        public List<Task> GetTasksByTaskType(int projectId, int taskTypeId)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId && t.TaskTypeId == taskTypeId).ToList();
        }

        public List<Task> GetTasksByUrgency(int projectId, string urgency)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId && t.Urgency == urgency).ToList();
        }
    }
}