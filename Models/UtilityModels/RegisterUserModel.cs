/* RegisterUserModel.cs
This files contains the RegisterUserModel class, which is one of the utility models used by the ProjectManager web application.
Utility models are models that are differentialed from the regular models that are used to define database tables/schemes. Utility models
are used as containers of data within the server-side application, mostly when receiving HTTP POST requests from a client of the Web API.

The RegisterUserModel class is used when a user of the application logs in/registers and the login/registration information is sent to the server.
This information is extracted from the POST request and inserted into an instance of this class so the information can be used by the serverside application
*/
namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding during user registration and login
    public class RegisterUserModel
    {
        //the first name of the user being registered
        public string firstName {get; set;}
        //the last name of the user being registered
        public string lastName {get; set;}
        //the username of the user being registered
        public string userName {get; set;}
        //the password of the user being registered
        public string password {get; set;}
    }
}