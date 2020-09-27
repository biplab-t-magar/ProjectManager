using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class Project
    {
        [Key]
        public int ProjectId {get; set;}
        [Required]
        public string Name {get; set;}
        [Required]
        public string TimeCreated {get; set;}

        public string Description {get; set;}

        public List<Task> Tasks {get; set;}
        // public List<TaskType> TaskTypes {get; set;}
        public List<ProjectUser> ProjectUsers {get; set;}
        public List<ProjectUpdate> ProjectUpdates {get; set;}
    }
}