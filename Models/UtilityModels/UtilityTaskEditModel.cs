/* UtilityTaskEditModel.cs
This files contains the UtilityTaskEditModel class, which is one of the utility models used by the ProjectManager web application.
Utility models are models that are differentialed from the regular models that are used to define database tables/schemes. Utility models
are used as containers of data within the server-side application, mostly when receiving HTTP POST requests from a client of the Web API.

The UtilityTaskEditModel class is used when a user of the application edits a task and the task information is sent to the server.
This information is extracted from the POST request and inserted into an instance of this class so the information can be used by the serverside application
*/
using System;

namespace ProjectManager.Models.UtilityModels
{
    //represents a user model for model binding when editing a model
    public class UtilityTaskEditModel
    {
        //the id of the task that is being edited
        public int TaskId {get; set;}
        //the id of the project which the task belongs to
        public int ProjectId {get; set;}
        //the new name of the task being edited
        public string Name {get; set;}

        //the new description of the task being edited
        public string Description {get; set;}

        //the new task status of the task being edited
        public string TaskStatus {get; set;}

        //the new urgency of the task being edited
        public string Urgency {get; set;}

        //the id of the new task type of the task being edited
        public int TaskTypeId {get; set;}
        //the time that the task being edited was created
        public DateTime TimeCreated {get; set;}
        
    }
}