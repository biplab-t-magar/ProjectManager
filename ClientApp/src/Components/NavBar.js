import React, { useState } from 'react';
import { Link, withRouter } from 'react-router-dom';
import { NavBarItems } from '../Utilities/NavBarItems.js';
import '../CSS/NavBar.css';

const NavBar = () => {
    const [currentLocation, setCurrentLocation] = useState(window.location.pathname);

    const onLocationChange = () =>{
        setCurrentLocation(window.location.pathname);
    }

    return (
        <div className="navigation">
            {/* hide the navbar when in login or register page */}
            {/* <nav className={`nav-bar ${(currentLocation === "/login" || currentLocation === "/register") ? "hidden" : ""}`}> */}
            <nav className="nav-bar">
                <ul className="nav-menu-items">
                    {NavBarItems.map((item, index) => {
                        return (
                            <li key={index} className={item.className} onClick={onLocationChange}>
                                <Link to={item.path} className={currentLocation === item.path ? "active" : ""}>
                                    {item.icon}
                                    <span>{item.title}</span>
                                </Link> 
                            </li>
                        )
                    })}
                </ul>
            </nav>
        </div>
        
    );
};

export default withRouter(NavBar);