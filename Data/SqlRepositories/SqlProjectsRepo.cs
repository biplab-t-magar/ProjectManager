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
            List<Project> projects = _context.Projects.ToList();
            //search through all the entries
            for(int i = 0; i < projects.Count; i++)
            {
                if(projects[i].ProjectId == projectId)
                {
                    return projects[i];
                }
            }
            //return null if no match is found
            return null; 

        }
    }
}