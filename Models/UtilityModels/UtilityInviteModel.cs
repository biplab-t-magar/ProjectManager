/* UtilityInviteModel.cs
This files contains the UtilityInviteModel class, which is one of the utility models used by the ProjectManager web application.
Utility models are models that are differentialed from the regular models that are used to define database tables/schemes. Utility models
are used as containers of data within the server-side application, mostly when receiving HTTP POST requests from a client of the Web API.

The UtilityInviteModel class is used when a user of the application invites another user to a project and the invitation information is sent to the server.
This information is extracted from the POST request and inserted into an instance of this class so the information can be used by the serverside application
*/

namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding when handling invitations to projects
    public class UtilityInviteModel
    {
        //the id of the project to which the user is being invited
        public int projectId {get; set;}

        //the username of the user being invited
        public string inviteeUserName {get; set;}
    }
}