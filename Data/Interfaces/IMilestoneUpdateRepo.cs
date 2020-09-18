using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IMilestoneUpdateRepo
    {
        IEnumerable<MilestoneUpdate> GetAllMilestoneUpdates(int projectId, int milestoneId);
    } 
}