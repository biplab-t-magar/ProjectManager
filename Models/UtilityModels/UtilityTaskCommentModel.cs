/* UtilityTaskCommentModel.cs
This files contains the UtilityTaskCommentModel class, which is one of the utility models used by the ProjectManager web application.
Utility models are models that are differentialed from the regular models that are used to define database tables/schemes. Utility models
are used as containers of data within the server-side application, mostly when receiving HTTP POST requests from a client of the Web API.

The UtilityTaskCommentModel class is used when a user of the application posts a comment on the client side and the comment information is sent to the server.
This information is extracted from the POST request and inserted into an instance of this class so the information can be used by the serverside application
*/
namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding when posting a comment
    public class UtilityTaskCommentModel
    {

        public int TaskId {get; set;}        
    
        public string Comment {get; set;}
        
    }
}