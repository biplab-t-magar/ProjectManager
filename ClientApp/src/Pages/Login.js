/**/
/*
 * This file represents the Login page in the web application
 * It consists of the Login functional component that handles the rendering of the 
 * page display, basic client-side verification, and also the communication with the server to log a user in 
 * / 
/**/
import React, {useState, useEffect} from 'react';
import "../CSS/Login.css";
import {Link, Redirect, useHistory} from "react-router-dom";

/**/
/*
 * NAME:
 *      Login() - React functional component corresponding to the Login page
 * SYNOPSIS:
 *      Login()
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to log in
 *      This components handles the sending of data to the server, the generation of the login form, and the retreival of data from the
 *      server used to sign the user in (using browser cookies) or to display error messages if login failed
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/29/2020 
 * /
 /**/
const Login = () => {
    //useState hooks
    const [userName, setUserName] = useState("");
    const [userPassword,  setUserPassword] = useState("");
    const [userNameError, setUserNameError] = useState("");
    const [passwordError, setPasswordError] = useState("");
    const [userAuthenticated, setUserAuthenticated] = useState(false);

    //on first render, check if user has already been authenticated
    useEffect(() => {
        checkAuthentication();
    }, []);

    //if the user has already been authenticated, redirect to home page
    if(userAuthenticated) {
        window.location.pathname = "/";
    }

    /**/
    /*
    * NAME:
    *      checkAuthentication() - async function to communicate with the server and check if the user has already been signed in
    *                              based on the Cookie stored in the web browser
    * SYNOPSIS:
    *      checkAuthentication()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response on whether the user is already signed in. Sets the userAuthenticated set
    *       to true or false based on the server response
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/29/2020 
    * /
    /**/
    const checkAuthentication = async () => {
        let response = await fetch("/account")
            .catch(error => error);
        //if response status is Ok, redirect to home page 
        if(response.ok) {
            setUserAuthenticated(true);
        } else {
            setUserAuthenticated(false);
        }
    }
    /**/
    /*
    * NAME:
    *      handleSubmit() - handles the submission of the Login form
    * SYNOPSIS:
    *      handleSubmit(e)
    *           e --> the JavaScript event generated when submitting the form
    * DESCRIPTION:
    *      This function executes the action to be taken once the user has filled out the form and hit submit.
    *      First it validates the user input in the forms, and sets the error message if user input is not valid.
    *      If user input is valid, it sends a request to the server to log the user with the given information in.
    *      Finally, it redirects to the Home page of the web application
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      09/29/2020 
    * /
    /**/
    const handleSubmit = async (e) => {
        //stop default form submit behavior
        e.preventDefault();
        let errorsExist = false;

        //check userName error
        if(userName.length == 0) {
            setUserNameError("You must include your username to login");
            errorsExist = true;
        } else {
            setUserNameError("");
        }

        //check password erro
        if (userPassword.length == 0){
            setPasswordError("You must include a valid password to login");
            errorsExist = true;
        } else {
            setPasswordError("");
        }
        
        if(errorsExist == false) {
            //payload adheres to the RegisterUserModel in the server
            const payload = {
                userName: userName,
                password: userPassword
            }
            //making post request to server
            const response = await fetch("/account/login" , {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(payload)
            })

            const data= await response.json();
            if(!response.ok) {
                setPasswordError(data);
            } else {
                setUserAuthenticated(true);
            }
        }
    }
    //return the JSX that generates the page. 
    return (
        <div>
            {/* Redirecting to another home page if user has been authenticated */}
            {/* {userAuthenticated ? <Redirect push to="/" /> : ""} */}
            <div className="login">
                <header className="welcome">
                    <span>Welcome to the Project Manager Web Application</span>
                </header>                      
            </div>
            <div className="login-form"> 
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="user-name">User Name</label>
                        <input 
                            type="text" 
                            className="form-control" 
                            id="user-name" 
                            placeholder="User Name" 
                            value={userName} 
                            onChange={(e) => setUserName(e.target.value)}
                        />
                        <small className="error-message">
                            {userNameError ? userNameError : ""}
                        </small>
                    </div>
                    
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input 
                            type="password" 
                            className="form-control" 
                            id="password" 
                            placeholder="Password" 
                            value={userPassword}
                            onChange={e => setUserPassword(e.target.value)}
                        />
                         <small className="error-message">
                            {passwordError ? passwordError : ""}
                        </small>
                    </div>
                    <button type="submit" className="btn btn-primary">Submit</button>
                    <div className="no-account">
                        Don't have an account yet? 
                        <Link to="/register">Register here</Link>
                    </div>
                </form>

            </div>
        </div>
    );
}

export default Login;