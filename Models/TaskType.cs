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

        //foreign key
        //if ProjectId is null, then TaskType will be available in all projects for the organization
        
        public int ProjectId {get; set;}

        //foreign key
        //if organization is specified, then TaskType will be available for all projects of the organization
        public int OrganizationId {get; set;}

        //foreign key
        //if user is specified, then TaskType will be available for to user for future
        public int UserId {get; set;}
        
    }
}