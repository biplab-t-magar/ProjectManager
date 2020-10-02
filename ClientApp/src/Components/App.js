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

//Add notes to tags

const App = () => {
    return(
        <div>
            <BrowserRouter>
            <NavBar/>
                <Switch>    
                    <Route path="/" exact component={Home}/>
                    <Route path="/projects" exact component={Projects}/>
                    <Route path="/reports" exact component={Reports}/>
                    <Route path="/tasks" exact component={UserTasks}/>
                    <Route path="/projects/:projectId" exact component={ProjectDetails} />
                    <Route path="/projects/:projectId/users/:userId" exact component={ProjectUserActivity} />
                    <Route path="/projects/:projectId/tasks" exact component={ProjectTasks} />
                    <Route path="*" component ={() => "404 nodsafasd flajsfld jasldfj lasjdflj sdft found"} />
                </Switch>
            </BrowserRouter>
        </div> 
    );
};

export default App;