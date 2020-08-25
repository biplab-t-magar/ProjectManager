namespace ProjectManager.Models
{
    public class TaskType
    {
        public int TaskTypeId {get; set;}
        [Required]
        public string Name {get; set;}

        //enum
        //set default value to be Medium
        public string Urgency {get; set;}


        //Validation Required: At least one of the three id's below MUST be not NULL


        //Validation Required: If either ProjectId or OrganizationId is not NULL, then the other SHOULD BE NULL
        //foreign key
        //if ProjectId is null, then TaskType will be available in all projects for the organization
        public int ProjectId {get; set;}
        public Project Project{get; set;}

        //foreign key
        //if organization is specified, then TaskType will be available for all projects of the organization
        public int OrganizationId {get; set;}
        public Organization Organization {get; set;}

        //Validation: 

        //foreign key
        //if user is specified, then TaskType will be available for to user for future
        public int UserId {get; set;}
        public User User{get; set;}

        public List<Task> Tasks {get; set;}
        
    }
}