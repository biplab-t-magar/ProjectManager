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
        public int TaskUpdateId {get; set;}

        public string Status {get; set;}
        [Required]
        public DateTime TimeStamp {get; set;}

        [Required]
        public int UpdatedByUserId {get; set;}
        [MaxLength(50)]
        public string Name {get; set;}
        
        public string Urgency {get; set;}

        public int TaskTypeId {get; set;}
        public TaskType TaskType {get; set;}
        public DateTime Deadline {get; set;}

        public Task Task {get; set;}

    }
}