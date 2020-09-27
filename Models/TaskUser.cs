using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class TaskUser
    {
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public Project Project {get; set;}
        [Required]
        public int TaskId {get; set;}
        [Required]
        public Task Task {get; set;}
        [Required]
        public int UserId {get; set;}
        [Required]
        public User User {get; set;}

        [Required]
        public DateTime TimeAdded {get; set;}

        public DateTime TimeRemoved {get; set;}

        List<TaskUserUpdate> TaskUserUpdates {get; set;}

    }
}