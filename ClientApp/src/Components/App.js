import React from 'react';
import {BrowserRouter, Route, Link, Switch} from 'react-router-dom';
// import 'bootstrap/dist/css/bootstrap.css';
import NavBar from './NavBar';
import '../CSS/App.css';

const App = () => {
    return(
        <div>
            <BrowserRouter>
                <NavBar/>
                <Switch>
                    <Route path="/" />
                </Switch>
            </BrowserRouter>
        </div> 
    );
};

export default App;