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

    useEffect(() => {
        CheckAuthentication();
        fetchTaskData();
        fetchTaskTypes();
        fetchAssignedUsers();
        fetchUnassignedUsers();
    }, []);

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

    const fetchAssignedUsers = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task/${match.params.taskId}/assigned-users`);
        const data = await res.json();
        setAssignedUsers(data);
    }

    const fetchUnassignedUsers = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task/${match.params.taskId}/unassigned-users`);
        const data = await res.json();
        setUnassignedUsers(data);
    }

    const fetchTaskData = async () => {
        const res = await fetch(`/project/task/${match.params.taskId}`);
        const data = await res.json();
        setTaskDetails(data);
    };

    const fetchTaskTypes = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task-types`);
        const data = await res.json();
        setProjectTaskTypes(data);
    }

    const findTaskTypeId = (taskTypeName) => {
        for(let i = 0; i < projectTaskTypes.length; i++) {
            if(taskTypeName === projectTaskTypes[i].name) {
                return projectTaskTypes[i].taskTypeId;
            }
        }
    }

    const getTaskTypeName = (typeId) => {
        for(let i = 0; i < projectTaskTypes.length; i++) {
            if(projectTaskTypes[i].taskTypeId === typeId) {
                console.log(projectTaskTypes[i].name)
                return projectTaskTypes[i].name;
            }
        }
    }

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
            let taskTypeId = -1;
            console.log(projectTaskTypes);
            console.log(newTaskType);
            if(projectTaskTypes.length != 0 && newTaskType !== "none") {
                taskTypeId = findTaskTypeId(newTaskType);
                console.log(taskTypeId);
            }
            
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
            console.log(payload);
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
                window.location.pathname = `/projects/${taskDetails.projectId}/task/${data.taskId}`;
            } else {    
                console.log(data);
            }
        }
    }

    const assignUserToTask = async (userId) => {
        const payload = {
            TaskId: taskDetails.taskId,
            AppUserId: userId
        }

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
            fetchAssignedUsers();
            fetchUnassignedUsers();
        }
    }

    const unassignUserFromTask = async(userId) => {
        const payload = {
            TaskId: taskDetails.taskId,
            AppUserId: userId
        }
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
            fetchAssignedUsers();
            fetchUnassignedUsers();
        }
    }

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