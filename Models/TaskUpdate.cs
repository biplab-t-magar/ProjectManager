using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class TaskUpdate
    {
        //foreign key
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public int TaskId {get; set;}
        [Required]
        public Task Task {get; set;}
        [Required]
        public int TaskUpdateId {get; set;}

        public string Status {get; set;}
        [Required]
        public DateTime TimeStamp {get; set;}

        [Required]
        public int UpdatedByUserId {get; set;}
        public User UpdatedBy {get; set;}
        public string Name {get; set;}
        
        public string TaskStatus {get; set;}

        public string Urgency {get; set;}

        public int TaskTypeId {get; set;}

    }
}