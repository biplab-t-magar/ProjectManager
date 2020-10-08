/**/
/* 
 * This file contains the NavBar component, that generates and returns the JSX needed to render the navigation bar in the web application
 */
/**/


import React, { useState } from 'react';
import { Link, withRouter } from 'react-router-dom';
import { NavBarItems } from '../Utilities/NavBarItems.js';
import '../CSS/NavBar.css';

/**/
/*
 * NAME:
 *      NavBar() - React functional component corresponding to the navigation bar in the web application
 * SYNOPSIS:
 *      NavBar()
 * DESCRIPTION:
 *      This function is a React functional component that generates the navigation for the project. The JSX 
 *      returned by this component makes sure that the highlighted section of the navbar corresponds to the route the user is currently in
 * RETURNS
 *      JSX that renders the navbar
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/13/2020 
 * /
 /**/
const NavBar = () => {
    //The React state hooks to re-render the component when the user clicks on a section of the navbar
    const [currentLocation, setCurrentLocation] = useState(window.location.pathname);

    /**/
    /*
    * NAME:
    *      onLocationChange() - A click handler function for the NavBar component
    * SYNOPSIS:
    *      onLocationChange()
    * DESCRIPTION:
    *      When a section on the navbar is clicked, this click handler function changes the currentLocation state
    *       to the current window location
    * RETURNS
    *      
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/13/2020 
    * /
    /**/
    const onLocationChange = () =>{
        setCurrentLocation(window.location.pathname);
    }

    //return JSX corresponding to the navigation bar
    return (
        <div className="navigation">
            {/* hide the navbar when in login or register page */}
            {/* <nav className={`nav-bar ${(currentLocation === "/login" || currentLocation === "/register") ? "hidden" : ""}`}> */}
            <nav className="nav-bar">
                <ul className="nav-menu-items">
                    {NavBarItems.map((item, index) => {
                        return (
                            <li key={index} className={item.className} onClick={onLocationChange}>
                                {/* only highlight the active section in the navigation bar */}
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