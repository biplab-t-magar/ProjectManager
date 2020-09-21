import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { NavBarItems } from './NavBarItems.js';
import '../CSS/NavBar.css';

const NavBar = () => {
    const [currentLocation, setCurrentLocation] = useState(window.location.pathname);

    const onLocationChange = () =>{
        setCurrentLocation(window.location.pathname);
    }

    return (
        <div className="navigation">
            <nav className="nav-bar">
                <ul className="nav-menu-items">
                    {NavBarItems.map((item, index) => {
                        return (
                            <li key={index} className={item.className} onClick={onLocationChange}>
                                <Link to={item.path} className={currentLocation === item.path ? "active" : ""}>
                                    {item.icon}
                                    <span>{item.title}</span>
                                </Link> 
                            </li>
                        )
                    })}
                </ul>
            </nav>
        </div>
        
    );
};

export default NavBar;