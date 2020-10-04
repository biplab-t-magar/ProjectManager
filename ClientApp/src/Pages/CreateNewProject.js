import React, { useState } from "react";
import PageDescription from "../Components/PageDescription.js";
import "../CSS/CreateNewProject.css";
const CreateNewProject = () => {
    const [projectName, setProjectName] = useState("");
    const [projectNameError, setProjectNameError] = useState("");
    const [projectDescription, setProjectDescription] = useState("");

    const handleSubmit = async (e) => {
        let errorsExist = false;
        //prevent default action
        e.preventDefault();
        //check project name error
        if(projectName.length == 0) {
            setProjectNameError("You must include a name for your project");
            errorsExist = true;
        } else {
            setProjectNameError("");
        }


        if(errorsExist == false) {
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
                window.location.pathname = `/projects/${data.projectId}`;
            })
            .catch(error => console.log(error));
        }

    }
    

    return(
        <div className="page">
            <div className="create-new-project">
                <PageDescription title="Create a new project" description="You will be the manager for this project"></PageDescription>
                <div className="project-form">
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label htmlFor="project-name">Project Name</label>
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
                            <textarea 
                                type="text" 
                                className="form-control" 
                                id="project-description" 
                                placeholder="Project Description" 
                                value={projectDescription} 
                                onChange={(e) => setProjectDescription(e.target.value)}
                            />
                        </div>
                        <button type="submit" className="btn btn-primary">Create Project</button>

                    </form>
                </div>
            </div>
        </div>
    );
}

export default CreateNewProject;