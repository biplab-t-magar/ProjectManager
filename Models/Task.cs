using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class Task
    {

        //foreign key
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public Project Project {get; set;}

        public int TaskId {get; set;}
        [Required]
        public string Name {get; set;}
        
        //enum
        // Unstarted, Started, Suspended, RoadblockEncountered, UnderReview, Completed
        [Required]
        public string TaskStatus {get; set;}

        //enum
        //default is Medium
        //Low, Medium, High
        [Required]
        public string Urgency {get; set;}

        //optional
        public int TaskTypeId {get; set;}
        public TaskType TaskType {get; set;}

        //foreign key
        [Required]
        public int MilestoneId {get; set;}
        [Required]
        public Milestone Milestone {get; set;}

        public List<TaskUpdate> TaskUpdates {get; set;}
        public List<TaskUser> TaskUsers {get; set;}
    }
}