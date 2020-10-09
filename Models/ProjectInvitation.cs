/*ProjectInvitation.cs
This file contains the ProjectInvitation class. 
The ProjectInvitation class is one of the Model classes used by the ProjectManager web application. Each model in the application represents one entity 
in the application. This entity is used in the client side, on the server side, and is also used to define and create database schemes and relationships.

The ProjectManager class represents a invitation to a project by one AppUser to another in the ProjectManager web application. 
*/
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class ProjectInvitation
    {
        //the unique id that identifies the project invitation
        [Required]
        public int ProjectInvitationId {get; set;}

        //the id of the project to which the user is invited
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public string InviterId {get; set;}
        [Required]
        public string InviteeId {get; set;}
        
        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public Project Project {get; set;}
        
        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public AppUser Inviter {get; set;}
        
        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public AppUser Invitee {get; set;}

    }
}