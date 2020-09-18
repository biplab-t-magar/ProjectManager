using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IMilestoneRepo
    {
        IEnumerable<Milestone> GetAllMilestonesInProject(int projectId);
        Milestone GetMilestoneById(int projectId, int milestoneId);
    } 
}