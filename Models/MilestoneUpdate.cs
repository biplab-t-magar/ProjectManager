namespace ProjectManager.Models
{
    public class MilestoneUpdate
    {
        //foreign keys: (projectId, MilestoneId), time
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public int MilestoneId {get; set;}
        [Required]
        public Milestone Milestone {get; set;}
        [Required]
        public string Time {get; set;}

        [Required]
        public string MilestoneStatus {get; set;}
        
    }
}