using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class TaskUser
    {
        [Required]
        public int TaskId {get; set;}
        [Required]
        public string AppUserId {get; set;}

        [JsonIgnore]
        public AppUser AppUser {get; set;}
        [JsonIgnore]
        public Task Task {get; set;}
        [JsonIgnore]
        public List<TaskUserUpdate> TaskUserUpdates {get; set;}
    }
}