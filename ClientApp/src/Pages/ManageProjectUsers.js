import React, {useState, useEffect} from "react";
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription";
import LoadingSpinner from '../Utilities/LoadingSpinner';
import "../CSS/ManageProjectUsers.css";

const ManageProjectUsers = ({match}) => {
    const [projectDetails, setProjectDetails] = useState({});
    const [projectMembers, setProjectMembers] = useState([]);
    const [projectUserRoles, setProjectUserRoles] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);
    const [userNameToInvite, setUserNameToInvite] = useState("");

    useEffect(() => {
        fetchProjectData();
        fetchUserData();
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

    const getUserRole = (userId) => {
        for(let i = 0; i < projectUserRoles.length; i++) {
            if(projectUserRoles[i].userId === userId) {
                return projectUserRoles[i].role;
            }
        }
    };


    return(
        <div className="page">
            <div className="manage-user-users">
                <PageDescription 
                    title={` Manage users for ${(projectDetails.name ? projectDetails.name : "")}`} 
                    description="Make changes to user roles and add new users to your team"
                />
                <div className="users-list">
                    <div className="user-list-header user-list-row">
                        <div className="user-name column">UserName</div>
                        <div className="user-full-name column">Full Name</div>
                        <div className="user-role column">Role</div>
                        <div className="user-change column">Change</div>
                    </div>
                    <div>
                        {contentLoaded ? 
                            projectMembers.map((member ) => {
                                return (
                                    <div  className="user-entry user-list-row">
                                        <Link to={`/user/${member.userId}`}>
                                            <div className="user-name column">{member.userName}</div>
                                        </Link>
                                        <div className="user-full-name column">{member.firstName} {member.lastName}</div>
                                        <div className="user-role column">{getUserRole(member.userId)}</div>
                                        <div className="user-change column">
                                            <button type="button manage-user" className="btn btn-sm create-button">
                                                Change to {`${member.role === "Member" ? "Administrator" : "Member"}`}
                                            </button>
                                        </div>
                                    </div>
                                );
                            }) : <div className="spinner"><LoadingSpinner /> </div>
                        }
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
                        <button className="btn btn-lg create-button">Invite </button>
                    </div>
                    
                    <div className="users-invited">
                        <div className="pending-invitations-header">
                            Pending Invitations
                        </div>
                        <div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default ManageProjectUsers;