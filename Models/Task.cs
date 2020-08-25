namespace ProjectManager.Models
{
    public class Task
    {

        //foreign key
        [Required]
        public int ProjectId {get; set;}

        public int TaskId {get; set;}
        [Required]
        public string Name {get; set;}
        
        //enum
        // Unstarted, Started, Suspended, RoadblockEncountered, UnderReview, Completed
        [Required]
        public string TaskStatus {get; set;}

        //enum
        //default is Medium
        //Low, Medium, High
        [Required]
        public string Urgency {get; set;}

        //optional
        public int TaskTypeId {get; set;}
        
        //foreign key
        [Required]
        public int ProjectId {get; set;}

        //foreign key
        [Required]
        public int MilestoneId {get; set;}
        
    }
}