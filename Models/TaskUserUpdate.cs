using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class TaskUserUpdate
    {
        [Required]
        public int TaskUserUpdateId {get; set;}

        [Required]
        public int TaskId {get; set;}
        
        [Required]
        public string AppUserId {get; set;}
        
        [Required]
        public string UpdaterId {get; set;}
        public DateTime? TimeAdded {get; set;}
        //make TimeRemoved nullable
        public DateTime? TimeRemoved {get; set;}

    }
}