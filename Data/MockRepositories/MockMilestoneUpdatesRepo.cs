using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.MockRepositories
{
    public class MockMilestoneUpdatesRepo : IMilestoneUpdatesRepo
    {
        private List<MilestoneUpdate> milestoneUpdates; 
        MockMilestoneUpdatesRepo()
        {
            milestoneUpdates = new List<MilestoneUpdate>
            {
                new MilestoneUpdate{ProjectId=1, MilestoneId=1, TimeOfUpdate="09/15/2020 23:05:03", MilestoneStatus="In Progress"},
                new MilestoneUpdate{ProjectId=1, MilestoneId=1, TimeOfUpdate="09/29/2020 19:05:47", MilestoneStatus="Complete"},
                new MilestoneUpdate{ProjectId=1, MilestoneId=2, TimeOfUpdate="09/29/2020 19:05:55", MilestoneStatus="In Progress"},
                new MilestoneUpdate{ProjectId=2, MilestoneId=1, TimeOfUpdate="07/02/2020 19:13:55", MilestoneStatus="In Progress"},
            };
        }
        public IEnumerable<MilestoneUpdate> GetAllMilestoneUpdates(int projectId, int milestoneId)
        {
            List<MilestoneUpdate> milestoneUpdatesToReturn = new List<MilestoneUpdate>();
            
            for(int i = 0; i < milestoneUpdates.Count; i++)
            {
                if(milestoneUpdates[i].ProjectId == projectId && milestoneUpdates[i].MilestoneId == milestoneId)
                {
                    milestoneUpdatesToReturn.Add(milestoneUpdates[i]);
                }  
                
            }
            return milestoneUpdatesToReturn;
        }
    }
}