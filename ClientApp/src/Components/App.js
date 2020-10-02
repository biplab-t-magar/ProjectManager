import React from 'react';
import {BrowserRouter, Route, Switch} from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.css';
import NavBar from './NavBar';
import '../CSS/App.css';
import Home from '../Pages/Home.js';
import Projects from '../Pages/Projects.js';
import Reports from '../Pages/Reports.js';
import UserTasks from '../Pages/UserTasks.js';
import ProjectDetails from "../Pages/ProjectDetails.js";
import ProjectUserActivity from "../Pages/ProjectUserActivity.js";
import ProjectTasks from '../Pages/ProjectTasks';
import Login from "../Pages/Login";
import Register from "../Pages/Register";

//Add notes to tags

const App = () => {
    const renderNavBar = () => {
        if (window.location.pathname !== "/login" && window.location.pathname !== "/register") {
            return (<NavBar/>);
        }
    }


    return(
        <div>
            <BrowserRouter>
                {renderNavBar()}
                <Switch> 
                    <Route path="/" exact component={Home}/>
                    <Route path="/login" exact component={Login}/>
                    <Route path="/register" exact component={Register}/>
                    <Route path="/projects" exact component={Projects}/>
                    <Route path="/reports" exact component={Reports}/>
                    <Route path="/tasks" exact component={UserTasks}/>
                    <Route path="/projects/:projectId" exact component={ProjectDetails} />
                    <Route path="/projects/:projectId/users/:userId" exact component={ProjectUserActivity} />
                    <Route path="/projects/:projectId/tasks" exact component={ProjectTasks} />
                    <Route path="*" component ={() => "This page does not existThis page does not existThis page does not existThis page does not existThis page does not exist"} />
                </Switch>
            </BrowserRouter>
        </div> 
    );
};

export default App;