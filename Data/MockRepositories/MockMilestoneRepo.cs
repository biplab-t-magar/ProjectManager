using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public class MockMilestoneRepo : IMilestoneRepo
    {
        private List<Milestone> milestones;

        public MockMilestoneRepo()
        {
            milestones = new List<Milestone> 
            {
                new Milestone{ProjectId=1, MilestoneId=1, Name="Finish Project Design", MilestoneStatus="Completed"},
                new Milestone{ProjectId=1, MilestoneId=2, Name="Finish Project Implementation", MilestoneStatus="In Progress"},
                new Milestone{ProjectId=1, MilestoneId=3, Name="Finish Project Design", MilestoneStatus="Not Started"},
                new Milestone{ProjectId=2, MilestoneId=1, Name="Create business plan", MilestoneStatus="In Progress"},
                new Milestone{ProjectId=2, MilestoneId=2, Name="Figure out logistics", MilestoneStatus="Not Started"},
                new Milestone{ProjectId=2, MilestoneId=3, Name="Register business", MilestoneStatus="Not Started"},
                new Milestone{ProjectId=2, MilestoneId=4, Name="Higher Employees", MilestoneStatus="Not Started"},
            };
        }

        public IEnumerable<Milestone> GetAllMilestonesInProject(int projectId)
        {
            List<Milestone> projectMilestones = new List<Milestone>();
            
            for(int i = 0; i < milestones.Count; i++)
            {
                if(milestones[i].ProjectId == projectId)
                {
                    projectMilestones.Add(milestones[i]);
                }  
                
            }
            return projectMilestones;
        }

        public Milestone GetMilestoneById(int projectId, int milestoneId)
        {
            for(int i = 0; i < milestones.Count; i++) 
            {
                if(milestones[i].ProjectId == projectId && milestones[i].MilestoneId == milestoneId)
                {
                    return milestones[i];
                }
            }
            return null;
        }
    }
}

