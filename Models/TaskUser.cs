/*TaskUser.cs
This file contains the TaskUser class. 
The TaskUser class is one of the Model classes used by the ProjectManager web application. Each model in the application represents one entity 
in the application. This entity is used in the client side, on the server side, and is also used to define and create database schemes and relationships.

The TaskUser class represents an assignment of a user to a task in the ProjectManager web application. 
In the database level, the TaskUser class represents a join table in a many-to-many relationship between the Task table and the User table.
*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class TaskUser
    {
        //The task that is part of this task-user relationship 
        [Required]
        public int TaskId {get; set;}
        //the user who is part of this task-user relationship
        [Required]
        public string AppUserId {get; set;}

        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public AppUser AppUser {get; set;}
        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public Task Task {get; set;}
        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public List<TaskUserUpdate> TaskUserUpdates {get; set;}
    }
}