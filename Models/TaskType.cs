using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Project Project {get; set;}
        [JsonIgnore]
        public List<Task> Tasks {get; set;}

    }
}