using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class ProjectUpdate
    {
        
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public Project Project {get; set;}
        [Required]
        public int ProjectUpdateId {get; set;}
        [Required]
        public int UpdatedByUserId {get; set;}
        [Required]
        public User UpdatedBy {get; set;}
        [Required]
        public DateTime TimeStamp {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}



    }
}