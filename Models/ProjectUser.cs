namespace ProjectManager
{
    public class ProjectUser
    {
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public int UserId {get; set;}
    }
}