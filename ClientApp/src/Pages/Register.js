/**/
/*
 * This file represents the Register page in the web application
 * It consists of the Register functional component that handles the rendering of the 
 * page display, basic client-side verification, and also the communication with the server to register a user 
 * / 
/**/

import React, {useEffect, useState} from 'react';
import "../CSS/Register.css";
import {Link, Redirect, withRouter} from "react-router-dom";

/**/
/*
 * NAME:
 *      Register() - React functional component corresponding to the Register page
 * SYNOPSIS:
 *      Register()
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page where a user registers
 *      This components handles the sending of data to the server, the generation of the register form, and the retreival of data from the
 *      server used to sign the user in (using browser cookies) or to display error messages if registration failed
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      09/29/2020 
 * /
 /**/
const Register = () => {
    //useState hooks
    const [userFirstName, setUserFirstName] = useState("");
    const [userLastName, setUserLastName] = useState("");
    const [userName, setUserName] = useState("");
    const [userPassword,  setUserPassword] = useState("");
    const [confirmPassoword, setConfirmPassword] = useState("");
    const [userNameError, setUserNameError] = useState("");
    const [passwordError, setPasswordError] = useState("");
    const [fNameError, setFNameError] = useState("");
    const [lNameError, setLNameError] = useState("");
    const [userAuthenticated, setUserAuthenticated] = useState(false);

    //on first render, check if user has already been authenticated
    useEffect(() => {
        checkAuthentication();
    }, []);

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
        let response = await fetch("/account");
        //if response status is Ok, redirect to home page 
        if(response.status === 200 ) {
            setUserAuthenticated(true);
        } else {
            setUserAuthenticated(false);
        }
    }
    
    /**/
    /*
    * NAME:
    *      handleSubmit() - handles the submission of the registration form
    * SYNOPSIS:
    *      handleSubmit(e)
    *           e --> the JavaScript event generated when submitting the form
    * DESCRIPTION:
    *      This function executes the action to be taken once the user has filled out the form and hit submit.
    *      First it validates the user input in the forms, and sets the error message if user input is not valid.
    *      If user input is valid, it sends a request to the server to register the user with the given information.
    *      Finally, it redirects to the Home page of the web application if the registration is successful. 
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
        //checking for all errors in form entries

        //check userName error
        if(userName.length == 0) {
            setUserNameError("You must include your username to login");
            errorsExist = true;
        } else {
            setUserNameError("");
        }

        //checking firstname error
        if (userFirstName.length == 0){
            setFNameError("You must include a First Name");
            errorsExist = true;
        } else {
            setFNameError("");
        }
        //checking last name error
        if (userLastName.length == 0){
            setLNameError("You must include a Last Name");
            errorsExist = true;
        } else {
            setLNameError("");
        }
        //checking password error
        if (userPassword.length == 0){
            setPasswordError("You must include a valid password to register");
            errorsExist = true;
        } else if (userPassword !== confirmPassoword) {
            setPasswordError("Your confirmation password does not match");
            errorsExist = true;
        }
        else {
            setPasswordError("");
        }
        
        if(errorsExist == false) {
            //payload adheres to the RegisterUserModel in the server
            const payload = {
                firstName: userFirstName,
                lastName: userLastName,
                userName : userName,
                password: userPassword
            }

            //making post request to server
            await fetch("/account/register" , {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(payload)
            })
            .then((response) => {
                if(!response.ok) {
                    console.log(response.statusText)
                }
                //redirect to the home page
                else {
                    setUserAuthenticated(true);
                }
            })
            .then(response => console.log(response))
            .catch(error => console.log(error));

        }
    }

    //return the JSX that generates the page. 
    return (
        <div>
            {/* Redirecting to another home page if user has been authenticated */}
            {/* {userAuthenticated ? <Redirect to="/" /> : ""} */}
            <div className="register">
                <header className="welcome">
                    <span>Create Your Project Manager Account</span>
                </header>                     
            </div>
            <div className="register-form"> 
                <form onSubmit={handleSubmit}>
                    {/* first name input*/}
                    <div className="form-group">
                        <label htmlFor="first-name">First Name</label>
                        <input 
                            type="text" 
                            className="form-control" 
                            id="first-name" 
                            placeholder="First Name" value={userFirstName} 
                            onChange={(e) => setUserFirstName(e.target.value)}
                        />
                        <small className="error-message">
                            {fNameError ? fNameError : ""}
                        </small>
                    </div>
                    {/* last name input */}
                    <div className="form-group">
                        <label htmlFor="last-name">Last Name</label>
                        <input 
                            type="text" 
                            className="form-control" 
                            id="last-name" 
                            placeholder="Last Name" value={userLastName} 
                            onChange={(e) => setUserLastName(e.target.value)}
                        />
                        <small className="error-message">
                            {lNameError ? lNameError : ""}
                        </small>
                    </div>
                    {/* user name input */}
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
                    {/* password input */}
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
                    </div>
                    {/* confirm password */}
                    <div className="form-group">
                        <label htmlFor="confirm-password">Password</label>
                        <input 
                            type="password" 
                            className="form-control" 
                            id="confirm-password" 
                            placeholder="Password" 
                            value={confirmPassoword}
                            onChange={e => setConfirmPassword(e.target.value)}
                        />
                         <small className="error-message">
                            {passwordError ? passwordError : ""}
                        </small>
                    </div>
                    <button type="submit" className="btn btn-primary">Submit</button>
                    <div className="no-account">
                        Already have an account?
                        <Link to="/login">Login here</Link>
                    </div>
                </form>

            </div>
        </div>
    );
}

export default Register;