namespace ProjectManager
{
    public class User
    {
        public int UserId;
        [Required]
        public string FirstName;
        [Required]
        public string LastName;
        public string MiddleName;
        
        public int OrganizationId;

    }
}