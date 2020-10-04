namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding during user registration and login
    public class RegisterUserModel
    {
        public string firstName {get; set;}
        public string lastName {get; set;}
        public string userName {get; set;}
        public string password {get; set;}
    }
}