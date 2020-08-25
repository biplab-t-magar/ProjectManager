using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class ProjectUser
    {
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public Project Project {get; set;}
        [Required]
        public int UserId {get; set;}
        [Required]
        public User User{get; set;}

        [Required]
        public ProjectUserRole ProjectUserRole {get; set;} 
        
    }
}