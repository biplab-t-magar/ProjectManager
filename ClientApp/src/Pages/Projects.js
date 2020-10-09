/**/
/*
 * This file represents the Projects page in the web application
 * It consists of the Projects functional component that handles the rendering of the 
 * page display and also the communication with the server to fetch the list of all Projects a user
 * is involved in
 * / 
/**/

import React, {useState, useEffect} from 'react';
import '../CSS/Projects.css';
import PageDescription from "../Components/PageDescription";
import {Link} from "react-router-dom";
import ConvertDate from "../Utilities/ConvertDate.js"
import LoadingSpinner from '../Utilities/LoadingSpinner';
import CheckAuthentication from '../Utilities/CheckAuthentication';

/**/
/*
 * NAME:
 *      Projects() - React functional component corresponding to the Projects page
 * SYNOPSIS:
 *      Projects()
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to list all of a user's projects
 *      This components handles the retrieval of all data needed to display the user's projects and related information
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/15/2020 
 * /
 /**/
const Projects = () => {
    // const [numOfEntries, setNumOfEntries] = useState(10);
    const [projectsList, setProjectsList] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);

    //on intial render, fetch project list from server
    useEffect(() => {
        CheckAuthentication();
        fetchAllProjects();
    }, []);
    

    /**/
    /*
    * NAME:
    *      fetchAllProjects() - async function to retrieve the list of projects from the server
    * SYNOPSIS:
    *      fetchAllProjects()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the all the project's of a user
    *      Sets the projectsList state corresponding to project data
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/15/2020 
    * /
    /**/
    const fetchAllProjects = async () => {
        
        const res = await fetch("/user/projects");
        const jsonData = await res.json();
        if(!res.ok) {
            window.pathname.location = "/login";
        } 
        //for hiding loading icon
        setContentLoaded(true);
        setProjectsList(jsonData);
        
    };

    //return the JSX that generates the page. 
    return (
        <div className="page">
            <div className="projects">
                <PageDescription title="Your Projects" description="This is a list of all of your projects so far"/>
                <Link to="/projects/new">
                    <button type="button" className="btn btn-lg create-button create-button">+ Create New Project</button>
                </Link>
                <div className="projects-list">
                    <div className="project-list-header project-list-row">
                        <div className="name column">Project Name</div>
                        <div className="timeCreated column">Created On</div>
                        <div className="description column">Description</div>
                    </div>
                    <div>
                        {contentLoaded ? 
                            projectsList.map((project ) => {
                                return (
                                    <Link to={`/projects/${project.projectId}`} key={project.projectId}>
                                        <div  className="project-entry project-list-row">
                                            <div className="name column">{project.name}</div>
                                            <div className="timeCreated column">{ConvertDate(project.timeCreated)}</div>
                                            <div className="description column">{project.description}</div>
                                        </div>
                                    </Link>
                                );
                            }) : <div className="spinner"><LoadingSpinner /> </div>
                        }
                        
                    </div>
                </div>
                
            </div>
        </div>
    );
}

export default Projects;
