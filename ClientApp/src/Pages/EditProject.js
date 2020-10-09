/**/
/*
 * This file represents the EditProject page in the web application
 * It consists of the EditProject functional component that handles the rendering of the 
 * page display and also the communication with the server to update the information of a project
 * / 
/**/

import React, { useState, useEffect } from "react";
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription.js";
import "../CSS/CreateNewProject.css"; //use same css as for create new project page because they look very similar
import CheckAuthentication from "../Utilities/CheckAuthentication.js";

/**/
/*
 * NAME:
 *      EditProject() - React functional component corresponding to the EditProject page
 * SYNOPSIS:
 *      EditProject({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to edit a project.
 *      This components handles the retrieval of data, generation of forms, and the sending of data to the 
 *      server, thus handling the process of editing a project
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/02/2020 
 * /
 /**/
const EditProject = ({match}) => {
    //state hooks with information to render in the page.
    const [projectDetails, setProjectDetails] = useState({});
    const [newProjectName, setNewProjectName] = useState("");
    const [projectNameError, setProjectNameError] = useState("");
    const [newProjectDescription, setNewProjectDescription] = useState("");
    const [projectDescriptionError, setProjectDescriptionError] = useState("");

    //checks authentication and fetches project data on first rendering on the page
    useEffect(() => {
        CheckAuthentication();
        fetchProjectData();
    }, []);

    //this useEffect hook is called everytime the value of the projectDetails state variable is changed
    useEffect(() => {
        setNewProjectName(projectDetails.name);
        setNewProjectDescription(projectDetails.description);
    }, [projectDetails]);

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
    *      10/02/2020 
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
    *      handleSubmit() - handles the submission of the edit project form
    * SYNOPSIS:
    *      handleSubmit()
    *           e --> the JavaScript event generated when submitting the form
    * DESCRIPTION:
    *      This function executes the action to be taken once the user has filled out the form and hit submit.
    *      First it validates the user input in the forms, and sets the error message if user input is not valid.
    *      If user input is valid, it sends a request to the server to edit the project with given information
    *      Finally, it redirects to the ProjectDetails page corresponding to the for the project
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/27/2020 
    * /
    /**/
    const handleSubmit = async (e) => {
        let errorsExist = false;
        //prevent default action
        e.preventDefault();
        //check project name 
        if(newProjectName.length === 0) {
            setProjectNameError("You must include a name for your project");
            errorsExist = true;
        } else if(newProjectName.length > 100) {
            setProjectNameError("Your project name should be no more than 100 characters.");
            errorsExist = true;
        } 
        else {
            setProjectNameError("");
        }

        //check projectDescriptionError
        if(newProjectDescription.length > 500) {
            setProjectDescriptionError("Your project description should be no more than 500 characters.");
            errorsExist = true;
        } 
        else {
            setProjectDescriptionError("");
        }

        if(errorsExist == false) {
            //if no errors were found, create an object to send information in the HTTP Request body
            //this payload adheres to the UtilityProjectModel in the serer
            const payload = {
                ProjectId: projectDetails.projectId,
                Name: newProjectName,
                Description: newProjectDescription,
                TimeCreated: projectDetails.timeCreated
            }
            //making post request to server
            const response = await fetch("/project/edit" , {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(payload)
            })
            //convert response to json
            .then(response => response.json())
            //relocate to the projectDetails page
            .then(data => {
                window.location.pathname = `/projects/${data.projectId}`;
            })
            .catch(error => console.log(error));
            
            
        }

    }
    
    //JSX to render edit project form
    return(
        <div className="page">
            <div className="edit-project">
                <PageDescription 
                    title={`Edit ${(newProjectName ? newProjectName : "")}`} 
                    description="Changing the details for this project" 
                />
                <div className="project-form">
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label htmlFor="project-name">Project Name</label>
                            <input 
                                type="text" 
                                className="form-control" 
                                id="project-name" 
                                placeholder="Project Name" 
                                value={newProjectName || ""} 
                                onChange={(e) => setNewProjectName(e.target.value)}
                            />
                            <small className="error-message">
                                {projectNameError ? projectNameError : ""}
                            </small>
                        </div>
                        <div className="form-group">
                            <label htmlFor="project-description">Project Description (optional)</label>
                            <textarea 
                                type="text" 
                                className="form-control" 
                                id="project-description" 
                                placeholder="Project Description" 
                                value={newProjectDescription || ""} 
                                onChange={(e) => setNewProjectDescription(e.target.value)}
                            />
                            <small className="error-message">
                                {projectDescriptionError ? projectDescriptionError : ""}
                            </small>
                        </div>
                        <button type="submit" className="btn btn-primary create">Update Project</button>
                        <Link to={`/projects/${match.params.projectId}`}>
                            <button className="btn btn-secondary cancel">Cancel</button>
                        </Link>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default EditProject;