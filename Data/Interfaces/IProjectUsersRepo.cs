using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IProjectUsersRepo
    {
        IEnumerable<ProjectUser> GetUsersByProject(int projectId);
        IEnumerable<ProjectUser> GetProjectsByUser(int userId);
        ProjectUser GetProjectUser(int projectId, int userId);
    }
}