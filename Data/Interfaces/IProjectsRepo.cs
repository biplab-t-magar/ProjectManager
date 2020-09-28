using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IProjectsRepo
    {
        Project GetProjectById(int projectId);
    }
}