using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class User
    {
        public int UserId {get; set;}
        [Required]
        public string FirstName {get; set;}
        [Required]
        public string LastName {get; set;}
        public string MiddleName {get; set;}
        [Required]
        public string Email {get; set;}
        public string Bio{get; set;}

        public List<ProjectUser> ProjectUsers {get; set;}
        public List<TaskUser> TaskUsers {get; set;}
    }
}