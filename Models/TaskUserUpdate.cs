/*TaskUserUpdate.cs
This file contains the TaskUserUpdate class. 
The TaskUserUpdate class is one of the Model classes used by the ProjectManager web application. Each model in the application represents one entity 
in the application. This entity is used in the client side, on the server side, and is also used to define and create database schemes and relationships.

The TaskUserUpdate class represents an update in the users assigned to a task in the ProjectManager Web Application
*/

using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class TaskUserUpdate
    {
        //the primary identifier of the TaskUserUpdate entry (the primary key in the table)
        [Required]
        public int TaskUserUpdateId {get; set;}

        //the task with which the update is associated with
        [Required]
        public int TaskId {get; set;}
        
        //the user who has been added/removed from the task
        [Required]
        public string AppUserId {get; set;}
        
        //the user who added/removed the other user from the task
        [Required]
        public string UpdaterId {get; set;}
        //the time that the user was added to the task
        public DateTime? TimeAdded {get; set;}
        //the time that the user was removed from the task
        //make TimeRemoved nullable
        public DateTime? TimeRemoved {get; set;}

    }
}