/**/
/*
 * This file represents the EditTask page in the web application
 * It consists of the EditTask functional component that handles the rendering of the 
 * page display and also the communication with the server to update the information of a task
 * / 
/**/

import React, { useEffect, useState } from "react"
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription.js";
import "../CSS/EditTask.css";
import CheckAuthentication from "../Utilities/CheckAuthentication.js";

/**/
/*
 * NAME:
 *      EditTask() - React functional component corresponding to the EditTask page
 * SYNOPSIS:
 *      EditTask({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project that this task is a part of
 *          match.params.taskId --> the id of the task to be edited
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to edit a task.
 *      This components handles the retrieval of data, generation of forms, and the sending of data to the 
 *      server, thus handling the process of editing a task
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/02/2020 
 * /
 /**/
const EditTask = ({match}) => {
    const [taskDetails, setTaskDetails] = useState({});
    const [newTaskName, setNewTaskName] = useState("");
    const [taskNameError, setTaskNameError] = useState("");
    const [newTaskDescription, setNewTaskDescription] = useState("");
    const [taskDescriptionError, setTaskDescriptionError] = useState("");
    const [projectTaskTypes, setProjectTaskTypes] = useState([]);
    const [newUrgency, setNewUrgency] = useState("");
    const [newTaskType, setNewTaskType] = useState("");
    const [newTaskStatus, setNewTaskStatus] = useState("");
    const [assignedUsers, setAssignedUsers] = useState([]);
    const [unassignedUsers, setUnassignedUsers] = useState([]);

    //called on the first rendering of the page
    useEffect(() => {
        CheckAuthentication();
        fetchTaskData();
        fetchTaskTypes();
        fetchAssignedUsers();
        fetchUnassignedUsers();
    }, []);

    //called every time the value for taskDetails is changed
    useEffect(()=> {
        setNewTaskName(taskDetails.name);
        setNewTaskDescription(taskDetails.description);
        setNewUrgency(taskDetails.urgency);
        setNewTaskStatus(taskDetails.taskStatus);
        if(taskDetails.taskTypeId !== undefined && taskDetails.taskTypeId !== null) {
            setNewTaskType(getTaskTypeName(taskDetails.taskTypeId));
        } else {
            setNewTaskType("none");
        }
        
    }, [taskDetails]);

    /**/
    /*
    * NAME:
    *      fetchAssignedUsers() - async function to retrieve users assigned to a task
    * SYNOPSIS:
    *      fetchAssignedUsers()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the users assigned to a task
    *      Sets the assignedUsers state
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/02/2020 
    * /
    /**/
    const fetchAssignedUsers = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task/${match.params.taskId}/assigned-users`);
        const data = await res.json();
        setAssignedUsers(data);
    }

    /**/
    /*
    * NAME:
    *      fetchUnassignedUsers() - async function to retrieve users not assigned to a task
    * SYNOPSIS:
    *      fetchUnassignedUsers()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the users not assigned to a task
    *      Sets the unassignedUsers state
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/02/2020 
    * /
    /**/
    const fetchUnassignedUsers = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task/${match.params.taskId}/unassigned-users`);
        const data = await res.json();
        setUnassignedUsers(data);
    }

    /**/
    /*
    * NAME:
    *      fetchTaskData() - async function to retrieve data on the current task
    * SYNOPSIS:
    *      fetchTaskData()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the task
    *      Sets the taskDetails state
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/02/2020 
    * /
    /**/
    const fetchTaskData = async () => {
        const res = await fetch(`/project/task/${match.params.taskId}`);
        const data = await res.json();
        setTaskDetails(data);
    };

    /**/
    /*
    * NAME:
    *      fetchTaskTypes() - async function to retrieve data on the task types of the project
    * SYNOPSIS:
    *      fetchTaskTypes()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the task types of 
    *      the project. Sets the projectTaskTypes state
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/02/2020 
    * /
    /**/
    const fetchTaskTypes = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task-types`);
        const data = await res.json();
        setProjectTaskTypes(data);
    }

    /**/
    /*
    * NAME:
    *      findTaskTypeId() - finds the id of a task type given its name
    * SYNOPSIS:
    *      findTaskTypeId(taskTypeName)
    *             taskTypeName  -->  the name of the task type whose Id is to be found
    * DESCRIPTION:
    *      This function finds what the task type id is given the name of a task type.
    * RETURNS
    *       The id of the given task type
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/02/2020 
    * /
    /**/
    const findTaskTypeId = (taskTypeName) => {
        for(let i = 0; i < projectTaskTypes.length; i++) {
            if(taskTypeName === projectTaskTypes[i].name) {
                return projectTaskTypes[i].taskTypeId;
            }
        }
    }

    /**/
    /*
    * NAME:
    *      getTaskTypeName() - finds the name of a task type given its id
    * SYNOPSIS:
    *      getTaskTypeName(taskTypeId)
    *             taskTypeId  -->  the Id of the task type whose name is to be found
    * DESCRIPTION:
    *      This function finds what the task type name is given the id of a task type.
    * RETURNS
    *       The name of the given task type
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/02/2020 
    * /
    /**/
    const getTaskTypeName = (typeId) => {
        for(let i = 0; i < projectTaskTypes.length; i++) {
            if(projectTaskTypes[i].taskTypeId === typeId) {
                console.log(projectTaskTypes[i].name)
                return projectTaskTypes[i].name;
            }
        }
    }

    /**/
    /*
    * NAME:
    *      handleSubmit() - handles the submission of the edit task form
    * SYNOPSIS:
    *      handleSubmit(e)
    *           e --> the JavaScript event generated when submitting the form
    * DESCRIPTION:
    *      This function executes the action to be taken once the user has filled out the form and hit submit.
    *      First it validates the user input in the forms, and sets the error message if user input is not valid.
    *      If user input is valid, it sends a request to the server to edit the given task with the given information
    *      Finally, it redirects to the TaskDetails page corresponding to the edited task
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/02/2020 
    * /
    /**/
    const handleSubmit = async (e) => {
        let errorsExist = false;
        //prevent default action
        e.preventDefault();
        //check task name error
        if(newTaskName.length === 0) {
            setTaskNameError("You must include a name for the task");
            errorsExist = true;
        } else if(newTaskName.length > 50) {
            setTaskNameError("Your task name should be no more than 50 characters.");
            errorsExist = true;
        } 
        else {
            setTaskNameError("");
        }

        //check task description error
        if(newTaskDescription.length > 500) {
            setTaskDescriptionError("Your project description should be no more than 500 characters.");
            errorsExist = true;
        } 
        else {
            setTaskDescriptionError("");
        }

        if(errorsExist === false) {
            //if the task type id was not specified or if it was specified to be "none"
            //, then set the taskTypeId to -1, so that the server ignores this taskTypeId when 
            //if receives the HTTP request
            let taskTypeId = -1;
            if(projectTaskTypes.length != 0 && newTaskType !== "none") {
                taskTypeId = findTaskTypeId(newTaskType);
                console.log(taskTypeId);
            }
            
            //create the payload to be sent over in the body of the HTTP request. 
            //This payload adhers to the UtilityTaskEditModel in the server
            const payload = {
                TaskId: taskDetails.taskId,
                ProjectId: taskDetails.projectId,
                Name: newTaskName,
                Description: newTaskDescription,
                TaskStatus: newTaskStatus,
                Urgency: newUrgency,
                TaskTypeId: taskTypeId,
                TimeCreated: taskDetails.timeCreated
            }
            //making post request to server
            const response = await fetch("/project/edit-task" , {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(payload)
            });
            const data = await response.json();
            if(response.ok) {
                //if the task was successfully edited, redirect to task details page
                window.location.pathname = `/projects/${taskDetails.projectId}/task/${data.taskId}`;
            } else {    
                console.log(data);
            }
        }
    }

    /**/
    /*
    * NAME:
    *      assignUserToTask() - handles the assigning of a user to a task
    * SYNOPSIS:
    *      assignUserToTask(userId)
    *           userId --> the id of the user to be assigned to the taks
    * DESCRIPTION:
    *      This function makes a request to the server to assign the current task to the specified user
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/02/2020 
    * /
    /**/
    const assignUserToTask = async (userId) => {
        //This payload adheres to the TaskUser model in the server
        const payload = {
            TaskId: taskDetails.taskId,
            AppUserId: userId
        }

        //make request to server
        const response = await fetch("/project/assign-task-user" , {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-type": "application/json"
            },
            body: JSON.stringify(payload)
        });
        const data = await response.json();

        if(!response.ok) {
            console.log(data);
        } else {
            //once the request has been made, the server updates the list of assigned and unassigned users in its database
            //so, we need to again fetch the assigned users and unassigned users list to update the page accordingly
            fetchAssignedUsers();
            fetchUnassignedUsers();
        }
    }
    /**/
    /*
    * NAME:
    *      unassignUserFromTask() - handles the unssigning of a user from a task
    * SYNOPSIS:
    *      unassignUserFromTask(userId)
    *           userId --> the id of the user to be unassigned from the taks
    * DESCRIPTION:
    *      This function makes a request to the server to unassign the user from the current task
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/02/2020 
    * /
    /**/
    const unassignUserFromTask = async(userId) => {
        //This payload adheres to the TaskUser model in the server
        const payload = {
            TaskId: taskDetails.taskId,
            AppUserId: userId
        }
        //make server request
        const response = await fetch("/project/unassign-task-user" , {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-type": "application/json"
            },
            body: JSON.stringify(payload)
        });
        const data = await response.json();

        if(!response.ok) {
            console.log(data);
        } else {
            //once the request has been made, the server updates the list of assigned and unassigned users in its database
            //so, we need to again fetch the assigned users and unassigned users list to update the page accordingly
            fetchAssignedUsers();
            fetchUnassignedUsers();
        }
    }
    //return the JSX that generates the page. 
    return(
        <div className="page">
            <div className="edit-task">
                <PageDescription 
                    title={`Edit ${taskDetails.name}`} 
                    description={(`Make changes to the task, change assigned personnel, and change task status`)} />
                <form className="task-form" onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="task-name">Task Name</label>
                        <input 
                            type="text" 
                            className="form-control" 
                            id="task-name" 
                            placeholder="Task Name" 
                            value={newTaskName || ""}
                            onChange={(e) => setNewTaskName(e.target.value)}
                        />
                        <small className="error-message">
                            {taskNameError ? taskNameError : ""}
                        </small>
                    </div>
                    <div className="form-group">
                        <label htmlFor="task-description">Task Description (optional)</label>
                        <textarea 
                            type="text" 
                            className="form-control" 
                            id="task-description" 
                            placeholder="Task Description" 
                            value={newTaskDescription} 
                            onChange={(e) => setNewTaskDescription(e.target.value)}
                        />
                        <small className="error-message">
                            {taskDescriptionError ? taskDescriptionError : ""}
                        </small>
                    </div>
                    <div className="categorical-details">
                        <div className="form-group">
                            <label htmlFor="select-urgency" className="category">Task Urgency</label>
                            <select className="select-urgency" id="urgency" value={newUrgency} onChange={(e) => setNewUrgency(e.target.value)}>
                                <option>Medium</option>
                                <option>Low</option>
                                <option>High</option>
                            </select>
                        </div>
                        <div className="form-group">
                            <label htmlFor="select-urgency"className="category">Task Type</label>
                            {projectTaskTypes.length == 0 ? 
                                <div className="no-task-types">
                                    You have not specified any task types for this project yet
                                </div>        
                                :
                                <select className="select-task-type" id="task-type" value={newTaskType} onChange={(e) => setNewTaskType(e.target.value)}>
                                    <option value="none">None</option>
                                    {projectTaskTypes.map((taskType, index) => {
                                        return (
                                            <option key={index}>{taskType.name}</option>
                                        );
                                    })}
                                </select>
                            }
                        </div>
                        <div className="form-group">
                            <label htmlFor="select-status"className="category">Task Status</label>
                            <select className="select-status" id="status" value={newTaskStatus} onChange={(e) => setNewTaskStatus(e.target.value)}>
                                {/* Open, Suspended, Roadblock Encountered, Under Review, Completed */}
                                <option>Open</option>
                                <option>Suspended</option>
                                <option>Roadblock Encountered</option>
                                <option>Under Review</option>
                                <option>Completed</option>
                            </select>
                        </div>
                    </div>
                    <button type="submit" className="btn btn-primary create">Update Task</button>
                    <Link to={`/projects/${taskDetails.projectId}/task/${taskDetails.taskId}`}>
                        <button className="btn btn-secondary cancel">Cancel</button>
                    </Link>
                </form>
                <div className="assigned-users users-list">
                    <div className="users-list-header">
                        Users assigned to this task
                    </div>
                    {assignedUsers.length == 0 ? 
                        <div className="no-content">No users assigned</div>
                        : assignedUsers.map((user, index) => {
                            return(
                                <div className="users-list-row" key={index}>
                                    <div className="user-name users-list-column">
                                        <Link to={`/profile/${user.id}`}>
                                            {user.firstName} {user.lastName}
                                        </Link>
                                    </div>
                                    <div className="users-list-column">
                                        <button onClick={() => unassignUserFromTask(user.id)} className="btn btn-primary create">
                                            Unassign
                                        </button>
                                    </div>
                                </div>
                            );
                    })}
                </div>
                <div className="unassigned-users users-list">
                    <div className="users-list-header">
                        Users not assigned to this task
                    </div>
                    {unassignedUsers.length == 0 ? 
                        <div className="no-content">All users assigned</div>
                        : unassignedUsers.map((user, index) => {
                            return(
                                <div className="users-list-row" key={index}>
                                    <div className="user-name users-list-column">
                                        <Link to={`/profile/${user.id}`}>
                                            {user.firstName} {user.lastName}
                                        </Link>
                                    </div>
                                    <div className="users-list-column">
                                        <button onClick={() => assignUserToTask(user.id)} className="btn btn-primary create">
                                            Assign
                                        </button>
                                    </div>
                                </div>
                            );
                    })}
                </div>
                
            </div>
            
            
        </div>
    );
}

export default EditTask;