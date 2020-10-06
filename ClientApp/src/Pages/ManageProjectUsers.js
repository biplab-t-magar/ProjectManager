import React, {useState, useEffect} from "react";
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription";
import LoadingSpinner from '../Utilities/LoadingSpinner';
import "../CSS/ManageProjectUsers.css";
import CheckAuthentication from "../Utilities/CheckAuthentication";

const ManageProjectUsers = ({match}) => {
    const [projectDetails, setProjectDetails] = useState({});
    const [projectMembers, setProjectMembers] = useState([]);
    const [projectUserRoles, setProjectUserRoles] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);
    const [userNameToInvite, setUserNameToInvite] = useState("");
    const [userToInviteError, setUserToInviteError] = useState("");
    const [pendingInvitees, setPendingInvitees] = useState([]);
    const [changeRoleError, setChangeRoleError] = useState("");
    useEffect(() => {
        CheckAuthentication();
        fetchProjectData();
        fetchUserData();
        fetchPendingInvitees();
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
        setContentLoaded(true);
    };

    const fetchPendingInvitees = async () => {
        const res = await fetch(`/project/${match.params.projectId}/invitees`);
        const data = await res.json();
        setPendingInvitees(data);
    }

    const getUserRole = (userId) => {
        for(let i = 0; i < projectUserRoles.length; i++) {
            if(projectUserRoles[i].appUserId === userId) {
                return projectUserRoles[i].role;
            }
        }
    };

    const cancelInvitation = async (userId) => {
        console.log(projectDetails.projectId);
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
            
            if(!response.ok) {
                console.log(data);
                setUserToInviteError(data);
            } else {
                let updatedInvitees = [...pendingInvitees];
                updatedInvitees.push(data);
                setPendingInvitees(updatedInvitees);
                setUserToInviteError("");
            }
        }
    }   

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
                                        <Link to={`/user/${member.id}`} className="user-name column">
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
                        <small className="error-message">
                            {userToInviteError ? userToInviteError : ""}
                        </small>
                        <button onClick={inviteUser} className="btn btn-lg create-button">Invite </button>
                        
                    </div>
                    
                    <div className="pending-invitations">
                        <div className="pending-invitations-header">
                            Pending Invitations
                        </div>
                        <div className="pending-invitations-list">
                            {pendingInvitees.map((invitee, index) => {
                                    return (
                                        <div  className="invitation" key={index}>
                                            <Link to={`/user/${invitee.userName}`}>
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