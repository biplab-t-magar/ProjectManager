import React, {useEffect, useState} from 'react';
import {Redirect} from "react-router-dom"

const Home = () => {
    const [userAuthenticated, setUserAuthenticated] = useState(false);

    //on first render, check if user has already been authenticated
    useEffect(() => {
        checkAuthentication();
        console.log("iiiiiiasldfjlaksjdf");
    }, []);


    const checkAuthentication = async () => {
        let response = await fetch("/account");
        //if response status is Ok, redirect to home page 
        if(response.status === 200 ) {
            console.log("biisdlfjs")
            setUserAuthenticated(true);
        } else {
            console.log("not authetnicated");
            setUserAuthenticated(false);
        }
    }

    return (
        <div>
            {userAuthenticated ? "" : <Redirect to="/login" />}
            <h1>Home</h1>
        </div>
    );
}

export default Home;