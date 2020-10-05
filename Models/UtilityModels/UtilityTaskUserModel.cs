namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding during user registration and login
    public class UtilityTaskUserModel
    {
        public int TaskId {get; set;}
        public string AppUserId {get; set;}
    }
}