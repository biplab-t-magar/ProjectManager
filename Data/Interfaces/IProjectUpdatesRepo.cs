using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IProjectUpdatesRepo
    {
        List<Project> GetProjectUpdates(int projectId);
    }
}