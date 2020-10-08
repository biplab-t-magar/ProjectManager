import React, { useEffect, useState } from "react";
import {Link} from "react-router-dom";
import "../CSS/ProjectTasks.css";
import "../CSS/TaskUrgency.css";
import CheckAuthentication from "../Utilities/CheckAuthentication";
import ConvertDate from "../Utilities/ConvertDate.js";
import ConvertTime from "../Utilities/ConvertTime.js";


const ProjectTasks = ({match}) => {
    const [projectInfo, setProjectInfo] = useState({});
    const [taskList, setTaskList] = useState([]);
    const [projectTaskTypes, setProjectTaskTypes] = useState([]);

    useEffect(() => {
        CheckAuthentication();
        fetchProjectInfo();
        fetchTaskList();
        fetchTaskTypes();
    }, []);

    const fetchProjectInfo = async () => {
        const res = await fetch(`/project/${match.params.projectId}`);
        const data = await res.json();
        setProjectInfo(data);
    }

    const fetchTaskList = async () => {
        const res = await fetch(`/project/${match.params.projectId}/tasks`);
        const data = await res.json();
        setTaskList(data);
    }

    const fetchTaskTypes = async () => {
        const res = await fetch(`/project/${match.params.projectId}/task-types`);
        const data = await res.json();
        setProjectTaskTypes(data);
    };

    const getTaskTypeName = (typeId) => {
        for(let i = 0; i < projectTaskTypes.length; i++) {
            if(projectTaskTypes[i].taskTypeId === typeId) {
                return projectTaskTypes[i].name;
            }
        }
    }


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
