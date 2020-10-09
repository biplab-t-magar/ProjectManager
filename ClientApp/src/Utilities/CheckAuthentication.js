/**/
/*
 * This file contains the utility function to check if a user is signed in to the application
 * / 
/**/

/**/
/*
 * NAME:
 *      CheckAuthentication() - Asynchronous function to check if a user is authenticated and redirect to login page if not
 * SYNOPSIS:
 *      CheckAuthentication()
 * DESCRIPTION:
 *      This function makes a request to the server, which sends a HTTP response with 401 status code if the user has not been
 *       signed in yet. If it receives the 401 status code, it changes the window location to the login page
 * RETURNS
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/20/2020 
 * /
 /**/
const CheckAuthentication = async () => {
    let response = await fetch("/account")
        .catch(error => error);
    //if response status is Ok, redirect to home page 
    if(response.status === 401) {
        window.location.pathname = "/login";
    } 
}

export default CheckAuthentication;