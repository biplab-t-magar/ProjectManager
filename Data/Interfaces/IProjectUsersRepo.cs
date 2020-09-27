using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IProjectUsersRepo
    {
        List<ProjectUser> GetUsersByProject(int projectId);
        List<ProjectUser> GetProjectsByUser(int userId);
        List<ProjectUser> GetProjectUsersByRole(int projectId, int userId, string role);
    }
}