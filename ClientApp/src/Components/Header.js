/**/
/* 
 * This file contains the Header component, that generates and returns the JSX needed to render the Header in the web application
 */
/**/

import React from 'react';
import {Link} from "react-router-dom";
import "../CSS/Header.css"

/**/
/*
 * NAME:
 *      Header() - React functional component corresponding to the Header in the web application
 * SYNOPSIS:
 *      Header()
 * DESCRIPTION:
 *      This function is a React functional component that generates the header for the project, along with 
 *      logout functionality
 * RETURNS
 *      JSX that renders the header
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/1/2020 
 * /
 /**/
const Header = () => {

    /**/
    /*
    * NAME:
    *      logOut() - Log a user out by communicating to the server
    * SYNOPSIS:
    *      logOut()
    * DESCRIPTION:
    *      This function logs a user of an application out and then redirects to the application login page 
    * RETURNS
    *      
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/1/2020 
    * /
    /**/
    const logOut = async () => {
        const response = await fetch("/account/logout");
        if(response.ok) {
            window.location.pathname = "/login";
        }
    }

    //return header JSX
    return (
        <div>
            <header className="header">
                <Link to="/">
                    <h1>ProjectManager</h1>
                </Link>
                <div onClick={logOut} className="logout">
                    Log Out
                </div>
            </header>
        </div>
    );
}

export default Header;