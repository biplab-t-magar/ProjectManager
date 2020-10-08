import React, { useEffect, useState } from "react";
import PageDescription from "../Components/PageDescription";
import LoadingSpinner from "../Utilities/LoadingSpinner";
import "../CSS/UserActivityInProject.css";
import { Link } from "react-router-dom";
import CheckAuthentication from "../Utilities/CheckAuthentication";

const UserActivityInProject = ({match}) => {
    const [projectDetails, setProjectDetails] = useState({});
    const [user, setUser] = useState({});
    const [userProjectActivities, setUserProjectActivities] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);

    useEffect(() => {
        CheckAuthentication();
        fetchUserActivity();
        fetchProjectData();
        fetchUserInfo();
    }, [])

    const fetchProjectData = async () => {
        const res = await fetch(`/project/${match.params.projectId}`);
        const data = await res.json();
        setProjectDetails(data);
    };

    const fetchUserInfo = async () => {
        const response = await fetch(`/user/${match.params.userId}`);
        const data = await response.json();
        setUser(data);
    }

    const fetchUserActivity = async () => {
        const response = await fetch(`/project/${match.params.projectId}/user/${match.params.userId}/activity`);
        const data = await response.json();
        setUserProjectActivities(data);
        setContentLoaded(true);
    }

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