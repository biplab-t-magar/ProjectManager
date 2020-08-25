namespace ProjectManager.Models
{
    public class Organization
    {
        public int OrganizationId {get; set;}
        [Required]
        public string Name {get; set;}
    }
}