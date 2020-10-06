import React, { useEffect, useState } from 'react';
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription.js";
import CheckAuthentication from '../Utilities/CheckAuthentication.js';
import "../CSS/UserTasks.css";
import "../CSS/TaskUrgency.css"
import ConvertDate from "../Utilities/ConvertDate";
import ConvertTime from "../Utilities/ConvertTime";
import LoadingSpinner from "../Utilities/LoadingSpinner";

const Tasks = () => {
    const [userProjects, setUserProjects] = useState([]);
    const [userTasks, setUserTasks] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);

    useEffect(() => {
        CheckAuthentication();
        fetchUserTasks();
        fetchProjects();
    }, [])

    const fetchProjects = async () => {
        const res = await fetch("/user/projects");
        const data = await res.json();
        setUserProjects(data);
        setContentLoaded(true);
    }; 

    const fetchUserTasks = async () => {
        const res = await fetch("/user/tasks");
        const data = await res.json();
        setUserTasks(data);
    }

    const getProjectName = (projectId) => {
        for(let i = 0; i < userProjects.length; i++) {
            if(userProjects[i].projectId === projectId) {
                return userProjects[i].name;
            }
        }
    }
    
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

