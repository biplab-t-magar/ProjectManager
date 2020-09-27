using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class ProjectUserUpdate
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
        public int ProjectUserUpdateId {get; set;}
        public int UpdatedByUserId {get; set;}
        [Required]
        public User UpdatedBy {get; set;}
        [Required]
        public string TimeStamp {get; set;}
        
        public string Role {get; set;}

        public string TimeRemoved {get; set;}

        
    }
}