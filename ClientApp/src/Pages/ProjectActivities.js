/**/
/*
 * This file represents the ProjectActivities page in the web application
 * It consists of the ProjectActivities functional component that handles the rendering of the 
 * page display and also the communication with the server to fetch all the activities in a project
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
 *      ProjectActivities() - React functional component corresponding to the ProjectActivities page
 * SYNOPSIS:
 *      ProjectActivities({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to list all the activities in a project.
 *      This components handles the retrieval of all data corresponding to the project's activities
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/4/2020 
 * /
 /**/
const ProjectActivities = ({match}) => {
    //useState hooks
    const [projectDetails, setProjectDetails] = useState({});
    const [projectActivities, setProjectActivities] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);

    //useEffect hook
    //called on the first rendering of the page
    useEffect(() => {
        CheckAuthentication();
        fetchActivity();
        fetchProjectData();
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
    *      fetchActivity() - async function to retrieve project activity data from server
    * SYNOPSIS:
    *      fetchActivity()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing all the activities of the project
    *      Sets the projectActivities state corresponding to fetched data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchActivity = async () => {
        const response = await fetch(`/project/${match.params.projectId}/activity`);
        const data = await response.json();
        setProjectActivities(data);
        setContentLoaded(true);
    }

    //return the JSX that generates the page. 
    return(
        <div className="page">
            <div className="user-activity-in-project">
                <PageDescription 
                    title={`All activity in ${projectDetails ? projectDetails.name : ""}`} 
                    description={`Here is a list of all the activites in this project`}
                />
                <div className="user-activity-in-project">
                    <div className="user-activity-in-project-row user-activity-in-project-header">
                        Activities in {projectDetails ? projectDetails.name : ""}
                    </div>
                    {contentLoaded ? projectActivities.map((activity, index) => {
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

export default ProjectActivities;