/**/
/*
 * This file represents the ManageTaskTypes page in the web application
 * It consists of the ManageTaskTypes functional component that handles the rendering of the 
 * page display and also the communication with the server to create and delete TaskTypes for the project
 * / 
/**/

import React, { useState, useEffect } from "react";
import PageDescription from "../Components/PageDescription.js";
import "../CSS/ManageTaskTypes.css";
import {Link} from "react-router-dom";
import CheckAuthentication from "../Utilities/CheckAuthentication.js";

/**/
/*
 * NAME:
 *      ManageTaskTypes() - React functional component corresponding to the ManageTaskTypes page
 * SYNOPSIS:
 *      ManageTaskTypes({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to manage the task types in a project.
 *      This components handles the retrieval of data, and the sending of data to the 
 *      server, thus handling the processes of creating a new task type and deleting a task type
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/04/2020 
 * /
 /**/
const ManageTaskTypes = ({match}) => {
    //userState hooks
    const [projectDetails, setProjectDetails] = useState({});
    const [newTaskTypeName, setNewTaskTypeName] = useState("");
    const [taskTypeNameError, setTaskTypeNameError] = useState("");
    const [projectTaskTypes, setProjectTaskTypes] = useState([]);
    const [deleteError, setDeleteError] = useState("");

    //useEffect hook called on the first rendering of the page
    useEffect(() => {
        CheckAuthentication();
        fetchProjectData();
        fetchTaskTypes();
    }, []);

    /**/
    /*
    * NAME:
    *      fetchProjectData() - async function to retrieve project data from server
    * SYNOPSIS:
    *      fetchProjectData()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the project.
    *      Sets the state corresponding to project data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchProjectData = async () => {
        const res = await fetch(`/project/${match.params.projectId}`);
        const data = await res.json();
        setProjectDetails(data);
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
    *      10/04/2020 
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
    *      createNewTaskType() - async function that creates a new task type for the project
    * SYNOPSIS:
    *      createNewTaskType()
    * DESCRIPTION:
    *      This function makes a GET request to the server to create a new task type with the name specified in the newTaskTypeName state
    *      It also updates the projectTaskTypes array to reflect the changes in the list of project task types
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const createNewTaskType = async () => {
        let errorsExist = false;

        //check task type name 
        if(newTaskTypeName.length === 0) {
            setTaskTypeNameError("You must include a name for the new task type");
            errorsExist = true;
        } 
        else {
            setTaskTypeNameError("");
        }

        if(errorsExist == false) {
            //payload adheres to the UtilityTaskTypeModel in the server
            const payload = {
                ProjectId: projectDetails.projectId,
                Name: newTaskTypeName
            }
            //making post request to server
            const response = await fetch("/project/create-task-type" , {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(payload)
            });
            if(response.ok) {
                //if no error indicated in response, update the list of task types to show
                const data = await response.json();
                let updatedTaskTypes = [...projectTaskTypes];
                updatedTaskTypes.push(data);
                setProjectTaskTypes(updatedTaskTypes);
                setTaskTypeNameError("");
                setNewTaskTypeName("");
            }
        }
    }

    /**/
    /*
    * NAME:
    *      deleteTaskType() - async function that deletes a task type from the project
    * SYNOPSIS:
    *      deleteTaskType(taskTypeId)
    *           taskTypeId --> the id of the task type to be deleted
    * DESCRIPTION:
    *      This function makes a GET request to the server to delete a task type with the specified id
    *      It also updates the projectTaskTypes array to reflect the changes in the list of project task types
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const deleteTaskType = async (taskTypeId) => {
        const response = await fetch(`/project/${projectDetails.projectId}/task-types/${taskTypeId}`, {
            method: "DELETE",
            headers: {
                "Accept": "application/json",
                "Content-type": "application/json"
            },
        });
        const data = await response.json();
        if(!response.ok){
            setDeleteError(data);
        } else {
            setProjectTaskTypes(data);
            setDeleteError("");
        }
    }

    //return the JSX that generates the page. 
    return(
        <div className="page">
            <div className="manage-task-types">
                <PageDescription 
                    title={` Manage Tasks Types for ${(projectDetails.name ? projectDetails.name : "")}`} 
                    description="Create and delete Task Types"
                />
                <div className="create-task-type">
                    <input 
                        type="text" 
                        className="form-control" 
                        id="task-type-name" 
                        placeholder="Name For New Task Type" 
                        value={newTaskTypeName || ""} 
                        onChange={(e) => setNewTaskTypeName(e.target.value)}
                    />
                    <small className="error-message">
                        {taskTypeNameError ? taskTypeNameError : ""}
                    </small>
                    <button onClick={createNewTaskType} className="btn btn-lg create-button">Create Task Type </button>
                    <Link to={`/projects/${projectDetails.projectId}`}>
                        <button className="btn btn-secondary cancel">Cancel</button>
                    </Link>
                </div>
                <div className="task-types-list">
                    <div className="task-types-list-header">Project Task Types</div>
                    {projectTaskTypes.map((taskType) =>{
                        return(
                            <div key={taskType.taskTypeId} className="task-types-list-row">
                                <div className="task-type-name">
                                    {taskType.name}
                                </div>
                                <button onClick={() => deleteTaskType(taskType.taskTypeId)} type="button delete-task-type" className="btn btn-sm delete-button">
                                    Delete
                                </button>
                            </div>      
                        );
                    })}
                    <small className="error-message">
                        {deleteError ? deleteError : ""}
                    </small>
                </div>
            </div>
        </div>
    );
}

export default ManageTaskTypes;