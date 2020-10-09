/**/
/*
 * This file represents the TaskDetails page in the web application
 * It consists of the TaskDetails functional component that handles the rendering of the 
 * page display and also the communication with the server to receive all information on a given task
 * / 
/**/

import React, { useEffect, useState } from "react";
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription"
import "../CSS/TaskDetails.css";
import ConvertDate from "../Utilities/ConvertDate";
import ConvertTime from "../Utilities/ConvertTime";
import "../CSS/TaskUrgency.css";
import CheckAuthentication from "../Utilities/CheckAuthentication";
import LoadingSpinner from "../Utilities/LoadingSpinner";

/**/
/*
 * NAME:
 *      TaskDetails() - React functional component corresponding to the TaskDetails page
 * SYNOPSIS:
 *      TaskDetails({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project the task is a part of
 *          match.params.taskId --> the id of the task
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to list the details of a task
 *      This components handles the retrieval of all data corresponding to the task's details needed to be displayed
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/27/2020 
 * /
 /**/
const TaskDetails = ({match}) => {
    //useEffect hooks
    const [taskDetails, setTaskDetails] = useState({});
    const [taskComments, setTaskComments] = useState([]);
    const [taskUsers, setTaskUsers] = useState([]);
    const [taskTypes, setTaskTypes] = useState([]);
    const [commentsLoaded, setCommentsLoaded] = useState(false);

    //useEffect hook to be called on the first render of the page
    useEffect(() => {
        CheckAuthentication();
        fetchTaskData();
        fetchTaskUsers();
        fetchTaskTypes();
        fetchTaskComments();
    }, []);

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
    *      09/27/2020 
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
    *      fetchTaskComments() - async function to retrieve all the comments in a task
    * SYNOPSIS:
    *      fetchTaskComments()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing a list of all the comments
    *       made under a task
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
    const fetchTaskComments = async () => {
        const res = await fetch(`/project/${match.params.taskId}/comments/recent/15`);
        const data = await res.json();
        setTaskComments(data);
        setCommentsLoaded(true);
    }
    /**/
    /*
    * NAME:
    *      fetchTaskUsers() - async function to retrieve information on all the users assigned to a task
    * SYNOPSIS:
    *      fetchTaskUsers()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing all the users assigned to a task
    *      Sets the taskUsers state corresponding to retrieved information
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
    const fetchTaskUsers = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task/${match.params.taskId}/assigned-users`);
        const data = await res.json();
        setTaskUsers(data);
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
        setTaskTypes(data);
    }

    /**/
    /*
    * NAME:
    *      getTaskTypeName() - finds the name of a task type given its id
    * SYNOPSIS:
    *      getTaskTypeName(typeId)
    *             typeId  -->  the Id of the task type whose name is to be found
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
        for(let i = 0; i < taskTypes.length; i++) {
            if(taskTypes[i].taskTypeId === typeId) {
                return taskTypes[i].name;
            }
        }
    }

    //return the JSX that generates the page. 
    return(
        <div className="page">
            <div className="task-details">
                <PageDescription 
                    title={`${taskDetails.name}`} 
                    description="Here are the details for this task"
                />
                <div className="details">
                    <div className="task-description">
                        {taskDetails.description}
                    </div>
                    <Link to={`/projects/${match.params.projectId}/task/${match.params.taskId}/edit`}>
                        <button id="task-edit" type="button" className="btn btn-lg create-button">Edit Task</button>
                    </Link>
                    <Link to={`/projects/${match.params.projectId}/tasks/`}>
                        <button id ="cancel-task-edit" type="button" className="btn btn-lg btn-secondary">Cancel</button>
                    </Link>
                    <div className="categorical-details">
                        <div className="task-urgency row">
                            <div className="field">
                                Task Urgency: 
                            </div>
                            <div className="entry urgency">
                                <div className={`${taskDetails.urgency}`}>
                                    {taskDetails.urgency}
                                </div>
                                
                            </div>
                        </div>
                        <div className="task-status row">
                            <div className="field">
                                Task Status: 
                            </div>
                            <div className="entry">
                                {taskDetails.taskStatus}
                            </div>
                        </div>
                        <div className="time-created row">
                            <div className="field">
                                Time Created:
                            </div>
                            <div className="entry">
                                {ConvertDate(taskDetails.timeCreated)} at {ConvertTime(taskDetails.timeCreated)}
                            </div>
                        </div>
                        <div className="task-type row">
                            <div className="field">
                                Task Type:
                            </div>
                            <div className="entry">
                                {getTaskTypeName(taskDetails.taskTypeId)}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="task-users">
                    <div className="task-users-header">
                        Assigned Personnel
                    </div>
                    <div className="task-users-list">
                        {taskUsers.length == 0 ?
                        <div>No users assigned to this task</div>
                        : taskUsers.map((user, index) => {
                            return(
                                <div key={index} className="task-users-row">
                                    <div className="task-user-name">
                                        <Link to={`/profile/${user.id}`}>
                                            {user.firstName} {user.lastName}
                                        </Link>
                                    </div>    
                                </div>
                            );
                        })}
                    </div>
                </div>
                <div className="task-comments">
                    <div className="task-comments-header task-comments-row">
                        Recent Comments for this task
                        <Link to={`/projects/${match.params.projectId}/task/${match.params.taskId}/comments`}> 
                            <div className="more-comments">View and Add Comments</div>
                        </Link>
                        
                    </div>
                    <div className="comments-listing">
                        {commentsLoaded ? 
                        (taskComments.length == 0 ?
                            <div className="no-comments">No comments on this task so far</div>
                            : taskComments.map((comment, index) => {
                            return (
                                <div key={index} className="task-comments-row">
                                    {comment.comment}
                                </div>
                            );
                        }) )
                        : <div className="spinner"><LoadingSpinner/> </div>}
                    </div>
                    
                </div>
            </div>

        </div>
    );
}

export default TaskDetails;