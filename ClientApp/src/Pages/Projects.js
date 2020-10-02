import React, {useState, useEffect} from 'react';
import '../CSS/Projects.css';
import PageDescription from "../Components/PageDescription";
import {Link} from "react-router-dom";

const Projects = () => {
    // const [numOfEntries, setNumOfEntries] = useState(10);
    const [projectsList, setProjectsList] = useState([]);

    //on intial render, fetch project list from server
    useEffect(() => {
        fetchAllProjects();
    }, []);
    
    const fetchAllProjects = async () => {
        const res = await fetch('/user/1/projects');
        const jsonData = await res.json();
        setProjectsList(jsonData);
    };


    return (
        <div className="page">
            <div className="projects">
                <PageDescription title="Your Projects" description="This is a list of all of your projects so far"/>
                <button type="button" className="btn btn-lg create-button create-button">+ Create New Project</button>
                <div className="projects-list">
                    {/*headers*/}
                    <div className="project-list-header project-list-row">
                        <div className="name column">Project Name</div>
                        <div className="timeCreated column">Created On</div>
                        <div className="deadline column">Deadline</div>
                        <div className="description column">Description</div>
                    </div>
                    {projectsList.map((project ) => {
                        return (
                            <Link to={`/projects/${project.projectId}`} key={project.projectId}>
                                <div  className="project-entry project-list-row">
                                    <div className="name column">{project.name}</div>
                                    <div className="timeCreated column">{project.timeCreated}</div>
                                    <div className="deadline column">{project.deadline}</div>
                                    <div className="description column">{project.description}</div>
                                </div>
                            </Link>
                        );
                    })}
                </div>
                
            </div>
        </div>
    );
}

export default Projects;
