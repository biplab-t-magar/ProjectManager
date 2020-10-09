/* UpdateUserModel.cs
This files contains the UpdateUserModel class, which is one of the utility models used by the ProjectManager web application.
Utility models are models that are differentialed from the regular models that are used to define database tables/schemes. Utility models
are used as containers of data within the server-side application, mostly when receiving HTTP POST requests from a client of the Web API.

The UpdateUserModel class is used when a user of the application updates user information and the information is sent to the server.
This information is extracted from the POST request and inserted into an instance of this class so the information can be used by the serverside application
*/
namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding during updating a user
    public class UpdateUserModel
    {
        //the new first name of the user whose info is being updated
        public string FirstName {get; set;}

        //the new last name of the user whose info is being updated
        public string LastName {get; set;}

        //the new bio of the user whose info is being updated
        public string Bio {get; set;}
    }
}