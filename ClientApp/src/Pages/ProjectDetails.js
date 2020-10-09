/**/
/*
 * This file represents the ProjectDetails page in the web application
 * It consists of the ProjectDetails functional component that handles the rendering of the 
 * main page for a project and also the communication with the server to get all the information 
 * for a particular project
 * / 
/**/
import React, { useEffect } from 'react';
import {Link} from "react-router-dom"
import {useState} from "react";
import PageDescription from '../Components/PageDescription';
import "../CSS/ProjectDetails.css";
import ConvertDate from '../Utilities/ConvertDate';
import ConvertTime from '../Utilities/ConvertTime';
import CheckAuthentication from '../Utilities/CheckAuthentication';
import LoadingSpinner from '../Utilities/LoadingSpinner';

/**/
/*
 * NAME:
 *      ProjectDetails() - React functional component corresponding to the ProjectDetails page
 * SYNOPSIS:
 *      ProjectDetails({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to list the details of a project, i.e. the main project page
 *      This components handles the retrieval of all data corresponding to the project's details needed to be displayed
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/18/2020 
 * /
 /**/
const ProjectDetails = ({match}) => {
    const [projectDetails, setProjectDetails] = useState({});
    const [projectMembers, setProjectMembers] = useState([]);
    const [projectUserRoles, setProjectUserRoles] = useState([]);
    const [recentProjectTasks, setRecentProjectTasks] = useState([]);
    const [projectTaskTypes, setProjectTaskTypes] = useState([]);
    const [currentUserRole, setCurrentUserRole]= useState("");
    const [projectActivities, setProjectActivities] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);

    //useEffect hook
    //called when the page is first rendered
    useEffect(() => {
        CheckAuthentication();
        fetchProjectActivity();
        getCurrentUserRole();
        fetchProjectData();
        fetchUserData();
        fetchRecentProjectTasks();
        fetchTaskTypes();
    }, []);


    /**/
    /*
    * NAME:
    *      getCurrentUserRole() - async function to retrieve the current user (i.e. the user using the web application) and his role
    * SYNOPSIS:
    *      getCurrentUserRole()
    * DESCRIPTION:
    *      Makes a GET request to server to receive information on the user and set the currentUserRole state accordingly
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/18/2020 
    * /
    /**/
    const getCurrentUserRole = async () => {
        const res = await fetch(`/user/${match.params.projectId}/user-role`);
        const data = await res.json();
        setCurrentUserRole(data.role);
    }

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
    *      10/04/2020 
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
    *      fetchUserData() - async function to retrieve information on all the users in a project
    * SYNOPSIS:
    *      fetchUserData()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing all the users in the project
    *      Sets the projectMembers state corresponding to retrieved information
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/18/2020 
    * /
    /**/
    const fetchUserData = async() => {
        const res = await fetch(`/project/${match.params.projectId}/users`);
        const data = await res.json();
        setProjectMembers(data);
        //we fetch project user roles inside the fetch user data function because
        //we do not want the former data to be retreived before the latter data
        fetchProjectUserRoles();
    };

    /**/
    /*
    * NAME:
    *      fetchProjectUserRoles() - async function to retrieve data on all the roles of each member in the project
    * SYNOPSIS:
    *      fetchProjectUserRoles()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on project users.
    *      Sets the projectMembers state corresponding to fetched data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/18/2020 
    * /
    /**/
    const fetchProjectUserRoles = async () => {
        const res = await fetch(`/project/${match.params.projectId}/roles`);
        const data = await res.json();
        setProjectUserRoles(data);
    };

    /**/
    /*
    * NAME:
    *      fetchRecentProjectTasks() - async function to retrieve recent tasks data from server
    * SYNOPSIS:
    *      fetchRecentProjectTasks()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing the 10 most recently created tasks of the project
    *      Sets the recentProjectTasks state corresponding to fetched data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/22/2020 
    * /
    /**/
    const fetchRecentProjectTasks = async () => {
        //get latest 15 tasks from project
        const res = await fetch(`/project/${match.params.projectId}/tasks/recent/10`);
        const data = await res.json();
        setRecentProjectTasks(data);
    };

    /**/
    /*
    * NAME:
    *      fetchProjectActivity() - async function to retrieve recent project activity data from server
    * SYNOPSIS:
    *      fetchProjectActivity()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing the 10 most recent the activities of the project
    *      Sets the recentProjectActivities state corresponding to fetched data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/22/2020 
    * /
    /**/
    const fetchProjectActivity = async () => {
        const response = await fetch(`/project/${match.params.projectId}/activity/10`);
        const data = await response.json();
        setProjectActivities(data);
        setContentLoaded(true);
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
    *      getUserRole() - returns the role of the user in the project given their user id
    * SYNOPSIS:
    *      getUserRole(userId)
    *           userId      -->     the user's Id
    * DESCRIPTION:
    *      Looks through all the elements in the projectUserRoles array to find the matching user Id, and
    *       then returns the role associated with the entry
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const getUserRole = (userId) => {
        for(let i = 0; i < projectUserRoles.length; i++) {
            if(projectUserRoles[i].appUserId === userId) {
                return projectUserRoles[i].role;
            }
        }
    };

    /**/
    /*
    * NAME:
    *      findTaskTypeId() - finds the id of a task type given its name
    * SYNOPSIS:
    *      findTaskTypeId(taskTypeName)
    *             taskTypeName  -->  the name of the task type whose Id is to be found
    * DESCRIPTION:
    *      This function finds what the task type id is given the name of a task type.
    * RETURNS
    *       The id of the given task type
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

    /**/
    /*
    * NAME:
    *      renderProjectUser() - React functional component that displays a project user along with his/her role and the user activity button
    * SYNOPSIS:
    *      renderProjectUser(member, index)
    *           member --> the project member 
    *           index --> the position of the project member in the array of the list of project members
    * DESCRIPTION:
    *      A React functional component that generates JSX to render the given project member into the page
    * RETURNS
    *      JSX that renders the project member info in the page
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/22/2020 
    * /
    /**/
    const renderProjectUser = (member, index) => {
        return(
            <li key={index} className="subsection-row">
                <div className="user-name subsection-column">
                    <Link to={`/profile/${member.id}`}>{member.firstName} {member.middleName} {member.lastName}</Link>
                </div>
                <div className="user-role subsection-column">
                    {getUserRole(member.id)}
                </div>                                        
                <Link to={`/projects/${match.params.projectId}/users/${member.id}`}>
                    <div className="user-activity subsection-column">
                        User Activity
                    </div>
                </Link>
            </li>
        );
    }

    /**/
    /*
    * NAME:
    *      renderRecentProjectTask() - React functional component that displays a project task and related information
    * SYNOPSIS:
    *      renderRecentProjectTask(task)
    *           task --> the task to be displayed
    * DESCRIPTION:
    *      A React functional component that generates JSX to render the given task into the page
    * RETURNS
    *      JSX that renders the task info in the page
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/22/2020 
    * /
    /**/
    const renderRecentProjectTask = (task) => {
        // console.log(task);

        return(
            <Link to={`/projects/${match.params.projectId}/task/${task.taskId}`} key={task.taskId}>
                <li className="subsection-row subsection-list">
                    <div className="task-name subsection-column">
                        {task.name} 
                    </div>                                    
                    {/* <Link to={`projects/${match.params.projectId}/users/${member.userId}`}> */}
                    <div className="task-type subsection-column">
                        {getTaskTypeName(task.taskTypeId)}
                    </div>
                    <div className="task-urgency subsection-column">
                        {task.urgency}
                    </div>
                </li>
            </Link>
        );
    }

    //return the JSX that generates the page. 
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

                
                {/* Show this option only to the administrator */}
                {currentUserRole == "Administrator" ? 
                    <div className="only-for-administrator">
                        <Link to={`/projects/${match.params.projectId}/edit`}>
                            <button type="button" className="btn btn-lg create-button">Edit Project</button>
                        </Link>

                        <Link to={`/projects/${match.params.projectId}/task-types`}>
                            <button type="button" className="btn btn-lg create-button ">Manage Task Types</button>
                        </Link>

                        <Link to={`/projects/${match.params.projectId}/delete`}>
                            <button type="button" className="btn btn-lg btn-danger delete-button">Delete Project</button>
                        </Link>
                    </div>
                    : ""
                }
                
                <div className="project-body">
                    <div className="project-users subsection">
                        <div className="subsection-row subsection-header">
                            Project Team
                            {/* only show manage button to administrators */}
                            {currentUserRole == "Administrator" ?
                            <Link to={`/projects/${match.params.projectId}/users`}>
                                Manage
                            </Link>
                            : ""
                            }
                            
                        </div>
                        <ul>
                            {projectMembers.map((member, index) => {
                                return(renderProjectUser(member, index));
                            })}
                        </ul>
                    </div>
                    <div className="recent-tasks subsection">
                        <div className="subsection-row subsection-header">
                            Recent Tasks
                            <Link to={`/projects/${match.params.projectId}/tasks`} className="view-and-manage-tasks">
                                View And Manage Tasks
                            </Link>
                        </div>
                        <ul>
                            {recentProjectTasks.map((task) => {
                                return(renderRecentProjectTask(task));
                            })}
                        </ul>
                    </div>
                    <div className="recent-activities subsection">
                        <div className="subsection-row subsection-header">
                            Recent Project Activities
                            <Link to={`/projects/${match.params.projectId}/activities`}>
                                View All Project Activities
                            </Link>
                        </div>
                        <div>
                            {contentLoaded ? projectActivities.map((activity, index) => {
                                return(
                                    <Link to={`/projects/${activity.projectId}/task/${activity.taskId}`}>
                                        <div className="project-activity" key={index}>
                                            {activity.activity}
                                        </div>
                                    </Link>
                                );
                            })
                            :
                            <div className="spinner"><LoadingSpinner/></div>
                        }
                        </div> 
                    </div>
                </div>

            </div>
        </div>
    );
}

export default ProjectDetails;