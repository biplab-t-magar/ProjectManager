/**/
/*
 * This file is the main component that is mounted onto the HTML web application. It is this file that links together
 * all the other components in the web application. It sets up routes and delineate the traversal of the web application.
 * / 
/**/

import React from 'react';
import {BrowserRouter, Route, Switch, withRouter} from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import NavBar from './NavBar';
import '../CSS/App.css';
import Projects from '../Pages/Projects.js';
import UserTasks from '../Pages/UserTasks.js';
import ProjectDetails from "../Pages/ProjectDetails.js";
import ProjectTasks from '../Pages/ProjectTasks';
import Login from "../Pages/Login";
import Register from "../Pages/Register";
import CreateNewProject from '../Pages/CreateNewProject';
import EditProject from '../Pages/EditProject';
import DeleteProject from '../Pages/DeleteProject';
import Header from './Header';
import ManageProjectUsers from '../Pages/ManageProjectUsers';
import ManageTaskTypes from '../Pages/ManageTaskType';
import CreateTask from "../Pages/CreateTask";
import TaskDetails from '../Pages/TaskDetails';
import EditTask from "../Pages/EditTask.js";
import TaskComments from '../Pages/TaskComments';
import NotFoundPage from '../Pages/NotFountPage';
import UserProfile from "../Pages/UserProfile";
import UserActivityInProject from '../Pages/UserActivityInProject';
import ProjectActivities from '../Pages/ProjectActivities';


/**/
/*
 * NAME:
 *      App() - primary functional component in the web application, linking together all other components
 * SYNOPSIS:
 *      App()
 * DESCRIPTION:
 *      This function is a React functional component that makes use of the React router in order to 
 *      set the routing for all the other components in the WebApplication. It holds all of the components, but
 *      only renders them when the route specified matches the components.
 * RETURNS
 *      JSX to render component that corresponds to the route the application user is currently in
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/6/2020 
 * /
 /**/

const App = () => {

    /**/
    /*
    * NAME:
    *      renderNavBars() - conditionally returns the JSX for the NavBar and Header
    * SYNOPSIS:
    *      renderNavBars()
    * DESCRIPTION:
    *      This function checks the current browser location and only returns NavBar and Header JSX if the user is not in the 
    *      login or registration page
    * RETURNS
    *      The Header and NavBar components
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/8/2020 
    * /
    /**/
    const renderNavBars = () => {
        //only render the navbar and header if the user is logged in
        if (window.location.pathname !== "/login" && window.location.pathname !== "/register") {
            return (
            <div>
                <Header/>
                <NavBar/>
            </div>
            );
        }
    }

    //return the component corresponding to the current route
    return(
        <div>
            <BrowserRouter>
                {renderNavBars()}
                <Switch> 
                    <Route path="/" exact component={UserProfile}/>
                    <Route path="/login" exact component={Login}/>
                    <Route path="/register" exact component={Register}/>
                    <Route path="/projects" exact component={Projects}/>
                    <Route path="/tasks" exact component={UserTasks}/>
                    <Route path="/profile/:userId" exact component={UserProfile} />
                    <Route path="/projects/new" exact component={CreateNewProject} />
                    <Route path="/projects/:projectId/activities" exact component={ProjectActivities} />
                    <Route path="/projects/:projectId/users/:userId" exact component={UserActivityInProject} />
                    <Route path="/projects/:projectId/task/:taskId" exact component={TaskDetails} />
                    <Route path="/projects/:projectId/task/:taskId/comments" exact component={TaskComments} />
                    <Route path="/projects/:projectId/task/:taskId/edit" exact component={EditTask} />
                    <Route path="/projects/:projectId" exact component={ProjectDetails} />
                    <Route path="/projects/:projectId/task-types" exact component={ManageTaskTypes} />
                    <Route path="/projects/:projectId/tasks" exact component={ProjectTasks} />
                    <Route path="/projects/:projectId/tasks/new" exact component={CreateTask} />   
                    <Route path="/projects/:projectId/edit" exact component={EditProject} />
                    <Route path="/projects/:projectId/delete" exact component={DeleteProject} />
                    <Route path="/projects/:projectId/users" exact component={ManageProjectUsers} />
                    <Route path="*" component ={NotFoundPage} />
                </Switch>
            </BrowserRouter>
        </div> 
    );
};

export default App;