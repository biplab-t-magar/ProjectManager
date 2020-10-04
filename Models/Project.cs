using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class Project
    {
        [Required]
        public int ProjectId {get; set;}
        [Required]
        [MaxLength(250)]
        public string Name {get; set;}
        [Required]
        public DateTime TimeCreated {get; set;}

        [MaxLength(300)]
        public string Description {get; set;}

        [JsonIgnore]
        public List<ProjectUser> ProjectUsers {get; set;}
        [JsonIgnore]
        public List<Task> Tasks {get; set;}
        [JsonIgnore]
        public List<TaskType> TaskTypes {get; set;}

    }
}