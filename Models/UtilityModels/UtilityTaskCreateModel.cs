/* UtilityTaskCreateModel.cs
This files contains the UtilityTaskCreateModel class, which is one of the utility models used by the ProjectManager web application.
Utility models are models that are differentialed from the regular models that are used to define database tables/schemes. Utility models
are used as containers of data within the server-side application, mostly when receiving HTTP POST requests from a client of the Web API.

The UtilityTaskCreateModel class is used when a user of the application creates a new task for a project and the task information is sent to the server.
This information is extracted from the POST request and inserted into an instance of this class so the information can be used by the serverside application
*/
namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding when creating a task
    public class UtilityTaskCreateModel
    {
        //the id of the project for which the task is being created
        public int ProjectId {get; set;}
        public string Name {get; set;}

        public string Description {get; set;}

        public string TaskStatus {get; set;}

        public string Urgency {get; set;}

        public int TaskTypeId {get; set;}
        
    }
}