namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding during user registration and login
    public class UpdateUserModel
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Bio {get; set;}
    }
}