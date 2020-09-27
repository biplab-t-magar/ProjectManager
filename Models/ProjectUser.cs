using System;
using System.Collections.Generic;
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

        //Could be: Manager, Member, Spectator
        [Required]
        public string Role {get; set;}

        [Required]
        public DateTime TimeAdded {get; set;}

        public DateTime TimeRemoved {get; set;}

        
        List<ProjectUserUpdate> ProjectUserUpdates {get; set;}
        
    }
}