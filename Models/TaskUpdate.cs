namespace ProjectManager
{
    public class TaskUpdate
    {
        //foreign key
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public int TaskId {get; set;}
        [Required]
        public string Status {get; set;}
        [Required]
        public string Time {get; set;}
    }
}