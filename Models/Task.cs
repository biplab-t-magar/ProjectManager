using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class Task
    {
        //foreign key
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public Project Project {get; set;}

        public int TaskId {get; set;}
        [Required]
        public string Name {get; set;}
        
        // Not Started, Started, Suspended, Roadblock Encountered, Under Review, Completed
        [Required]
        public string TaskStatus {get; set;}

        //enum
        //default is Medium
        //Low, Medium, High
        [Required]
        public string Urgency {get; set;}
        [Required]
        public DateTime TimeCreate {get; set;}

        [Required]
        //default task type should be set to general
        public int TaskTypeId {get; set;}
        [Required]
        public TaskType TaskType {get; set;}

        public List<TaskUpdate> TaskUpdates {get; set;}
        public List<TaskUser> TaskUsers {get; set;}
    }
}