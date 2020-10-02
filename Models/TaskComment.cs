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

        [JsonIgnore]
        public AppUser AppUser {get; set;}
        [JsonIgnore]
        public Project Project {get; set;}
        [JsonIgnore]
        public Task Task {get; set;}
    }
}