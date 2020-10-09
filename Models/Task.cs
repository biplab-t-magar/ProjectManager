/*Task.cs
This file contains the Task class. 
The Task class is one of the Model classes used by the ProjectManager web application. Each model in the application represents one entity 
in the application. This entity is used in the client side, on the server side, and is also used to define and create database schemes and relationships.

The Task class represents a task in the ProjectManager web application. 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class Task
    {
        //the unique identifier of a task; used as the primary key in the database table
        [Required]
        public int TaskId {get; set;}

        //the id of the project that task is a part of
        [Required]
        public int ProjectId {get; set;}

        //the name of the task
        [MaxLength(50)]
        [Required]
        public string Name {get; set;}

        //the description about the task
        [MaxLength(500)]
        [Required]
        public string Description {get; set;}
        
        //the current task status of the task
        //In the application, values can be set to one of the follow
        // Open, Suspended, Roadblock Encountered, Under Review, Completed
        [Required]
        public string TaskStatus {get; set;}

        //the urgency of the task
        //default is Medium
        //Low, Medium, High
        [Required]
        public string Urgency {get; set;}

        //the time the task was created in
        [Required]
        public DateTime TimeCreated {get; set;}

        //the id of the task type of the task
        public int? TaskTypeId {get; set;}
        
        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public TaskType TaskType {get; set;}
        
        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public List<TaskUser> TaskUsers {get; set;}

    }
}