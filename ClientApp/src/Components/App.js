import React from 'react';
import {BrowserRouter, Route} from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.css';
import NavBar from './NavBar';
import '../CSS/App.css';
import Home from '../Pages/Home.js';
import Projects from '../Pages/Projects.js';
import Reports from '../Pages/Reports.js';
import Tasks from '../Pages/Tasks.js';
import ProjectDetails from "../Pages/ProjectDetails.js";
import ProjectUserActivity from "../Pages/ProjectUserActivity.js";

//Add notes to tags

const App = () => {
    return(
        <div>
            <BrowserRouter>
                <div>
                    {/* <Header /> */}
                    <NavBar/>
                    <Route path="/" exact component={Home}/>
                    <Route path="/projects" exact component={Projects}/>
                    <Route path="/reports" exact component={Reports}/>
                    <Route path="/tasks" exact component={Tasks}/>
                    <Route path="/projects/:projectId" exact component={ProjectDetails} />
                    <Route path="/projects/:projectId/users/:userId" exact component={ProjectUserActivity} />
                </div>
            </BrowserRouter>
        </div> 
    );
};

export default App;