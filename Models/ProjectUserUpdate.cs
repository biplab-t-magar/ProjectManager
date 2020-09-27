using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class ProjectUserUpdate
    {
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public int UserId {get; set;}
        [Required]
        ProjectUser ProjectUser{get; set;}

        [Required]
        public int ProjectUserUpdateId {get; set;}
        public int UpdatedByUserId {get; set;}
        [Required]
        public User UpdatedBy {get; set;}
        [Required]
        public DateTime TimeStamp {get; set;}
        
        public string Role {get; set;}

        public DateTime TimeRemoved {get; set;}

    }
}