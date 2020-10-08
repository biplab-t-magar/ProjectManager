/**/
/*
 * This file contains the utility function to check if a user is signed in to the application
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