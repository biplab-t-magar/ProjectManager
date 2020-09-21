import React from 'react';
import '../CSS/Projects.css';
import PageDescription from "../Components/PageDescription";

const Projects = () => {
    let errorMessage = "This is not a valid number of entries to show.";
    let numOfEntriesToShow = 10;
    const ProjectListColums = ["Project Id", "Project Name", "Date Created", "Description"]
    return (
        <div className="page">
            <div className="projects-content">
                <PageDescription title="Your Projects" description="This is a list of all of your projects so far"/>
                <button type="button" className="btn btn-lg create-button create-button">+ Create New Project</button>
                <div className="num-of-entries">
                    Show 
                    <input type="text" value={numOfEntriesToShow} className="invalid-entry" id="num-of-entries-input"/>
                    entries
                    <div className="text-danger invalid-entry">{errorMessage}</div>
                </div>
                <div className="project-list">
                    <div className="row">
                        <div>Project Id</div>
                        <div>Project Name</div>
                        <div>Date Created</div>
                        <div>Description</div>
                    </div>
                    
                </div>
                
            </div>
        </div>
    );
}

export default Projects;