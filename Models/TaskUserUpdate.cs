using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class TaskUserUpdate
    {
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public Project Project {get; set;}
        [Required]
        public int TaskId {get; set;}
        [Required]
        public Task Task {get; set;}
        [Required]
        public int UserId {get; set;}
        [Required]
        public User User {get; set;}
        [Required]
        public int TaskUserUpdateId {get; set;}

        [Required]
        public int UpdatedByUserId {get; set;}
        public User UpdatedBy{get; set;}
        [Required]
        public string TimeStamp {get; set;}
        public string TimeRemoved {get; set;}

    }
}