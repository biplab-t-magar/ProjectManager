/**/
/*
 * This file represents the NotFound page in the web application
 * It consists of the NotFound functional component that handles the rendering of the page to display
 * when the user tries to access an invalid route
 * / 
/**/

import React from "react";

/**/
/*
 * NAME:
 *      NotFoundPage() - React functional component corresponding to the NotFoundPage page
 * SYNOPSIS:
 *      NotFoundPage()
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the Not Found page for all the possible routes
 *      specified by the user that are not valid
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/17/2020 
 * /
 /**/
const NotFoundPage = () => {
    //return the JSX that generates the page. 
    return(
        <div className="page">
            <div className="not-found-page">
                <h1>Uh Oh! This is not a valid page</h1>
            </div>
        </div>
    )
}

export default NotFoundPage;