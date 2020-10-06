const CheckAuthentication = async () => {
    let response = await fetch("/account")
        .catch(error => error);
    //if response status is Ok, redirect to home page 
    if(response.status === 401) {
        window.location.pathname = "/login";
    } 
}

export default CheckAuthentication;