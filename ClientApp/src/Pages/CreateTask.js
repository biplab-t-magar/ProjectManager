import React, {useState, useEffect} from "react";
import PageDescription from "../Components/PageDescription";
import {Link} from "react-router-dom";
import "../CSS/CreateTask.css";
import CheckAuthentication from "../Utilities/CheckAuthentication";

const CreateTask = ({match}) => {
    const [projectDetails, setProjectDetails] = useState({});
    const [taskName, setTaskName] = useState("");
    const [taskNameError, setTaskNameError] = useState("");
    const [taskDescription, setTaskDescription] = useState("");
    const [taskDescriptionError, setTaskDescriptionError] = useState("");
    const [projectTaskTypes, setProjectTaskTypes] = useState([]);
    const [urgency, setUrgency] = useState("Medium");
    const [taskType, setTaskType] = useState("none");

    useEffect(() => {
        CheckAuthentication();
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

    const findTaskTypeId = (taskTypeName) => {
        for(let i = 0; i < projectTaskTypes.length; i++) {
            if(taskTypeName === projectTaskTypes[i].name) {
                return projectTaskTypes[i].taskTypeId;
            }
        }
    }

    const handleSubmit = async (e) => {
        let errorsExist = false;
        //prevent default action
        e.preventDefault();
        //check task name error
        if(taskName.length === 0) {
            setTaskNameError("You must include a name for the task");
            errorsExist = true;
        } else if(taskName.length > 50) {
            setTaskNameError("Your task name should be no more than 50 characters.");
            errorsExist = true;
        } 
        else {
            setTaskNameError("");
        }

        //check task description error
        if(taskDescription.length > 500) {
            setTaskDescriptionError("Your project description should be no more than 500 characters.");
            errorsExist = true;
        } 
        else {
            setTaskDescriptionError("");
        }

        if(errorsExist === false) {
            let taskTypeId = -1;
            if(projectTaskTypes.length != 0 && taskType !== "none") {
                taskTypeId = findTaskTypeId(taskType);
            }
            
            const payload = {
                ProjectId: projectDetails.projectId,
                Name: taskName,
                Description: taskDescription,
                TaskStatus: "Open",
                Urgency: urgency,
                TaskTypeId: taskTypeId
            }
            //making post request to server
            const response = await fetch("/project/create-task" , {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(payload)
            });
            const data = await response.json();
            if(response.ok) {
                window.location.pathname = `/projects/${projectDetails.projectId}/task/${data.taskId}`;
            } else {    
                console.log(data);
            }
        }
    }

    return(
        <div className="page">
            <div className="create-task">
                <PageDescription 
                    title={`Create a new task`} 
                    description={(`This task will be added to the project ${projectDetails.name ? projectDetails.name : ""}`)} 
                />
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="task-name">Task Name</label>
                        <input 
                            type="text" 
                            className="form-control" 
                            id="task-name" 
                            placeholder="Task Name" 
                            value={taskName} 
                            onChange={(e) => setTaskName(e.target.value)}
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
                            value={taskDescription} 
                            onChange={(e) => setTaskDescription(e.target.value)}
                        />
                        <small className="error-message">
                            {taskDescriptionError ? taskDescriptionError : ""}
                        </small>
                    </div>
                    <div className="form-group">
                        <label htmlFor="select-urgency">Task Urgency</label>
                        <select className="select-urgency" id="urgency" value={urgency} onChange={(e) => setUrgency(e.target.value)}>
                            <option>Medium</option>
                            <option>Low</option>
                            <option>High</option>
                        </select>
                    </div>
                    <div className="form-group">
                        <label htmlFor="select-urgency">Task Type</label>
                        {projectTaskTypes.length == 0 ? 
                            <div className="no-task-types">
                                You have not specified any task types for this project yet
                            </div>        
                            :
                            <select className="select-task-type" id="task-type" value={taskType} onChange={(e) => setTaskType(e.target.value)}>
                                <option value="none" disabled hidden>None</option>
                                {projectTaskTypes.map((taskType, index) => {
                                    return (
                                        <option key={index}>{taskType.name}</option>
                                    );
                                })}
                            </select>
                        }
                    </div>
                    <button type="submit" className="btn btn-primary create">Create Task</button>
                    <Link to={`/projects/${projectDetails.projectId}`}>
                        <button className="btn btn-secondary cancel">Cancel</button>
                    </Link>
                    
                </form>
            </div>
            
        </div>
    );
}

export default CreateTask;