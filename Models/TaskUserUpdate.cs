using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class TaskUserUpdate
    {
    
        [Required]
        public int ProjectId {get; set;}

        [Required]
        public int TaskId {get; set;}
        
        [Required]
        public int UserId {get; set;}
        
        [Required]
        public int TaskUserUpdateId {get; set;}

        [Required]
        public int UpdaterId {get; set;}
        [Required]
        public DateTime TimeStamp {get; set;}
        //make TimeRemoved nullable
        public DateTime? TimeRemoved {get; set;}

        public TaskUser TaskUser {get; set;}


    }
}