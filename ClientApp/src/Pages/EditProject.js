import React, { useState, useEffect } from "react";
import {Link} from "react-router-dom";
import PageDescription from "../Components/PageDescription.js";
import "../CSS/CreateNewProject.css"; //use same css as for create new project page because they look very similar


const EditProject = ({match, location}) => {
    const [projectDetails, setProjectDetails] = useState({});
    const [newProjectName, setNewProjectName] = useState("");
    const [projectNameError, setProjectNameError] = useState("");
    const [newProjectDescription, setNewProjectDescription] = useState("");
    const [projectDescriptionError, setProjectDescriptionError] = useState("");

    useEffect(() => {
        fetchProjectData();
    }, []);

    useEffect(() => {
        setNewProjectName(projectDetails.name);
        setNewProjectDescription(projectDetails.description);
    }, [projectDetails]);

    const fetchProjectData = async () => {
        const res = await fetch(`/project/${match.params.projectId}`);
        const data = await res.json();
        setProjectDetails(data);
    };

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
            const payload = {
                ProjectId: location.id,
                Name: newProjectName,
                Description: newProjectDescription,
                TimeCreated: location.timeCreated
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
            .then(response => response.json())
            .then(data => {
                window.location.pathname = `/projects/${data.projectId}`;
            })
            .catch(error => console.log(error));
            
            
        }

    }
    

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