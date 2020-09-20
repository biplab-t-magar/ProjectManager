using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class Project
    {
        [Key]
        public int ProjectId {get; set;}
        [Required]
        public string Name {get; set;}
        [Required]
        public string DateCreated {get; set;}
        public string DateTerminated {get; set;}

        //foreign key
        public int OrganizationId {get; set;}
        public Organization Organization {get; set;}

        public List<Milestone> Milestones {get; set;}
        public List<Task> Tasks {get; set;}
        // public List<TaskType> TaskTypes {get; set;}
        public List<ProjectUser> ProjectUsers {get; set;}
    }
}