import React, {useState, useEffect} from "react";
import PageDescription from "../Components/PageDescription";
import CheckAuthentication from "../Utilities/CheckAuthentication.js";
import "../CSS/UserProfile.css";
import { Link } from "react-router-dom";

const UserProfile = ({match}) => {
    const [user, setUser] = useState({});
    const [currentUser, setCurrentUser] = useState({});
    const [projectInvitations, setProjectInvitations] = useState([]);
    const [projectInviters, setProjectInviters] = useState([]);
    const [projectsInvitedTo, setProjectsInvitedTo] = useState([]);

    useEffect(() => {
        CheckAuthentication();
        fetchUserInfo();
        fetchCurrentUserInfo();
    }, []);

    //only fetch project invitations if user and current user are the same
    //make this comparison only when both user and current user have been fetched from the server
    useEffect(() => {
        if(user.id === currentUser.id) {
            fetchProjectInvitations();
            fetchProjectInviters();
            fetchProjectsInvitedTo();
        }
    }, [user, currentUser])

    const fetchUserInfo = async () => {
        if(match.params.userId) {
            const response = await fetch(`/user/${match.params.userId}`);
            const data = await response.json();
            setUser(data);
        } else {
            const response = await fetch(`/user`);
            const data = await response.json();
            setUser(data);
        }
    }

    const fetchCurrentUserInfo = async () => {
        const response = await fetch(`/user`);
        const data = await response.json();
        setCurrentUser(data);
    }

    const fetchProjectInvitations = async () => {
        const response = await fetch('/user/project-invitations');
        const data = await response.json();
        setProjectInvitations(data);
    }

    const fetchProjectInviters = async () => {
        const response = await fetch('/user/project-inviters');
        const data = await response.json();
        setProjectInviters(data);
    }

    const fetchProjectsInvitedTo = async () => {
        const response = await fetch('/user/projects-invited-to');
        const data = await response.json();
        setProjectsInvitedTo(data);
    }

    const getUserNameFromId = (userId) => {
        for(let i = 0; i < projectInviters.length; i++) {
            if(projectInviters[i].id == userId) {
                return projectInviters[i].firstName + " " + projectInviters[i].lastName;
            }
        }
    }

    const getProjectNameFromId = (projectId) => {
        for(let i = 0; i < projectsInvitedTo.length; i++) {
            if(projectsInvitedTo[i].projectId == projectId) {
                return projectsInvitedTo[i].name;
            }
        }
    }

    const acceptInvite = async (invitation) => {
        const response = await fetch("/user/accept-invite" , {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-type": "application/json"
            },
            body: JSON.stringify(invitation)
        });
        if(!response.ok) {
            console.log(response);
        }
        fetchProjectInvitations();
        fetchProjectInviters();
        fetchProjectsInvitedTo();

    }

    const declineInvite = async (invitation) => {
        const response = await fetch("/user/decline-invite" , {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-type": "application/json"
            },
            body: JSON.stringify(invitation)
        });
        if(!response.ok) {
            console.log(response);
        }
        fetchProjectInvitations();
        fetchProjectInviters();
        fetchProjectsInvitedTo();
    }

    const renderProjectInvitations = () => {
        return(
            <div className="project-invites">
                <div className="project-invites-header">
                    Project Invitations
                </div>  
                {projectInvitations.map((invitation, index) => {
                    return(
                        <div key={index} className="invitation">
                            <strong>
                                <Link to={`/user/${invitation.inviterId}`}>{getUserNameFromId(invitation.inviterId)}</Link>
                            </strong> invited you to <strong>{getProjectNameFromId(invitation.projectId)}</strong>
                            <br />
                            <button onClick={() => acceptInvite(invitation)} id="accept-button" className="btn btn-success">Accept</button>
                            <button onClick={() => declineInvite(invitation)} id="decline-button" className="btn btn-danger">Decline</button>
                        </div>
                    );
                })}
            </div> 

        );
    }

    return(
        <div className="page">
            <div className="user-profile">
                <PageDescription 
                    title={`${user.firstName}'s profile`}
                    description="Edit and view your profile info" 
                />
                <div className="general-info">
                    <div className="name">
                        Name: {`${user.firstName} ${user.lastName}`}
                    </div>
                    <div className="bio">
                        {user.bio ? user.bio : "No bio available"}
                    </div>
                    {/* Is this the current user's profile? */}
                    {currentUser.id === user.id ?
                        <Link to="/profile/edit">
                            <button className="btn btn-primary edit-profile">Edit Profile</button>
                        </Link> 
                        : ""
                    }
                </div>
                {/* Only show projet invites if this is the current user's profile */}
                {currentUser.id === user.id ? renderProjectInvitations() : ""}
            </div>
        </div>
    )
}

export default UserProfile;