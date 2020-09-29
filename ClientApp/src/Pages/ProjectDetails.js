import React, { useEffect } from 'react';
import {Link} from "react-router-dom"
import {useState} from "react";
import PageDescription from '../Components/PageDescription';
import ProjectMemberListing from '../Components/ProjectMemberListing';
import "../CSS/ProjectDetails.css";

const ProjectDetails = ({match}) => {
    const [projectDetails, setProjectDetails] = useState({});
    const [projectMembers, setProjectMembers] = useState([]);
    const [projectUserRoles, setProjectUserRoles] = useState([]);

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
    }

    const fetchProjectUserRoles = async () => {
        const res = await fetch(`/project/${match.params.projectId}/roles`);
        const data = await res.json();
        setProjectUserRoles(data);
    }

    const findUserRole = (userId) => {
        for(let i = 0; i < projectUserRoles.length; i++) {
            if(projectUserRoles[i].userId == userId) {
                console.log(projectUserRoles[i].role);
                return projectUserRoles[i].role;
            }
        }
    }

    return (
        <div className="page">
            <div className="project-details">
                <PageDescription title={projectDetails.name} description="Here is a brief overview of your project with information curated for you"></PageDescription>
                <div className="project-description">
                    {projectDetails.description}
                </div>
                <button type="button" className="btn btn-lg create-button create-button">Edit Project</button>
                <button type="button" className="btn btn-lg create-button create-button">View Project History</button>
                <div className="project-details-body">
                    <div className="project-users">
                        <h3>Project Team</h3>
                        <ul>
                            {projectMembers.map((member) => {
                                return(
                                    <li key={member.userId}>
                                        <ProjectMemberListing user={member}/>
                                <span>{findUserRole(member.userId)}</span>
                                        <Link to={`projects/${match.params.projectId}/users/${member.userId}`}>User Activity</Link>
                                    </li>
                                );
                            })}
                        </ul>
                    </div>
                </div>

            </div>
            
        </div>
    );
}

export default ProjectDetails;