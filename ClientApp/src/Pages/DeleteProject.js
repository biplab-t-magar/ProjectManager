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


/**/
/*
 * NAME:
 *      DeleteProject() - React functional component corresponding to the DeleteProject page
 * SYNOPSIS:
 *      DeleteProject({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project to be deleted
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to delete a project
 *      This components handles the retrieval of the project data, generation of buttons, and the sending of data to the 
 *      server in order to communicate deletion
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/26/2020 
 * /
 /**/
const DeleteProject = ({match}) => {
    //React useState hook to manipulate projectDetails state
    const [projectDetails, setProjectDetails] = useState({});

    //useEffect hook that is called on the first rendering of the page, making sure that the user is authenticated
    // and fetching project info from the page
    useEffect(() => {
        CheckAuthentication();
        fetchProjectData();
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
    *      09/26/2020 
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
    *      onDelete(e) - asynchronous function that handles the click on the Delete button by a user
    * SYNOPSIS:
    *      onDelete(e)
    *           e --> the JavaScript event generated when submitting the form
    * DESCRIPTION:
    *      This function executes once the user has confirmed that he wants to delete the project
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/27/2020 
    * /
    /**/
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
    //Return JSX needed to render the page
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