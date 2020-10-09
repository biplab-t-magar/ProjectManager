/**/
/*
 * This file represents the UserTasks page in the web application
 * It consists of the UserTasks functional component that handles the rendering of the 
 * page display and also the communication with the server to receive information on every task that a user is assigned to
 * / 
/**/
import React, { useEffect, useState } from 'react';
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription.js";
import CheckAuthentication from '../Utilities/CheckAuthentication.js';
import "../CSS/UserTasks.css";
import "../CSS/TaskUrgency.css"
import ConvertDate from "../Utilities/ConvertDate";
import ConvertTime from "../Utilities/ConvertTime";
import LoadingSpinner from "../Utilities/LoadingSpinner";


/**/
/*
 * NAME:
 *      Tasks() - React functional component corresponding to the Tasks page
 * SYNOPSIS:
 *      Tasks()
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the list of all the tasks assigned to a user 
 *      This components handles the retrieval of all of the user's task, along with related information
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/7/2020 
 * /
 /**/
const Tasks = () => {
    //useState hooks
    const [userProjects, setUserProjects] = useState([]);
    const [userTasks, setUserTasks] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);

    //useEffect hook called on first render
    useEffect(() => {
        CheckAuthentication();
        fetchUserTasks();
        fetchProjects();
    }, [])

    /**/
    /*
    * NAME:
    *      fetchProjects() - async function to retrieve the list of projects of a user from the server
    * SYNOPSIS:
    *      fetchProjects()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the all the project's of a user
    *      Sets the userProjects state corresponding to retrieved data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/7/2020 
    * /
    /**/
    const fetchProjects = async () => {
        const res = await fetch("/user/projects");
        const data = await res.json();
        setUserProjects(data);
        setContentLoaded(true);
    }; 

    /**/
    /*
    * NAME:
    *      fetchUserTasks() - async function to retrieve the list of all tasks for a user from the server
    * SYNOPSIS:
    *      fetchUserTasks()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the all the projects assigned to a user
    *      Sets the userTasks state corresponding to retrieved data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/7/2020 
    * /
    /**/
    const fetchUserTasks = async () => {
        const res = await fetch("/user/tasks");
        const data = await res.json();
        setUserTasks(data);
    }

    /**/
    /*
    * NAME:
    *      getProjectName() - gets the project name given the id of the project
    * SYNOPSIS:
    *      getProjectName(projectid)
    *           projectId     -->    the id of the project whose name is to be found
    * DESCRIPTION:
    *      Gets the name of the project whose id is given
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
    const getProjectName = (projectId) => {
        for(let i = 0; i < userProjects.length; i++) {
            if(userProjects[i].projectId === projectId) {
                return userProjects[i].name;
            }
        }
    }
    
    //return the JSX that generates the page. 
    return (
        <div className="page">
            <div className="tasks">
                <PageDescription title="Your Tasks" description="This is a list of all of tasks assigned to you"/>

                <div className="tasks-list">
                    <div className="tasks-list-header tasks-list-row">
                        <div className="column name">Name</div>
                        <div className="column project">Project</div>
                        <div className="column status">Status</div>
                        <div className="column urgency">Urgency</div>
                        <div className="column created-on">Created On</div>
                        <div className="column description">Description</div>
                    </div>
                    {contentLoaded ? 
                        userTasks.map((task,index) => {
                        return (
                            <Link to={`/projects/${task.projectId}/task/${task.taskId}`} key={index}>
                                <div className="tasks-list-row tasks-list-entry" key={index}>
                                    <div className="column name">{task.name}</div>
                                    <div className="column project">{getProjectName(task.projectId)}</div>
                                    <div className="column status">{task.taskStatus}</div>
                                    <div className="column urgency">
                                        <div className={`${task.urgency}`}>
                                            {task.urgency}
                                        </div>
                                    </div>
                                    <div className="column created-on">{ConvertDate(task.timeCreated)} at {ConvertTime(task.timeCreated)}</div>
                                    <div className="column description">{task.description}</div>
                                </div>
                            </Link>
                        );
                    }) : <div className="spinner"><LoadingSpinner /> </div>}
                </div>
            </div>
        </div>
    );
}

export default Tasks;

