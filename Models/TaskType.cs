using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class TaskType
    {   
        
        
        [Required]
        public int TaskTypeId {get; set;}

        [Required]
        public int ProjectId {get; set;}

        [Required]
        [MaxLength(50)]
        public string Name {get; set;}
        [Required]
        public string DefaultUrgency {get; set;}

        [Required]
        public Project Project {get; set;}

        public List<Task> Tasks {get; set;}

    }
}