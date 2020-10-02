using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class ProjectInvitation
    {
        [Required]
        public int ProjectInvitationId {get; set;}
        [Required]
        public int ProjectId {get; set;}
        [Required]
        [MaxLength(250)]
        public string InviterId {get; set;}
        [Required]
        public string InviteeId {get; set;}
        [Required]
        public DateTime Timestamp {get; set;}

        [JsonIgnore]
        public Project Project {get; set;}
        [JsonIgnore]
        public AppUser Inviter {get; set;}
        [JsonIgnore]
        public AppUser Invitee {get; set;}

    }
}