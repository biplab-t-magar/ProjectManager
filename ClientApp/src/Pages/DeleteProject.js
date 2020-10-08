/**/
/*
 * This file represents the DeleteProject page in the web application
 * It consists of the DeleteProject functional component that handles the rendering of the 
 * page display and also the communication with the server to delete the project
 * / 
/**/

import React, {useEffect, useState} from "react";
import {Link} from "react-router-dom";
import "../CSS/DeleteProject.css";
import CheckAuthentication from "../Utilities/CheckAuthentication";

const DeleteProject = ({match}) => {
    const [projectDetails, setProjectDetails] = useState({});
    useEffect(() => {
        CheckAuthentication();
        fetchProjectData();
    }, []);


    const fetchProjectData = async () => {
        const res = await fetch(`/project/${match.params.projectId}`);
        const data = await res.json();
        setProjectDetails(data);
    };


    const onDelete = async (e) => {
        //prevent default action
        e.preventDefault();
        //making post request to server
        const response = await fetch(`/project/${match.params.projectId}/delete` , {
            method: "DELETE"
        })
        if(response.ok) {
            window.location.pathname = "/projects";
        } else {
            console.log("Unable to delete");
        }

    }

    return(
        <div className="page">
            <div className="delete-project">
                <h1>
                    Are you sure you want to delete <span className="project-name">{projectDetails.name}</span>?
                </h1>
                <h3>This action cannot be reversed</h3>

                <button className="btn btn-danger delete-button" onClick={onDelete}>Delete Project</button>
                <Link to={`/projects/${match.params.projectId}`}>
                    <button className="btn btn-secondary cancel">Cancel</button>
                </Link>
            </div>
        </div>
    );
}

export default DeleteProject;