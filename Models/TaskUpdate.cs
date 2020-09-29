using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [Required]
        public DateTime TimeStamp {get; set;}

        [Required]
        public int UpdaterId {get; set;}
        [MaxLength(50)]
        public string Name {get; set;}
        public string Status {get; set;}

        public string Urgency {get; set;}
        public DateTime? Deadline {get; set;}

        //make tasktype nullable
        public int? TaskTypeId {get; set;}


        [JsonIgnore]
        public TaskType TaskType {get; set;}
        [JsonIgnore]
        public Task Task {get; set;}

    }
}