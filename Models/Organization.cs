namespace ProjectManager.Models
{
    public class Organization
    {
        [key]
        public int OrganizationId {get; set;}
        [Required]
        public string Name {get; set;}

        public List<Project> Projects {get; set;}
        public List<TaskType> TaskTypes {get; set;}
        public List<User> Users {get; set;}
    }
}