using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class TaskTypeUpdate
    {   
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public Project Project{get; set;}
        [Required]
        public int TaskTypeId {get; set;}

        [Required]
        public int TaskTypeUpdateId {get; set;}
        [Required]
        public int UpdatedByUserId {get; set;}
        [Required]
        public User UpdatedBy {get; set;}
        [Required]
        public string TimeStamp {get; set;}


        public string Name {get; set;}

        public string DefaultUrgency {get; set;}
        
    }
}