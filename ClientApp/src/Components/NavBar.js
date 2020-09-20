import React from 'react';
import { Link } from 'react-router-dom';
import { AiOutlineBars } from 'react-icons/ai';
import { NavBarItems } from './NavBarItems.js';
import '../CSS/NavBar.css';

const NavBar = () => {
    return (
        <div className="navigation">
            <nav className="nav-bar">
                <ul className="nav-menu-items">
                    {NavBarItems.map((item, index) => {
                        return (
                            <li key={index} className={item.className}>
                                <Link to={item.path}>
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