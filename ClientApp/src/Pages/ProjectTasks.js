/**/
/*
 * This file represents the ProjectTasks page in the web application
 * It consists of the ProjectTasks functional component that handles the rendering of the 
 * page display and also the communication with the server to receive information on all the tasks of a project
 * / 
/**/

import React, { useEffect, useState } from "react";
import {Link} from "react-router-dom";
import "../CSS/ProjectTasks.css";
import "../CSS/TaskUrgency.css";
import CheckAuthentication from "../Utilities/CheckAuthentication";
import ConvertDate from "../Utilities/ConvertDate.js";
import ConvertTime from "../Utilities/ConvertTime.js";

/**/
/*
 * NAME:
 *      ProjectTasks() - React functional component corresponding to the ProjectTasks page
 * SYNOPSIS:
 *      ProjectTasks({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to list all the tasks in a project
 *      This components handles the retrieval of all data needed to display the projects's tasks and related information
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/03/2020 
 * /
 /**/
const ProjectTasks = ({match}) => {
    //useState hooks
    const [projectInfo, setProjectInfo] = useState({});
    const [taskList, setTaskList] = useState([]);
    const [projectTaskTypes, setProjectTaskTypes] = useState([]);

    //useEffect hook called when the page is first rendered
    useEffect(() => {
        CheckAuthentication();
        fetchProjectInfo();
        fetchTaskList();
        fetchTaskTypes();
    }, []);

    /**/
    /*
    * NAME:
    *      fetchProjectInfo() - async function to retrieve project data from server
    * SYNOPSIS:
    *      fetchProjectInfo()
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
    const fetchProjectInfo = async () => {
        const res = await fetch(`/project/${match.params.projectId}`);
        const data = await res.json();
        setProjectInfo(data);
    }

    /**/
    /*
    * NAME:
    *      fetchTaskList() - async function to retrieve list of all tasks in a project from the server
    * SYNOPSIS:
    *      fetchTaskList()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on all the tasks of a project.
    *      Sets the taskList state corresponding to project data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchTaskList = async () => {
        const res = await fetch(`/project/${match.params.projectId}/tasks`);
        const data = await res.json();
        setTaskList(data);
    }

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
    };

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
                return projectTaskTypes[i].name;
            }
        }
    }


    //return the JSX that generates the page. 
    return (
        <div className="page">
            <div className = "project-tasks">
                <div className="page-description">
                    <h1>All the tasks in 
                        <span className="project-name">{projectInfo.name}</span>
                    </h1>
                    <span>Here are all the tasks created for this project</span>
                </div>
                <Link to={`/projects/${projectInfo.projectId}/tasks/new`} >
                    <button type="button" className="btn btn-lg create-button create-button">+ Create New Task</button>
                </Link>
                <Link to={`/projects/${projectInfo.projectId}`} >
                    <button type="button" className="btn btn-lg create-button cancel">Go back to Project</button>
                </Link>
                <div className="tasks-list">
                    {/*headers*/}
                    <div className="tasks-list-header task-list-row">
                        <div className="name column">Name</div>
                        <div className="task-type column">Type</div>
                        <div className="task-status column">Status</div>
                        <div className="urgency column">Urgency</div>
                        <div className="time-created column">Created On</div>
                        <div className="description column">Description</div>
                    </div>
                    {taskList.map((task) => {
                        return (
                            <Link to={`/projects/${match.params.projectId}/task/${task.taskId}`} key={task.taskId}>
                                <div  className="task-entry task-list-row">
                                    <div className="name column">{task.name}</div>
                                    <div className="task-type column">{getTaskTypeName(task.taskTypeId)}</div>
                                    <div className="task-status column">{task.taskStatus}</div>
                                    <div className="urgency column">
                                        <div className={`${task.urgency}`}>{task.urgency}</div>
                                    </div>
                                    <div className="time-created column">{ConvertDate(task.timeCreated)} at {ConvertTime(task.timeCreated)}</div>
                                    <div className="description column">{task.description}</div>
                                </div>
                            </Link>
                        );
                    })}
                </div>
            </div>
        </div>
    );
};

export default ProjectTasks
