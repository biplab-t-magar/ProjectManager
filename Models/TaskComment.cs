/*TaskComment.cs
This file contains the TaskComment class. 
The TaskComment class is one of the Model classes used by the ProjectManager web application. Each model in the application represents one entity 
in the application. This entity is used in the client side, on the server side, and is also used to define and create database schemes and relationships.

The TaskComment class represents a comment made on a task by a user in the ProjectManager web application. 
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class TaskComment
    {
        //the unique identifier of a task comment; used as primary key in the database table
        [Required]
        public int TaskCommentId {get; set;}

        //the id of the user who made the comment
        [Required]
        public string AppUserId {get; set;}

        //the id of the task in which the comment was posted
        [Required]
        public int TaskId {get; set;}        
    
        //the actual content of the comment
        [Required]
        [MaxLength(300)]
        public string Comment {get; set;}

        //the time the comment was added
        [Required]
        public DateTime TimeAdded {get; set;}

        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public AppUser AppUser {get; set;}

        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public Task Task {get; set;}
    }
}