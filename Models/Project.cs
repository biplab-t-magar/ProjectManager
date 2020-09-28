using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public DateTime Deadline {get; set;}
        [MaxLength(700)]
        public string Description {get; set;}

        public List<ProjectUser> ProjectUsers {get; set;}
        public List<Task> Tasks {get; set;}
        public List<TaskType> TaskTypes {get; set;}

    }
}