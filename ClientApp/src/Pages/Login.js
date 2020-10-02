import React, {useReducer, useState} from 'react';
import "../CSS/Login.css";
import {Link} from "react-router-dom";

const Login = () => {
    const [userName, setUserName] = useState("");
    const [userPassword,  setUserPassword] = useState("");
    const [userNameError, setUserNameError] = useState("");
    const [passwordError, setPasswordError] = useState("");

    
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
            const payload = {
                userName: userName,
                password: userPassword
            }
            //making post request to server
            await fetch("/account/login" , {
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
            .catch(error => console.log(error));
        }
        
    }

    return (
        <div>
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
                            id="user-name" aria-describedby="emailHelp" 
                            placeholder="User Name" value={userName} 
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