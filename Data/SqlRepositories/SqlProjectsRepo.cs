using System.Collections.Generic;
using System.Linq;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlProjectsRepo : IProjectsRepo
    {
        private readonly ProjectManagerContext _context;

        public SqlProjectsRepo(ProjectManagerContext context)
        {   
            _context = context;
        }
        public Project GetProjectById(int projectId)
        {
            return _context.Projects.Find(projectId);

        }
    }
}