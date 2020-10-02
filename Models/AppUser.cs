using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace ProjectManager.Models
{
    //the user data and profile for our application
    public class AppUser : IdentityUser
    {
        // public int UserId {get; set;}
        [Required]
        [MaxLength(50)]
        public string FirstName {get; set;}
        [Required]
        [MaxLength(50)]
        public string LastName {get; set;}
        // [MaxLength(50)]
        // public string Email {get; set;}
        [MaxLength(300)]
        public string Bio{get; set;}
[       JsonIgnore]    
        List<ProjectUser> ProjectUsers {get; set;}
        [JsonIgnore]
        List<TaskUser> TaskUsers {get; set;}

    }
}