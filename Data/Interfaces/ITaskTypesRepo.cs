/* ITaskTypesRepo.cs
 This file contains the ITaskTypesRepo interface, which represents one of the four Repository interfaces in the ProjectManager project.
 A repository interface defines the list of all the public functions for a repository used by the web application. THe ITaskTypesRepo represents
 a repository that contains all information on the task types of the ProjectManager web application
*/
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface ITaskTypesRepo
    {
        TaskType GetTaskTypeById(int taskTypeId);
        
        TaskType CreateTaskType(int projectId, string name);

        void DeleteTaskType(int taskTypeId);
        bool SaveChanges();

    }
}