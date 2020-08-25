namespace ProjectManager.Models
{
    public class Milestone
    {

        //foreign key
        [Required]
        public int ProjectId {get; set}
        public int MilestoneId {get; set;}
        [Required]
        public string Name {get; set;}

        //enum
        //Unstarted, In Progress, Completed
        [Required]
        public string MilestoneStatus {get; set;}

        

    }
}