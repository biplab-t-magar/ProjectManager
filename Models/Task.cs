using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class Task
    {
        [Required]
        public int TaskId {get; set;}

        [Required]
        public int ProjectId {get; set;}
        [MaxLength(50)]
        [Required]
        public string Name {get; set;}
        [MaxLength(500)]
        [Required]
        public string Description {get; set;}
        
        // Open, Suspended, Roadblock Encountered, Under Review, Completed
        [Required]
        public string TaskStatus {get; set;}

        //default is Medium
        //Low, Medium, High
        [Required]
        public string Urgency {get; set;}
        [Required]
        public DateTime TimeCreated {get; set;}

        public int? TaskTypeId {get; set;}
        
        [JsonIgnore]
        public TaskType TaskType {get; set;}
        
        [JsonIgnore]
        public List<TaskUser> TaskUsers {get; set;}

    }
}