using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class User
    {
        [Key]
        public int UserId;
        [Required]
        public string FirstName {get; set;}
        [Required]
        public string LastName {get; set;}
        public string MiddleName {get; set;}
        
        //a user can only have one organization
        //if user works for two or more organizations, he will need separate accounts for each organization
        public int OrganizationId {get; set;}
        public Organization Organization {get; set;}


        public List<TaskType> TaskTypes {get; set;}
        public List<ProjectUser> ProjectUsers {get; set;}
        public List<TaskUser> TaskUsers {get; set;}
    }
}