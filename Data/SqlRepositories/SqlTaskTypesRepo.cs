/* SqlTaskTypesRepo.cs
 This file contains the SqlTaskTypesRepo class. The SqlTaskTypesRepo class is an implementation of the ITaskTypesRepo interface. It represents 
 an implementation of the interface by using an SQL database to store and retrieve data. So this repository class communicates with an SQL database
 (a PostgreSQL database, specifically),while providing all the functions to retrieve and manipulate the entries in the database
 that are listed in the interface it implements.
*/

using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlTaskTypesRepo : ITaskTypesRepo
    {
        //the database context
        private readonly ProjectManagerContext _context;

        /**/
        /*
        * NAME:
        *      SqlTaskTypesRepo - constructor for SqlTaskTypesRepo
        * SYNOPSIS:
            SqlTaskTypesRepo(ProjectManagerContext context)
        *           context --> the database context that is injected into the class through dependency injection
        * DESCRIPTION:
                The constructor implements the SqlTaskTypesRepo class, which represents an implementation of the ITaskTypesRepo interface
                It initializes the _context member variable, which will be used by all the functions in this class for data access
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/02/2020 
        * /
        /**/
        public SqlTaskTypesRepo(ProjectManagerContext context)
        {
            _context = context;
        }

        /**/
        /*
        * NAME:
        *      GetTaskTypeById - gets the task type object given its id
        * SYNOPSIS:
                GetTaskTypeById(int taskTypeId)
        *           taskTypeId --> the id of the task type to be returned
        * DESCRIPTION:
                Accesses the database context in order to find the task type of the given id
        * RETURNS
                A TaskType object with the given id, taken from the database
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/02/2020 
        * /
        /**/
        public TaskType GetTaskTypeById(int taskTypeId)
        {
            return _context.TaskTypes.Find(taskTypeId);
        }
        
        /**/
        /*
        * NAME:
        *      CreateTaskType - creates a new task type 
        * SYNOPSIS:
                CreateTaskType(int projectId, string name)
        *           projectId --> the id of the project for which the task type is being created
                    name --> the name of the task type
        * DESCRIPTION:
                Accesses the database context in order to create a new task type
        * RETURNS
                The TaskType object that was created
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/02/2020 
        * /
        /**/
        public TaskType CreateTaskType(int projectId, string name)
        {
            TaskType taskType = new TaskType() {ProjectId = projectId, Name = name};
            _context.Add(taskType);
            return taskType;
        }

        /**/
        /*
        * NAME:
        *      DeleteTaskType - deletes a task type 
        * SYNOPSIS:
                DeleteTaskType(int taskTypeId)
        *           taskTypeId --> the id of the task type to be deleted
        * DESCRIPTION:
                Accesses the database context in order to delete the specified task type
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/02/2020 
        * /
        /**/
        public void DeleteTaskType(int taskTypeId)
        {
            var taskType = GetTaskTypeById(taskTypeId);
            _context.TaskTypes.Remove(taskType);
        }


        /**/
        /*
        * NAME:
        *      SaveChanges - saves all changes made so far using the context into the database
        * SYNOPSIS:
                SaveChanges()
        * DESCRIPTION:
                Accesses the database context in order save changes to it
        * RETURNS
                true if savechanges was successful, false if not
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/02/2020 
        * /
        /**/
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

    }   
}