import React, { useEffect, useState } from "react";
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription"
import "../CSS/TaskDetails.css";
import ConvertDate from "../Utilities/ConvertDate";
import ConvertTime from "../Utilities/ConvertTime";


const TaskDetails = ({match}) => {
    const [taskDetails, setTaskDetails] = useState({});
    const [taskUsers, setTaskUsers] = useState([]);
    const [taskTypes, setTaskTypes] = useState([]);
    const [projectUsers, setProjectUsers] = useState([]);

    useEffect(() => {
        fetchTaskData();
        fetchTaskUsers();
        fetchTaskTypes();
        fetchProjectUsers();
    }, []);

    const fetchTaskData = async () => {
        const res = await fetch(`/project/task/${match.params.taskId}`);
        const data = await res.json();
        setTaskDetails(data);
        console.log(data);
    };

    const fetchTaskUsers = async () => {
        const res = await fetch(`/project/task/${match.params.taskId}/users`);
        const data = await res.json();
        setTaskUsers(data);
    }
    const fetchTaskTypes = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task-types`);
        const data = await res.json();
        setTaskTypes(data);
    }

    const fetchProjectUsers = async() => {
        const res = await fetch(`/project/${match.params.projectId}/users`);
        const data = await res.json();
        setProjectUsers(data);
    };

    const getTaskTypeName = (typeId) => {
        for(let i = 0; i < taskTypes.length; i++) {
            if(taskTypes[i].taskTypeId === typeId) {
                return taskTypes[i].name;
            }
        }
    }

    const userIsAssignedToTask = (userId) => {
        for(let i = 0; i < taskUsers.length; i++) {
            if(taskUsers[i].appUserId == userId) {
                return true;
            }
        }
        return false;
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
                        <button type="button" className="btn btn-lg create-button">Edit Project</button>
                    </Link>
                    <div className="task-status row">
                        <div className="field">
                            Task Status: 
                        </div>
                        <div className="entry">
                            {taskDetails.taskStatus}
                        </div>
                    </div>
                    <div className="task-urgency row">
                        <div className="field">
                            Task Urgency: 
                        </div>
                        <div className="entry">
                            {taskDetails.urgency}
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
                <div className="task-users">
                    <div className="task-users-header">
                        Project Members
                    </div>
                    <div className="task-users-list">
                        {projectUsers.map((user, index) => {
                            return(
                                <div key={index} className="task-users-row">
                                    <div className="task-user-name column">
                                        {user.firstName} {user.lastName}
                                    </div>
                                    <div>
                                        <button className="assigned column">
                                            {userIsAssignedToTask(user.id) ? "Unassign" : "Assign"}
                                        </button>
                                    </div>
                                    
                                </div>
                            );
                        })}
                    </div>
                </div>
            </div>

        </div>
    );
}

export default TaskDetails;