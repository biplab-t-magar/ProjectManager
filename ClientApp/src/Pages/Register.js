import React, {useReducer, useState} from 'react';
import "../CSS/Register.css";
import {Link} from "react-router-dom";
import { data } from 'jquery';

const Login = () => {
    const [userFirstName, setUserFirstName] = useState("");
    const [userLastName, setUserLastName] = useState("");
    const [userName, setUserName] = useState("");
    const [userPassword,  setUserPassword] = useState("");
    const [confirmPassoword, setConfirmPassword] = useState("");
    const [userNameError, setUserNameError] = useState("");
    const [passwordError, setPasswordError] = useState("");
    const [fNameError, setFNameError] = useState("");
    const [lNameError, setLNameError] = useState("");

    
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
            //data to send to server
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
            })
            .then(response => console.log(response))
            .catch(error => console.log(error));
            // if(!res.ok) {
            //     console.log(res.statusText);
            // }
            // const data = await res.json();
            // console.log(data);
        }
        
    }

    return (
        <div>
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

export default Login;