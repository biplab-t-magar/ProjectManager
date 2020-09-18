using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IMilestoneUpdatesRepo
    {
        IEnumerable<MilestoneUpdate> GetAllMilestoneUpdates(int projectId, int milestoneId);
    } 
}