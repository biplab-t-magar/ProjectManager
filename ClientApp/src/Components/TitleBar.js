import React, { useState } from 'react';
import { Link, withRouter } from 'react-router-dom';
import { NavBarItems } from '../Utilities/NavBarItems.js';
import '../CSS/NavBar.css';

const NavBar = () => {
    const [currentLocation, setCurrentLocation] = useState(window.location.pathname);


    return (
        <div className="title-bar">
            {/* hide the navbar when in login or register page */}
            {/* <nav className={`nav-bar ${(currentLocation === "/login" || currentLocation === "/register") ? "hidden" : ""}`}> */}
            <nav className="nav-bar">
                <ul className="nav-menu-items">
                    
                </ul>
            </nav>
        </div>
        
    );
};

export default withRouter(NavBar);