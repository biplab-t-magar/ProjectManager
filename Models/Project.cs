namespace ProjectManager.Models
{
    public class Project
    {
        public int ProjectId {get; set;}
        [Required]
        public string Name {get; set}
        [Required]
        public string DateCreated {get; set;}
        public string DateTerminated {get; set;}

        //foreign key
        public int OrganizationId {get; set;}
    }
}