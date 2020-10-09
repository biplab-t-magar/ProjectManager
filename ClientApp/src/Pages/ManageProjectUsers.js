/**/
/*
 * This file represents the ManageUserProjects page in the web application
 * It consists of the ManageUserProjects functional component that handles the rendering of the 
 * page display and also the communication with the server to invite users to a project and to change
 * the roles of users in projects
 * / 
/**/
import React, {useState, useEffect} from "react";
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription";
import LoadingSpinner from '../Utilities/LoadingSpinner';
import "../CSS/ManageProjectUsers.css";
import CheckAuthentication from "../Utilities/CheckAuthentication";


/**/
/*
 * NAME:
 *      ManageProjectUsers() - React functional component corresponding to the ManageProjectUsers page
 * SYNOPSIS:
 *      ManageProjectUsers({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to manage the users in a project
 *      This components handles the retrieval of data, and the sending of data to the 
 *      server, thus handling the processes of inviting users a project and changing their roles
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/04/2020 
 * /
 /**/
const ManageProjectUsers = ({match}) => {
    //useState hooks
    const [projectDetails, setProjectDetails] = useState({});
    const [projectMembers, setProjectMembers] = useState([]);
    const [projectUserRoles, setProjectUserRoles] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);
    const [userNameToInvite, setUserNameToInvite] = useState("");
    const [userToInviteError, setUserToInviteError] = useState("");
    const [pendingInvitees, setPendingInvitees] = useState([]);
    const [changeRoleError, setChangeRoleError] = useState("");

    //useEffect hook called on first render
    useEffect(() => {
        CheckAuthentication();
        fetchProjectData();
        fetchUserData();
        fetchPendingInvitees();
    }, []);

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
    *      fetchUserData() - async function to retrieve data on all the members of the project
    * SYNOPSIS:
    *      fetchUserData()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on project users.
    *      Sets the projectMembers state corresponding to fetched data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
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
    *      10/04/2020 
    * /
    /**/
    const fetchProjectUserRoles = async () => {
        const res = await fetch(`/project/${match.params.projectId}/roles`);
        const data = await res.json();
        setProjectUserRoles(data);
        setContentLoaded(true);
    };

    /**/
    /*
    * NAME:
    *      fetchPendingInvitees() - async function to retrieve all the pending invites to the project
    * SYNOPSIS:
    *      fetchPendingInvitees()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing all the pending invites to the project
    *      Sets the pendingInvites state corresponding to the fetched data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const fetchPendingInvitees = async () => {
        const res = await fetch(`/project/${match.params.projectId}/invitees`);
        const data = await res.json();
        console.log(data);
        setPendingInvitees(data);
    }

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
    *      cancelInvitation() - async function that cancels an invitation to a user
    * SYNOPSIS:
    *      cancelInvitation(userId)
    *           userId      -->     the user's Id
    * DESCRIPTION:
    *      This function makes a GET request to the server to cancel the inviation to the project sent to 
    *       the user with the given user Id. It also updates the pendingInvitees array to reflect the changes in the 
    *       invitees list
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const cancelInvitation = async (userId) => {
        //making post request to server
        const response = await fetch(`/project/${projectDetails.projectId}/cancel/${userId}` , {
            method: "DELETE",
            headers: {
                "Accept": "application/json",
                "Content-type": "application/json"
            },
        });
        const data = await response.json();
        
        if(!response.ok) {
            console.log(data);
        } else {
            setPendingInvitees(data);
        }
    }
    /**/
    /*
    * NAME:
    *      inviteUser() - async function that sends an invite to the specified user
    * SYNOPSIS:
    *      inviteUser(e)
    *           e --> the JavaScript event generated when submitting the form
    * DESCRIPTION:
    *      This function makes a GET request to the server to invite a user to the project. It also updates the 
    *       array containing the invited users to reflect the latest change
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const inviteUser = async (e) => {
        let errorsExist = false;
        //prevent default action
        e.preventDefault();
        //check invitee name 
        if(userNameToInvite.length === 0) {
            setUserToInviteError("You must include a user name to invite");
            errorsExist = true;
        } 
        else {
            setUserToInviteError("");
        }

        if(errorsExist == false) {
            //this payload adheres to the UtilityInviteModel in the server
            const payload = {
                inviteeUserName: userNameToInvite,
                projectId: projectDetails.projectId
            }
            //making post request to server
            const response = await fetch("/project/invite" , {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(payload)
            });
            const data = await response.json();
            
            //if error inviting the user, then set userToInviteError state to display the error
            if(!response.ok) {
                console.log(data);
                setUserToInviteError(data);
            } else {
                //update the Invitees list
                let updatedInvitees = [...pendingInvitees];
                updatedInvitees.push(data);
                setPendingInvitees(updatedInvitees);
                setUserToInviteError("");
            }
        }
    }   

    /**/
    /*
    * NAME:
    *      switchRole() - an async function that switches the role of a user
    * SYNOPSIS:
    *      switchRole(user)
    *           user      -->     the user whose role is to be switched
    * DESCRIPTION:
    *      Sends a request to the server to switch the user's role 
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/04/2020 
    * /
    /**/
    const switchRole = async (user) => {
        const payload = {
            ProjectId: projectDetails.projectId,
            AppUser: user,
            AppUserId: user.id,
            Role: getUserRole(user.id)
        }
        //making post request to server
        const response = await fetch("/project/switch-role" , {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-type": "application/json"
            },
            body: JSON.stringify(payload)
        });
        
        if(!response.ok) {
            const data = await response.json();
            setChangeRoleError(data);

        } else {
            fetchProjectUserRoles();
        }
    }

    //return the JSX that generates the page. 
    return(
        <div className="page">
            <div className="manage-users">
                <PageDescription 
                    title={` Manage users for ${(projectDetails.name ? projectDetails.name : "")}`} 
                    description="Make changes to user roles and add new users to your team"
                />
                <div className="users-list">
                    <div className="user-list-header user-list-row">
                        <div className="user-name column">UserName</div>
                        <div className="user-full-name column">Full Name</div>
                        <div className="user-role column">Role</div>
                        <div className="user-change column"></div>
                    </div>
                    <div>
                        {contentLoaded ? 
                            projectMembers.map((member, index ) => {
                                return (
                                    <div key={index} className="user-entry user-list-row" >
                                        <Link to={`/profile/${member.id}`} className="user-name column">
                                            {member.userName}
                                        </Link>
                                        <div className="user-full-name column">{member.firstName} {member.lastName}</div>
                                        <div className="user-role column">{getUserRole(member.id)}</div>
                                        <div className="user-change column">
                                            <button onClick={() => switchRole(member)} type="button manage-user" className="btn btn-sm create-button">
                                                Change to {`${getUserRole(member.id) === "Member" ? "Admin" : "Member"}`}
                                            </button>
                                            
                                        </div>
                                    </div>
                                );
                            }) : <div className="spinner"><LoadingSpinner /> </div>
                        }
                        <small className="error-message">
                            {changeRoleError ? changeRoleError : ""}
                        </small>
                    </div>
                </div>
                <div className="invite">
                    <div className="invite-users">
                        <div className="invite-users-header">Invite Users to Project</div>
                        <input 
                            type="text" 
                            className="user-name-input" 
                            id="user-name-input" 
                            placeholder="Enter User Name" 
                            value={userNameToInvite} 
                            onChange={(e) => setUserNameToInvite(e.target.value)}
                        />
                        
                        <button onClick={inviteUser} className="btn btn-lg create-button">Invite </button>
                        <small className="error-message">
                            {userToInviteError ? userToInviteError : ""}
                        </small>
                    </div>
                    
                    <div className="pending-invitations">
                        <div className="pending-invitations-header">
                            Pending Invitations
                        </div>
                        <div className="pending-invitations-list">
                            {pendingInvitees.map((invitee, index) => {
                                    return (
                                        <div  className="invitation" key={index}>
                                            <Link to={`/profile/${invitee.userName}`}>
                                                <div className="invitee-username invitation-column">{invitee.userName}</div>
                                            </Link>
                                            <div className="cancel-invitation invitation-column">
                                                <button onClick={() => cancelInvitation(invitee.id)} type="button manage-user" className="btn btn-sm create-button">
                                                    Cancel Invitation
                                                </button>
                                            </div>
                                        </div>
                                    );
                                }) 
                            }
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default ManageProjectUsers;