/**/
/*
 * This file represents the UserActivityInProject page in the web application
 * It consists of the UserActivityInProject functional component that handles the rendering of the 
 * page display and also the communication with the server to receive information on a user's activities in a project
 * / 
/**/

import React, { useEffect, useState } from "react";
import PageDescription from "../Components/PageDescription";
import LoadingSpinner from "../Utilities/LoadingSpinner";
import "../CSS/UserActivityInProject.css";
import { Link } from "react-router-dom";
import CheckAuthentication from "../Utilities/CheckAuthentication";

/**/
/*
 * NAME:
 *      UserActivityInProject() - React functional component corresponding to the UserActivityInProject page
 * SYNOPSIS:
 *      UserActivityInProject({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project 
 *          match.params.userId --> the id of the user
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to list all of a user's activities in a project.
 *      This components handles the retrieval of all data corresponding to the user's project activities
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/4/2020 
 * /
 /**/
const UserActivityInProject = ({match}) => {
    const [projectDetails, setProjectDetails] = useState({});
    const [user, setUser] = useState({});
    const [userProjectActivities, setUserProjectActivities] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);

    //useEffect hook to be called on the first render of the page
    useEffect(() => {
        CheckAuthentication();
        fetchUserActivity();
        fetchProjectData();
        fetchUserInfo();
    }, [])

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
    *      fetchUserInfo() - async function to retrieve user info from server
    * SYNOPSIS:
    *      fetchUserInfo()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the user.
    *      Sets the setUser state corresponding to user data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchUserInfo = async () => {
        const response = await fetch(`/user/${match.params.userId}`);
        const data = await response.json();
        setUser(data);
    }

    /**/
    /*
    * NAME:
    *      fetchUserActivity() - async function to retrieve a user's project activity data from server
    * SYNOPSIS:
    *      fetchUserActivity()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing all the activities by the user in the project
    *      Makes the userProjectActivities state to correspond to fetched data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchUserActivity = async () => {
        const response = await fetch(`/project/${match.params.projectId}/user/${match.params.userId}/activity`);
        const data = await response.json();
        setUserProjectActivities(data);
        setContentLoaded(true);
    }

    //return the JSX that generates the page. 
    return(
        <div className="page">
            <div className="user-activity-in-project">
                <PageDescription 
                    title={`${user ? user.firstName : "" }'s activity in ${projectDetails ? projectDetails.name : ""}`} 
                    description={`Here are all of ${user.firstName}'s activites in this project`}
                />
                <div className="user-activity-in-project">
                    <div className="user-activity-in-project-row user-activity-in-project-header">
                        {user ? user.firstName : ""}'s Project Activities
                    </div>
                    {contentLoaded ? userProjectActivities.map((activity, index) => {
                        return(
                        
                            <Link to={`/projects/${activity.projectId}/task/${activity.taskId}`}>
                                <div className="user-activity-in-project-row" key={index}>
                                    {activity.activity}
                                </div>
                            </Link>
                            
                        );
                    })
                    : <div className="spinner"><LoadingSpinner/></div>}
                </div>
            </div>
        </div>
    );
}

export default UserActivityInProject;