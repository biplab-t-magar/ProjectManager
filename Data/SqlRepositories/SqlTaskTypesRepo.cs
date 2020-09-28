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
        
    }   
}