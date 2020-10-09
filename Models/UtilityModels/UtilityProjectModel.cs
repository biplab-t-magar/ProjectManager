/* UtilityProjectModel.cs
This files contains the UtilityProjectModel class, which is one of the utility models used by the ProjectManager web application.
Utility models are models that are differentialed from the regular models that are used to define database tables/schemes. Utility models
are used as containers of data within the server-side application, mostly when receiving HTTP POST requests from a client of the Web API.

The UtilityProjectModel class is used when a user of the application creates a project and the project information is sent to the server.
This information is extracted from the POST request and inserted into an instance of this class so the information can be used by the serverside application
*/

namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding when creating a new project
    public class UtilityProjectModel
    {
        public string name {get; set;}
        public string description {get; set;}
    }
}