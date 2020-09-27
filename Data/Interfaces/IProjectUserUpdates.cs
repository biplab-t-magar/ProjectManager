using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IProjectUserUpdatesRepo
    {
        List<ProjectUser> GetProjectUserUpdates(int projectId);
    }
}