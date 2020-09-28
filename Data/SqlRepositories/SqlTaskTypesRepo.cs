using System.Collections.Generic;
using System.Linq;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlTaskTypesRepo : ITaskTypesRepo
    {
        private readonly ProjectManagerContext _context;

        public SqlTaskTypesRepo(ProjectManagerContext context)
        {
            _context = context;
        }

        public TaskType GetTaskTypeById(int taskTypeId)
        {
            return _context.TaskTypes.Find(taskTypeId);
        }

        public List<TaskType> GetTaskTypesByDefaultUrgency(int projectId, string urgency)
        {
            return _context.TaskTypes.Where(tt => tt.ProjectId == projectId && tt.DefaultUrgency == urgency).ToList();
        }

        public List<TaskType> GetTaskTypesByProject(int projectId)
        {
            return _context.TaskTypes.Where(tt => tt.ProjectId == projectId).ToList();
        }
    }   
}