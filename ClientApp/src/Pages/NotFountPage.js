/**/
/*
 * This file represents the NotFound page in the web application
 * It consists of the NotFound functional component that handles the rendering of the page to display
 * when the user tries to access an invalid route
 * / 
/**/

import React from "react";

const NotFoundPage = () => {
    return(
        <div className="page">
            <div className="not-found-page">
                <h1>Uh Oh! This is not a valid page</h1>
            </div>
        </div>
    )
}

export default NotFoundPage;