namespace ProjectManager
{
    public class MilestoneUpdate
    {
        //foreign keys
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public int MileStoneId {get; set;}
        [Required]
        public string Status {get; set;}
        [Required]
        public string Time {get; set;}
    }
}