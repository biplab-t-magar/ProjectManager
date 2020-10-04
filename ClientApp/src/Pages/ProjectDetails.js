import React, { useEffect } from 'react';
import {Link} from "react-router-dom"
import {useState} from "react";
import PageDescription from '../Components/PageDescription';
import "../CSS/ProjectDetails.css";
import ConvertDate from '../Utilities/ConvertDate';
import ConvertTime from '../Utilities/ConvertTime';

const ProjectDetails = ({match}) => {
    const [projectDetails, setProjectDetails] = useState({});
    const [projectMembers, setProjectMembers] = useState([]);
    const [projectUserRoles, setProjectUserRoles] = useState([]);
    const [recentProjectTasks, setRecentProjectTasks] = useState([]);
    const [projectTaskTypes, setProjectTaskTypes] = useState([]);

    useEffect(() => {
        fetchProjectData();
        fetchUserData();
        fetchRecentProjectTasks();
        fetchTaskTypes();
    }, []);

    const fetchProjectData = async () => {
        const res = await fetch(`/project/${match.params.projectId}`);
        const data = await res.json();
        setProjectDetails(data);
    };

    const fetchUserData = async() => {
        const res = await fetch(`/project/${match.params.projectId}/users`);
        const data = await res.json();
        setProjectMembers(data);
        //we fetch project user roles inside the fetch user data function because
        //we do not want the former data to be retreived before the latter data
        fetchProjectUserRoles();
    };

    const fetchProjectUserRoles = async () => {
        const res = await fetch(`/project/${match.params.projectId}/roles`);
        const data = await res.json();
        setProjectUserRoles(data);
    };

    const fetchRecentProjectTasks = async () => {
        //get latest 15 tasks from project
        const res = await fetch(`/project/${match.params.projectId}/tasks?numOfTasks=15`);
        const data = await res.json();
        setRecentProjectTasks(data);
    };

    const fetchTaskTypes = async () => {
        const res = await fetch(`/project/${match.params.projectId}/taskTypes`);
        const data = await res.json();
        setProjectTaskTypes(data);
    };

    const getUserRole = (userId) => {
        for(let i = 0; i < projectUserRoles.length; i++) {
            if(projectUserRoles[i].userId === userId) {
                return projectUserRoles[i].role;
            }
        }
    };
    const getTaskTypeName = (typeId) => {
        for(let i = 0; i < projectTaskTypes.length; i++) {
            if(projectTaskTypes[i].taskTypeId === typeId) {
                return projectTaskTypes[i].name;
            }
        }
    }

    const renderProjectUser = (member) => {
        return(
            <li key={member.userId} className="subsection-row">
                <div className="user-name subsection-column">
                    <Link to={`/user/${member.userId}`}>{member.firstName} {member.middleName} {member.lastName}</Link>
                </div>
                <div className="user-role subsection-column">
                    {getUserRole(member.userId)}
                </div>                                        
                <Link to={`/projects/${match.params.projectId}/users/${member.userId}`}>
                    <div className="user-activity subsection-column">
                        User Activity
                    </div>
                </Link>
            </li>
        );
    }

    const renderRecentProjectTasks = (task) => {
        // console.log(task);

        return(
            <Link to={`/projects/${match.params.projectId}/task/${task.taskId}`} key={task.taskId}>
                <li className="subsection-row subsection-list">
                    <div className="task-name subsection-column">
                        {task.name} 
                    </div>                                    
                    {/* <Link to={`projects/${match.params.projectId}/users/${member.userId}`}> */}
                    <div className="task-type subsection-column">
                        {getTaskTypeName(task.taskId)}
                    </div>
                    <div className="task-urgency subsection-column">
                        {task.taskUrgency}
                    </div>
                </li>
            </Link>
        );
    }


    return (
        <div className="page">
            <div className="project-details">
                <PageDescription title={projectDetails.name} description="Here is a brief overview of your project with information curated for you"></PageDescription>
                <div className="project-description">
                    {projectDetails.description}
                </div>
                <div className="project-dates">
                    <div className="">
                        Created on: {ConvertDate(projectDetails.timeCreated)} at {ConvertTime(projectDetails.timeCreated)}
                    </div>
                </div>
                <Link to={`/projects/${match.params.projectId}/edit`}>
                    <button type="button" className="btn btn-lg create-button">Edit Project</button>
                </Link>
                <button type="button" className="btn btn-lg create-button">View Project History</button>
                <button type="button" className="btn btn-lg create-button ">Manage Task Types</button>
                <Link to={`/projects/${match.params.projectId}/delete`}>
                    <button type="button" className="btn btn-lg btn-danger delete-button">Delete Project</button>
                </Link>
                <div className="project-body">
                    <div className="project-users subsection">
                        <div className="subsection-row subsection-header">
                            Project Team
                            <Link to={`/projects/${match.params.projectId}/users`}>
                                More Details
                            </Link>
                        </div>
                        <ul>
                            {projectMembers.map((member) => {
                                return(renderProjectUser(member));
                            })}
                        </ul>
                    </div>
                    <div className="recent-tasks subsection">
                        <div className="subsection-row subsection-header">
                            Recent Tasks
                            <Link to={`/projects/${match.params.projectId}/tasks`}>
                                View All Tasks
                            </Link>
                        </div>
                        <ul>
                            {recentProjectTasks.map((task) => {
                                return(renderRecentProjectTasks(task));
                            })}
                        </ul>
                    </div>
                </div>

            </div>
        </div>
    );
}

export default ProjectDetails;