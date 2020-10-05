namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding during user registration and login
    public class UtilityTaskCreateModel
    {
        public int ProjectId {get; set;}
        public string Name {get; set;}

        public string Description {get; set;}

        public string TaskStatus {get; set;}

        public string Urgency {get; set;}

        public int TaskTypeId {get; set;}
        
    }
}