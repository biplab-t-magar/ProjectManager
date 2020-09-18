using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IProjectsRepo
    {
        IEnumerable<Project> GetProjectsByOrganization(int organizationId);
        Project GetProjectById(int projectId);
    }
}