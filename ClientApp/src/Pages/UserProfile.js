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
    const [editingProfile, setEditingProfile] = useState(false);
    const [newFirstName, setNewFirstName] = useState("");
    const [newLastName, setNewLastName] = useState(""); 
    const [newBio, setNewBio] = useState("");
    const [firstNameError, setFirstNameError] = useState("");
    const [lastNameError, setLastNameError] = useState("");
    const [bioError, setBioError] = useState("");

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

    useEffect(() => {
        setNewFirstName(user.FirstName);
        setNewLastName(user.LastName);
        setNewBio(user.Bio);
    }, [user]);

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
                {projectInvitations.length == 0 ?
                <div>You do not have any project invitations</div>
                : projectInvitations.map((invitation, index) => {
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
    const renderGeneralInfo = () => {
        return (
            <div className="general-info">
                <div className="name">
                    Name: {`${user.firstName} ${user.lastName}`}
                </div>
                <div className="bio">
                    {user.bio ? user.bio : "No bio available"}
                </div>
                {/* Is this the current user's profile? If it is, show the Edit button */}
                {currentUser.id === user.id ?
                    <button onClick={() => setEditingProfile(true)} className="btn btn-primary edit-profile">Edit Profile</button>
                    : ""
                }
            </div>
        );
    }

    const renderEditForm = () => {
        return (
            <div className="edit-info">
                <form onSubmit={changeUserInfo}>
                    <div className="form-group">
                        <label htmlFor="first-name">First Name</label>
                        <input 
                            type="text" 
                            className="form-control" 
                            id="first-name" 
                            placeholder="First Name" 
                            value={newFirstName || ""} 
                            onChange={(e) => setNewFirstName(e.target.value)}
                        />
                        <small className="error-message">
                            {firstNameError ? firstNameError : ""}
                        </small>
                    </div>
                    <div className="form-group">
                        <label htmlFor="last-name">Last Name</label>
                        <input 
                            type="text" 
                            className="form-control" 
                            id="last-name" 
                            placeholder="Last Name" 
                            value={newLastName || ""} 
                            onChange={(e) => setNewLastName(e.target.value)}
                        />
                        <small className="error-message">
                            {lastNameError ? lastNameError : ""}
                        </small>
                        <label htmlFor="project-description">Bio (optional)</label>
                        <textarea 
                            type="text" 
                            className="form-control" 
                            id="bio" 
                            placeholder="Bio" 
                            value={newBio || ""} 
                            onChange={(e) => setNewBio(e.target.value)}
                        />
                        <small className="error-message">
                            {bioError ? bioError : ""}
                        </small>
                    </div>
                    <button id="submit-update-info" type="submit" className="btn btn-primary create">Update Info</button>
                    {/* //if cancel is clicked, then hide edit profile view */}
                    <button id="cancel-update-info" onClick={ () => setEditingProfile(false)} className="btn btn-secondary create">Cancel</button>
                </form>
            </div>
        );
    }

    const changeUserInfo = async (e) => {
        let errorsExist = false;
        //prevent default action
        e.preventDefault();
        //check project name 
        if(newFirstName.length === 0) {
            setFirstNameError("You must include your first name");
            errorsExist = true;
        } else if(newFirstName.length > 50) {
            setFirstNameError("Your first name should be no more than 50 characters.");
            errorsExist = true;
        } 
        else {
            setFirstNameError("");
        }

        if(newLastName.length === 0) {
            setLastNameError("You must include your first name");
            errorsExist = true;
        } else if(newLastName.length > 50) {
            setLastNameError("Your first name should be no more than 50 characters.");
            errorsExist = true;
        } 
        else {
            setLastNameError("");
        }

        //check projectDescriptionError
        if(newBio.length > 300) {
            setBioError("Your bio should be no more than 300 characters.");
            errorsExist = true;
        } 
        else {
            setBioError("");
        }

        if(errorsExist == false) {
            const payload = {
                FirstName: newFirstName,
                LastName: newLastName,
                Bio: newBio
            }
            //making post request to server
            const response = await fetch("/user/update-info" , {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(payload)
            })
            
            const data = await response.json();
            setEditingProfile(false);
            fetchUserInfo();
        }
    }

    return(
        <div className="page">
            <div className="user-profile">
                <PageDescription 
                    title={`${user.firstName}'s profile`}
                    description="Edit and view your profile info" 
                />
                {editingProfile ? renderEditForm() : renderGeneralInfo()}
                
                {/* Only show projet invites if this is the current user's profile */}
                {currentUser.id === user.id ? renderProjectInvitations() : ""}
            </div>
        </div>
    )
}

export default UserProfile;