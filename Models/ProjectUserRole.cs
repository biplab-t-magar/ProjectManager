namespace ProjectManager.Models
{
    public class ProjectUserRole
    {
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public int UserId {get; set;}
        [Required]
        public ProjectUser ProjectUser {get; set;}
        [Required]
        public string Role {get; set;}
    }
}