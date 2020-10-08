/**/
/* 
 * This file contains the PageDescription component
 */
/**/
import React from "react";

/**/
/*
 * NAME:
 *      PageDescription() - React functional component corresponding to the page descriptions used by some pages in the web application
 * SYNOPSIS:
 *      PageDescription(props) - 
 *          props.title --> The title of the page in the web application
 *          props.description --> The description of the page in the web application
 *      
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page title and description for a page. 
 * RETURNS
 *      JSX that renders the page title and description
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/05/2020 
 * /
 /**/
const PageDescription = (props) => {
    return (
        <div className="page-description">
            <h1>{props.title}</h1>
            <span>{props.description}</span>
        </div>
    );
};

export default PageDescription;