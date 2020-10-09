/*TaskUpdate.cs
This file contains the TaskUpdate class. 
The TaskUpdate class is one of the Model classes used by the ProjectManager web application. Each model in the application represents one entity 
in the application. This entity is used in the client side, on the server side, and is also used to define and create database schemes and relationships.

The TaskUpdate class represents an update made to a task by a user in the ProjectManager web application. 
In the application and the database, the Name, TaskStatus, Urgency, and TaskType attributes are uninitialized by default. If any of these attributes is initialized to 
a certain value, the web application interprets that particular attribute as the attribute that was updated in the task
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class TaskUpdate
    {

        //the unique identifier for a particular task update. represents the primary key in the corresponding database table
        [Required]
        public int TaskUpdateId {get; set;}
        //The id of the task that is updated
        //foreign key
        [Required]
        public int TaskId {get; set;}

        //The time when the task is updated
        [Required]
        public DateTime TimeStamp {get; set;}

        //the id of the user who made the task updates
        [Required]
        public string UpdaterId {get; set;}

        //The 
        [MaxLength(50)]
        public string Name {get; set;}
        public string TaskStatus {get; set;}

        public string Urgency {get; set;}

        //make tasktype nullable
        public int? TaskTypeId {get; set;}


        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public TaskType TaskType {get; set;}
        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public Task Task {get; set;}

    }
}