/**/
/*
 * This file represents the UserProfile page in the web application
 * It consists of the UserProfile functional component that handles the rendering of the 
 * page display and also the communication with the server to receive information on a user, along 
 * with communication required for accepting and rejecting project invities
 * / 
/**/

import React, {useState, useEffect} from "react";
import PageDescription from "../Components/PageDescription";
import CheckAuthentication from "../Utilities/CheckAuthentication.js";
import "../CSS/UserProfile.css";
import { Link } from "react-router-dom";
import LoadingSpinner from "../Utilities/LoadingSpinner.js";

/**/
/*
 * NAME:
 *      UserProfile() - React functional component corresponding to the UserProfile page
 * SYNOPSIS:
 *      UserProfile({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.userId --> the id of the user
 * DESCRIPTION:
 *      A React functional component that generates JSX to render information on the user
 *      This components handles the retrieval of all data corresponding to the user's info, including project invites and the 
 *      user's recent activities. It also contains renders edit functionality for the user's info
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/6/2020 
 * /
 /**/
const UserProfile = ({match}) => {
    //useState hooks
    const [user, setUser] = useState({});
    const [currentUser, setCurrentUser] = useState({});
    const [editingProfile, setEditingProfile] = useState(false);
    const [newFirstName, setNewFirstName] = useState("");
    const [newLastName, setNewLastName] = useState(""); 
    const [newBio, setNewBio] = useState("");
    const [firstNameError, setFirstNameError] = useState("");
    const [lastNameError, setLastNameError] = useState("");
    const [bioError, setBioError] = useState("");
    const [usersOwnProfile, setUsersOwnProfile] = useState(false);
    const [userActivity, setUserActivity] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);
    const [projectInvitations, setProjectInvitations] = useState([]);
    const [projectInviters, setProjectInviters] = useState([]);
    const [projectsInvitedTo, setProjectsInvitedTo] = useState([]);

    //useEffect hook to be called on first rendering of the page
    useEffect(() => {
        CheckAuthentication();
        fetchUserInfo();
        fetchCurrentUserInfo();
        fetchProjectInvitations();
        fetchProjectInviters();
        fetchProjectsInvitedTo();

    }, []);

    //useEffect hook to call when values for user or currentUser states changes
    //only fetch project invitations if user and current user are the same
    //make this comparison only when both user and current user have been fetched from the server
    useEffect(() => {
        if(user.id !== undefined && user.id === currentUser.id) {
            setUsersOwnProfile(true);
        }
    }, [user, currentUser])

    //useEffect hook to call when values for user state changes
    useEffect(() => {
        setNewFirstName(user.FirstName);
        setNewLastName(user.LastName);
        setNewBio(user.Bio);
    }, [user]);

    /**/
    /*
    * NAME:
    *      fetchUserInfo() - async function to retrieve info from server of the user whose profile this is
    * SYNOPSIS:
    *      fetchUserInfo()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the user whose profile page this page represents
    *      Sets the setUser state corresponding to user data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchUserInfo = async () => {
        if(match.params.userId) {
            const response = await fetch(`/user/${match.params.userId}`);
            const data = await response.json();
            setUser(data);
            fetchUserActivity(data.id);
        } else {
            const response = await fetch(`/user`);
            const data = await response.json();
            setUser(data);
            fetchUserActivity(data.id);
        }        
    }

    /**/
    /*
    * NAME:
    *      fetchCurrentUserInfo() - async function to retrieve info of the user who is currently using the web application
    * SYNOPSIS:
    *      fetchCurrentUserInfo()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the current user.
    *      Sets the setCurrentUser state to correspond to retrieved data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchCurrentUserInfo = async () => {
        const response = await fetch(`/user`);
        const data = await response.json();
        setCurrentUser(data);
    }
    /**/
    /*
    * NAME:
    *      fetchProjectInvitations() - async function to retrieve user's project invitations
    * SYNOPSIS:
    *      fetchProjectInvitations()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on all the project invitations recieved by a user
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchProjectInvitations = async () => {
        const response = await fetch('/user/project-invitations');
        const data = await response.json();
        setProjectInvitations(data);
    }

    /**/
    /*
    * NAME:
    *      fetchProjectInviters() - async function to retrieve all the user's that have invited the current user to their projects
    * SYNOPSIS:
    *      fetchProjectInviters()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on all users who invited the current user to their projects
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchProjectInviters = async () => {
        const response = await fetch('/user/project-inviters');
        const data = await response.json();
        setProjectInviters(data);
    }

    /**/
    /*
    * NAME:
    *      fetchProjectsInvitedTo() - async function to retrieve all the projects that the current user has been invited to
    * SYNOPSIS:
    *      fetchProjectsInvitedTo()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on all projects the current user has been invitied to
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchProjectsInvitedTo = async () => {
        const response = await fetch('/user/projects-invited-to');
        const data = await response.json();
        setProjectsInvitedTo(data);
    }

    /**/
    /*
    * NAME:
    *      fetchUserActivity() - async function to retrieve a user's activity data from server
    * SYNOPSIS:
    *      fetchUserActivity()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing all the activities by the user
    *      Sets the projectActivities state to correspond to fetched data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchUserActivity = async (userId) => {
        const response = await fetch(`/user/${userId}/activity/15`);
        const data = await response.json();
        setUserActivity(data);
        setContentLoaded(true);
    }

    /**/
    /*
    * NAME:
    *      renderGeneralInfo() - React functional component that render the username, description, and (if the profile is the current user's profile)
    *                             the Edit Profile button
    * SYNOPSIS:
    *      renderGeneralInfo()
    * DESCRIPTION:
    *      A React functional component that generates JSX to render the given user info
    * RETURNS
    *      JSX that renders the user info in the page
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
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
                {usersOwnProfile ?
                    <button onClick={() => setEditingProfile(true)} className="btn btn-primary edit-profile">Edit Profile</button>
                    : ""
                }
            </div>
        );
    }

    /**/
    /*
    * NAME:
    *      renderEditForm() - React functional component that renders the edit form, used to edit the current user's information
    * SYNOPSIS:
    *      renderEditForm()
    * DESCRIPTION:
    *      A React functional component that generates JSX to render the edit form
    * RETURNS
    *      JSX that renders the edit form in the page
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *     10/04/2020 
    * /
    /**/
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

    /**/
    /*
    * NAME:
    *      renderRecentActivity() - React functional component that renders the user's recent activities,
    * SYNOPSIS:
    *      renderRecentActivity()
    * DESCRIPTION:
    *      A React functional component that generates JSX to render the given user's recent activity
    * RETURNS
    *      JSX that renders the user activities in the page
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
    const renderRecentActivity = () => {
        return (
            <div className="user-activity">
                <div className="user-activity-row user-activity-header">
                    {usersOwnProfile ? 
                    "Your Recent Activity"
                    : `${user.firstName}'s Recent Activity`}
                </div>
                {!contentLoaded ? 
                <div className="spinner"><LoadingSpinner /> </div>
                :
                userActivity.map((activity, index) => {
                    return(
                        <div className="user-activity-row" key={index}>
                            {activity.activity}
                        </div>
                    );
                })
            }
            </div>
        );
    }

    /**/
    /*
    * NAME:
    *      renderProjectInvitations() - React functional component that renders the user's project invitations
    * SYNOPSIS:
    *      renderProjectInvitations()
    * DESCRIPTION:
    *      A React functional component that generates JSX to render the given user's project invitations
    * RETURNS
    *      JSX that renders the user's project invitiatons to the page
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
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
                                <Link to={`/profile/${invitation.inviterId}`}>{getUserNameFromId(invitation.inviterId)}</Link>
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

    /**/
    /*
    * NAME:
    *      changeUserInfo() - Asynchronous function that acts as a handler when user submits the Update Info form 
    * SYNOPSIS:
    *      changeUserInfo(e)
    *           e --> the JavaScript event generated when submitting the form
    * DESCRIPTION:
    *      This function first checks if the user's entered information is valid. It then makes a POST request to the server
    *       with the updated information so that the user's information is changed in the database
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
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
            //payloads adheres to the UpdateUserModel in the server
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

    /**/
    /*
    * NAME:
    *      getUserNameFromId() - gets the user name given the id of the user
    * SYNOPSIS:
    *      getUserNameFromId(userId)
    *           userId     -->    the id of the user whose name is to be found
    * DESCRIPTION:
    *      Gets the name of the user who id is given
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
    const getUserNameFromId = (userId) => {
        for(let i = 0; i < projectInviters.length; i++) {
            if(projectInviters[i].id == userId) {
                return projectInviters[i].firstName + " " + projectInviters[i].lastName;
            }
        }
    }

    /**/
    /*
    * NAME:
    *      getProjectNameFromId() - gets the project name given the id of the project
    * SYNOPSIS:
    *      getProjectNameFromId(projectid)
    *           projectId     -->    the id of the project whose name is to be found
    * DESCRIPTION:
    *      Gets the name of the project whose id is given
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
    const getProjectNameFromId = (projectId) => {
        for(let i = 0; i < projectsInvitedTo.length; i++) {
            if(projectsInvitedTo[i].projectId == projectId) {
                return projectsInvitedTo[i].name;
            }
        }
    }

    /**/
    /*
    * NAME:
    *      acceptInvite() - accepts the invite for a project on behalf of the current user
    * SYNOPSIS:
    *      acceptInvite(invitation)
    *           invitation     -->    the project invititation that is to be accepted
    * DESCRIPTION:
    *      Sends a POST request to the database that accepts the invitation and adds the user to the project
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/06/2020 
    * /
    /**/
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
        } else {
            const data = await response.json();
            console.log(data);
        }
        //after accepting invite, update the list of project invitations, project inviters, and projects invited to 
        //in the page
        fetchProjectInvitations();
        fetchProjectInviters();
        fetchProjectsInvitedTo();

    }

    /**/
    /*
    * NAME:
    *      declineInvite() - declines the invite for a project on behalf of the current user
    * SYNOPSIS:
    *      declineInvite(invitation)
    *           invitation     -->    the project invititation that is to be declined
    * DESCRIPTION:
    *      Sends a POST request to the database that declines the invitation 
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/06/2020 
    * /
    /**/
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
        //after accepting invite, update the list of project invitations, project inviters, and projects invited to 
        //in the page
        fetchProjectInvitations();
        fetchProjectInviters();
        fetchProjectsInvitedTo();
    }

    //return the JSX that generates the page. 
    return(
        <div className="page">
            <div className="user-profile">
                <PageDescription 
                    title={`${user.firstName}'s profile`}
                    description="Edit and view your profile info" 
                />
                {editingProfile ? renderEditForm() : renderGeneralInfo()}
                {usersOwnProfile ? renderProjectInvitations() : ""}
                {renderRecentActivity()}

            </div>
        </div>
    )
}

export default UserProfile;