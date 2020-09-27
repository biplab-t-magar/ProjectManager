using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IProjectUpdatesRepo
    {
        IEnumerable<Project> GetProjectUpdates(int projectId);
    }
}