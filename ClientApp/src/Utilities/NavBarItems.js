/**/
/*
 * This file contains all the information needed by the NavBar
 * / 
/**/

import React from 'react';
import { AiFillHome, AiOutlineProject} from "react-icons/ai"; 
import { GoTasklist } from "react-icons/go";

 
export  const NavBarItems = [
    {
        title: 'Home',
        path: '/',
        icon: <AiFillHome />,
        className: 'nav-text'
    },
    {
        title: 'Projects',
        path: '/projects',
        icon: <AiOutlineProject />,
        className: 'nav-text'
    }, 
    {
        title: 'Tasks',
        path: '/tasks',
        icon: <GoTasklist />,
        className: 'nav-text'
    }, 
    
]