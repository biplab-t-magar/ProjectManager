namespace ProjectManager.Models
{
    public class TaskUser
    {
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public int TaskId {get; set;}
        [Required]
        public int UserId {get; set;}

    }
}