/*TaskComment.cs
This file contains the TaskComment class. 
The TaskComment class is one of the Model classes used by the ProjectManager web application. Each model in the application represents one entity 
in the application. This entity is used in the client side, on the server side, and is also used to define and create database schemes and relationships.

The TaskType class represents a category of tasks defined by an administrator in a project.
*/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class TaskType
    {   
        
        //the unique identifier for the task type, represents the primary key in the database
        [Required]
        public int TaskTypeId {get; set;}

        //the id of the project that this task type is defined in
        [Required]
        public int ProjectId {get; set;}

        //the name of the task type
        [Required]
        [MaxLength(50)]
        public string Name {get; set;}

        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [Required]
        [JsonIgnore]
        public Project Project {get; set;}
        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        public List<Task> Tasks {get; set;}

    }
}