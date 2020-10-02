namespace ProjectManager.ProjectManagarUtilities
{
    //represents a user model for model binding during user registration and login
    public class RegisterUser 
    {
        public string firstName {get; set;}
        public string lastName {get; set;}
        public string userName {get; set;}
        public string password {get; set;}
    }
}