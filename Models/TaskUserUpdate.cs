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
        public string UserId {get; set;}
        
        [Required]
        public int UpdaterId {get; set;}
        [Required]
        public DateTime TimeStamp {get; set;}
        //make TimeRemoved nullable
        public DateTime? TimeRemoved {get; set;}
        [JsonIgnore]
        public TaskUser TaskUser {get; set;}


    }
}