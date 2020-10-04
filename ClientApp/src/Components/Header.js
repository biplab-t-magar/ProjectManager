import React from 'react';
import {Link} from "react-router-dom";
import "../CSS/Header.css"

const Header = () => {
    const logOut = async () => {
        const response = await fetch("/account/logout");
        if(response.ok) {
            window.location.pathname = "/login";
        }
    }

    return (
        <div>
            <header>
                <Link to="/">
                    <h1>ProjectManager</h1>
                </Link>
                <div onClick={logOut} className="logout">
                    Log Out
                </div>
            </header>
        </div>
    );
}

export default Header;