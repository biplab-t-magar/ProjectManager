/*ProjectUser.cs
This file contains the ProjectUser class. 
The ProjectUser class is one of the Model classes used by the ProjectManager web application. Each model in the application represents one entity 
in the application. This entity is used in the client side, on the server side, and is also used to define and create database schemes and relationships.

The ProjectUser class represents the inclusion of a user in a project
In the database level, the ProjectUser class represents a join table in a many-to-many relationship between the Project table and the User table.
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class ProjectUser
    {
        //the id of the project that is a part of the project-user relationship
        [Required]
        public int ProjectId {get; set;}

        //the id of the user who is a part of the project-user relationship
        [Required]
        public string AppUserId {get; set;}

        //the role the user has in the project
        //Could be: Administrator or Member
        [Required]
        public string Role {get; set;}

        //the time the user was added to the project
        [Required]
        public DateTime TimeAdded {get; set;}

        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public AppUser AppUser {get; set;}

        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public Project Project {get; set;}

    }
}