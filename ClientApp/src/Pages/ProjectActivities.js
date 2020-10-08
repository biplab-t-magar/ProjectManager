import React, { useEffect, useState } from "react";
import PageDescription from "../Components/PageDescription";
import LoadingSpinner from "../Utilities/LoadingSpinner";
import "../CSS/UserActivityInProject.css";
import { Link } from "react-router-dom";
import CheckAuthentication from "../Utilities/CheckAuthentication";


const ProjectActivities = ({match}) => {
    const [projectDetails, setProjectDetails] = useState({});
    const [projectActivities, setProjectActivities] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);

    useEffect(() => {
        CheckAuthentication();
        fetchActivity();
        fetchProjectData();
    }, [])

    const fetchProjectData = async () => {
        const res = await fetch(`/project/${match.params.projectId}`);
        const data = await res.json();
        setProjectDetails(data);
    };


    const fetchActivity = async () => {
        const response = await fetch(`/project/${match.params.projectId}/activity`);
        const data = await response.json();
        setProjectActivities(data);
        setContentLoaded(true);
    }

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