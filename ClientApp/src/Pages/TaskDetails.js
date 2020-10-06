import React, { useEffect, useState } from "react";
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription"
import "../CSS/TaskDetails.css";
import ConvertDate from "../Utilities/ConvertDate";
import ConvertTime from "../Utilities/ConvertTime";
import "../CSS/TaskUrgency.css";
import CheckAuthentication from "../Utilities/CheckAuthentication";
import LoadingSpinner from "../Utilities/LoadingSpinner";


const TaskDetails = ({match}) => {
    const [taskDetails, setTaskDetails] = useState({});
    const [taskComments, setTaskComments] = useState([]);
    const [taskUsers, setTaskUsers] = useState([]);
    const [taskTypes, setTaskTypes] = useState([]);
    const [commentsLoaded, setCommentsLoaded] = useState(false);

    useEffect(() => {
        CheckAuthentication();
        fetchTaskData();
        fetchTaskUsers();
        fetchTaskTypes();
        fetchTaskComments();
    }, []);


    const fetchTaskData = async () => {
        const res = await fetch(`/project/task/${match.params.taskId}`);
        const data = await res.json();
        setTaskDetails(data);
    };

    const fetchTaskComments = async () => {
        const res = await fetch(`/project/${match.params.taskId}/comments/recent/15`);
        const data = await res.json();
        setTaskComments(data);
        setCommentsLoaded(true);
    }

    const fetchTaskUsers = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task/${match.params.taskId}/assigned-users`);
        const data = await res.json();
        setTaskUsers(data);
    }
    const fetchTaskTypes = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task-types`);
        const data = await res.json();
        setTaskTypes(data);
    }


    const getTaskTypeName = (typeId) => {
        for(let i = 0; i < taskTypes.length; i++) {
            if(taskTypes[i].taskTypeId === typeId) {
                return taskTypes[i].name;
            }
        }
    }

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