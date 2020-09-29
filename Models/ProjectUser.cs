using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class ProjectUser
    {
        [Required]
        public int ProjectId {get; set;}

        [Required]
        public int UserId {get; set;}

        //Could be: Manager, Member, Spectator
        [Required]
        public string Role {get; set;}

        [Required]
        public DateTime TimeAdded {get; set;}

        [JsonIgnore]
        [Required]
        public User User {get; set;}

        [JsonIgnore]
        public Project Project {get; set;}

    }
}