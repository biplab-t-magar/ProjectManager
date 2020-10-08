/**/
/*
 * This file represents the CreateNewProject page in the web application
 * It consists of the CreateNewProject functional component that handles the rendering of the 
 * page display and also the communication with the server to create a new project
 * / 
/**/


import React, { useState, useEffect } from "react";
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription.js";
import "../CSS/CreateNewProject.css";
import CheckAuthentication from "../Utilities/CheckAuthentication.js";

/**/
/*
 * NAME:
 *      CreateNewProject() - React functional component corresponding to the CreateNewProject page
 * SYNOPSIS:
 *      CreateNewProject()
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to create a new project.
 *      This components handles the retrieval of data, generation of forms, and the sending of data to the 
 *      server, thus handling the process of project creation
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/20/2020 
 * /
 /**/
const CreateNewProject = () => {
    //react state hooks 
    //each time a state changes, any component of the page that has changed is re-rendered
    const [projectName, setProjectName] = useState("");
    const [projectNameError, setProjectNameError] = useState("");
    const [projectDescription, setProjectDescription] = useState("");
    const [projectDescriptionError, setProjectDescriptionError] = useState("");

    //the useEffect hook
    //This function is called only the first time this page is rendered
    useEffect(() => {
        CheckAuthentication();
    }, []);


    /**/
    /*
    * NAME:
    *      handleSubmit() - handles the submission of the create project form
    * SYNOPSIS:
    *      handleSubmit(e)
    *           e --> the JavaScript event generated when submitting the form
    * DESCRIPTION:
    *      This function executes the action to be taken once the user has filled out the form and hit submits.
    *      First it validates the user input in the forms, and sets the error message if user input is not valid.
    *      If user input is valid, it sends a request to the server to create a project for the user with the given information.
    *      Finally, it redirects to the ProjectDetails page corresponding to the newly created project
    * RETURNS

    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/20/2020 
    * /
    /**/
    const handleSubmit = async (e) => {
        let errorsExist = false;
        //prevent default action
        e.preventDefault();
        //check project name error
        if(projectName.length === 0) {
            setProjectNameError("You must include a name for your project");
            errorsExist = true;
        } else if(projectName.length > 100) {
            setProjectNameError("Your project name should be no more than 100 characters.");
            errorsExist = true;
        } 
        else {
            setProjectNameError("");
        }

        //check projectDescriptionError
        if(projectDescription.length > 500) {
            setProjectDescriptionError("Your project description should be no more than 500 characters.");
            errorsExist = true;
        } 
        else {
            setProjectDescriptionError("");
        }

        if(errorsExist === false) {
            const payload = {
                name: projectName,
                description: projectDescription,
            }
            //making post request to server
            const response = await fetch("/project/new" , {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(payload)
            })
            .then(response => response.json())
            .then(data => {
                //redirect to the project's page
                window.location.pathname = `/projects/${data.projectId}`;
            })
            .catch(error => console.log(error));
        }

    }
    
    //return the JSX that generates the form. 
    return(
        <div className="page">
            <div className="create-new-project">
                <PageDescription title="Create a new project" description="You will be the manager for this project"></PageDescription>
                <div className="project-form">
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label htmlFor="project-name">Project Name</label>
                            {/* Using React state in the "value" attribute to make this a controlled component */}
                            <input 
                                type="text" 
                                className="form-control" 
                                id="project-name" 
                                placeholder="Project Name" 
                                value={projectName} 
                                onChange={(e) => setProjectName(e.target.value)}
                            />
                            <small className="error-message">
                                {projectNameError ? projectNameError : ""}
                            </small>
                        </div>
                        <div className="form-group">
                            <label htmlFor="project-description">Project Description (optional)</label>
                            {/* Using React state in the "value" attribute to make this a controlled component */}
                            <textarea 
                                type="text" 
                                className="form-control" 
                                id="project-description" 
                                placeholder="Project Description" 
                                value={projectDescription} 
                                onChange={(e) => setProjectDescription(e.target.value)}
                            />
                            {/* only display error message is the state has been set to an error message */}
                            <small className="error-message">
                                {projectDescriptionError ? projectDescriptionError : ""}
                            </small>
                        </div>
                        <button type="submit" className="btn btn-primary create">Create Project</button>
                        <Link to={`/projects`}>
                            <button className="btn btn-secondary cancel">Cancel</button>
                        </Link>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default CreateNewProject;