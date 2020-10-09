/*Project.cs
This file contains the Project class. 
The Project class is one of the Model classes used by the ProjectManager web application. Each model in the application represents one entity 
in the application. This entity is used in the client side, on the server side, and is also used to define and create database schemes and relationships.

The Project class represents a project in the ProjectManager web application. 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class Project
    {
        //The unique identifier of a project; used as the primary key in the database table
        [Required]
        public int ProjectId {get; set;}

        //the name of the project
        [Required]
        [MaxLength(100)]
        public string Name {get; set;}

        //the time the project was created in
        [Required]
        public DateTime TimeCreated {get; set;}

        //a description of the project
        [MaxLength(500)]
        public string Description {get; set;}

        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public List<ProjectUser> ProjectUsers {get; set;}

        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public List<Task> Tasks {get; set;}

        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public List<TaskType> TaskTypes {get; set;}

    }
}