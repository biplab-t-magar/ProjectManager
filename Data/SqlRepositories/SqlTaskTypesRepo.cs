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
        

        public TaskType CreateTaskType(int projectId, string name)
        {
            TaskType taskType = new TaskType() {ProjectId = projectId, Name = name};
            _context.Add(taskType);
            return taskType;
        }


        public void DeleteTaskType(int taskTypeId)
        {
            var taskType = GetTaskTypeById(taskTypeId);
            _context.TaskTypes.Remove(taskType);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

    }   
}