import React, { useState, useEffect } from "react";
import PageDescription from "../Components/PageDescription.js";
import "../CSS/ManageTaskTypes.css";
import {Link} from "react-router-dom";

const ManageTaskTypes = ({match}) => {
    const [projectDetails, setProjectDetails] = useState({});
    const [newTaskTypeName, setNewTaskTypeName] = useState("");
    const [taskTypeNameError, setTaskTypeNameError] = useState("");
    const [projectTaskTypes, setProjectTaskTypes] = useState([]);
    const [deleteError, setDeleteError] = useState("");

    useEffect(() => {
        fetchProjectData();
        fetchTaskTypes();
    }, []);

    const fetchProjectData = async () => {
        const res = await fetch(`/project/${match.params.projectId}`);
        const data = await res.json();
        setProjectDetails(data);
    };

    const fetchTaskTypes = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task-types`);
        const data = await res.json();
        setProjectTaskTypes(data);
    }

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
                const data = await response.json();
                let updatedTaskTypes = [...projectTaskTypes];
                updatedTaskTypes.push(data);
                setProjectTaskTypes(updatedTaskTypes);
                setTaskTypeNameError("");
                setNewTaskTypeName("");
            }
        }
    }

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