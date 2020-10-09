/**/
/*
 * This file represents the CreateTask page in the web application
 * It consists of the CreateTask functional component that handles the rendering of the 
 * page display and also the communication with the server to create a new task for a project
 * / 
/**/
import React, {useState, useEffect} from "react";
import PageDescription from "../Components/PageDescription";
import {Link} from "react-router-dom";
import "../CSS/CreateTask.css";
import CheckAuthentication from "../Utilities/CheckAuthentication";

/**/
/*
 * NAME:
 *      CreateTask() - React functional component corresponding to the CreateTask page
 * SYNOPSIS:
 *      CreateTask({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project that this task is a part of
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to create a new task.
 *      This components handles the retrieval of data, generation of forms, and the sending of data to the 
 *      server, thus handling the process of task creation
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/27/2020 
 * /
 /**/
const CreateTask = ({match}) => {
    //state hooks
    const [projectDetails, setProjectDetails] = useState({});
    const [taskName, setTaskName] = useState("");
    const [taskNameError, setTaskNameError] = useState("");
    const [taskDescription, setTaskDescription] = useState("");
    const [taskDescriptionError, setTaskDescriptionError] = useState("");
    const [projectTaskTypes, setProjectTaskTypes] = useState([]);
    const [urgency, setUrgency] = useState("Medium");
    const [taskType, setTaskType] = useState("none");

    //useEffect hook: called on first render
    //first checks if user is authenticated
    //then fetches relevant information
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
    *      09/27/2020 
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
    *      fetchTaskTypes() - async function to retrieve the task types of a project from the server
    * SYNOPSIS:
    *      fetchTaskTypes()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on project task types
    *      Sets the state accordingly
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/27/2020 
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
    *      findTaskTypeId() - find the id of a task type given its name
    * SYNOPSIS:
    *      findTaskTypeId(taskTypeName)
    *             taskTypeName --> the name of the task type whose id is to be found
    * DESCRIPTION:
    *      Finds the id of a task type given its name
    * RETURNS
    *      The id of the task type with the given name
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/27/2020 
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
    *      handleSubmit() - handles the submission of the create task form
    * SYNOPSIS:
    *      handleSubmit()
    *           e --> the JavaScript event generated when submitting the form
    * DESCRIPTION:
    *      This function executes the action to be taken once the user has filled out the form and hit submits.
    *      First it validates the user input in the forms, and sets the error message if user input is not valid.
    *      If user input is valid, it sends a request to the server to create a new task with the given information
    *      Finally, it redirects to the TaskDetails page corresponding to the newly created task
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/27/2020 
    * /
    /**/
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
            //if the task type was not specified, set it to -1 so that the server can ignore it
            let taskTypeId = -1;
            if(projectTaskTypes.length != 0 && taskType !== "none") {
                taskTypeId = findTaskTypeId(taskType);
            }
            
            //build the payload for http request body
            //this payload adheres to the UtilityTaskCreateModel in the server
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
                //redirect to task details page
                window.location.pathname = `/projects/${projectDetails.projectId}/task/${data.taskId}`;
            } else {    
                console.log(data);
            }
        }
    }
    //return the JSX that generates the page. 
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
                                <option value="none">None</option>
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