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
        public int TaskId {get; set;}
        [Required]
        public int UserId {get; set;}

        [Required]
        public DateTime TimeAdded {get; set;}

        //this should be nullabe because value is only entered once a user is removed from a task
        public DateTime? TimeRemoved {get; set;}

        public User User {get; set;}
        public Task Task {get; set;}

        public List<TaskUserUpdate> TaskUserUpdates {get; set;}
    }
}