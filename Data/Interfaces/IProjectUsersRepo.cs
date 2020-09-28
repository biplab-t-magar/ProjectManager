using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IProjectUsersRepo
    {
        List<User> GetUsersByProject(int projectId);
        List<Project> GetProjectsByUser(int userId);
        List<User> GetUsersByRole(int projectId, string role);
    }
}