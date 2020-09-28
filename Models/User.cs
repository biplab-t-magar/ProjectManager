using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class User
    {
        public int UserId {get; set;}
        [Required]
        [MaxLength(50)]
        public string FirstName {get; set;}
        [Required]
        [MaxLength(50)]
        public string LastName {get; set;}
        public string MiddleName {get; set;}
        [Required]
        [MaxLength(50)]
        public string Email {get; set;}
        [MaxLength(300)]
        public string Bio{get; set;}

        List<ProjectUser> ProjectUsers {get; set;}
        List<TaskUser> TaskUsers {get; set;}

    }
}