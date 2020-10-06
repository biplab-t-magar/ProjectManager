using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class TaskComment
    {
        [Required]
        public int TaskCommentId {get; set;}

        [Required]
        public string AppUserId {get; set;}

        [Required]
        public int TaskId {get; set;}        
    
        [Required]
        [MaxLength(300)]
        public string Comment {get; set;}

        [Required]
        public DateTime TimeAdded {get; set;}

        [JsonIgnore]
        public AppUser AppUser {get; set;}

        [JsonIgnore]
        public Task Task {get; set;}
    }
}