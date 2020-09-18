using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IMilestonesRepo
    {
        IEnumerable<Milestone> GetMilestonesByProject(int projectId);
        Milestone GetMilestoneById(int projectId, int milestoneId);
    } 
}