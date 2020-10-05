namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding during user registration and login
    public class UtilityInviteModel
    {
        public int projectId {get; set;}
        public string inviteeUserName {get; set;}
    }
}