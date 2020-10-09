/**/
/*
 * This file contains the component to display a loading animation
 * / 
/**/
import React from "react";
import {Spinner} from "react-bootstrap"

/**/
/*
 * NAME:
 *      LoadingSpinner() - React functional component that renders a Loading Spinner animation
 * SYNOPSIS:
 *      LoadingSpinner()
 * DESCRIPTION:
 *      A React functional component that renders a Loading Spinner animation from the react-bootstrap library
 * RETURNS
 *      JSX that renders the loading spinner 
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/01/2020 
 * /
 /**/ 
const LoadingSpinner = () => {
    return (
        <Spinner animation="border" role="status">
            <span className="sr-only">Loading...</span>
        </Spinner>
        
    );
}

export default LoadingSpinner;

