using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class TaskType
    {   
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public Project Project{get; set;}
        [Required]
        public int TaskTypeId {get; set;}
        [Required]
        public string Name {get; set;}

        //enum
        //set default value to be Medium
        public string DefaultUrgency {get; set;}

        public List<Task> Tasks {get; set;}
        public List<TaskTypeUpdate> TaskTypeUpdate{get; set;}

        
    }
}