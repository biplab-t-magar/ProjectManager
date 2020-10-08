import React from 'react';
import { AiFillHome, AiOutlineProject} from "react-icons/ai"; 
import { GoTasklist } from "react-icons/go";
import { GiEgyptianProfile } from "react-icons/gi";
import { HiDocumentReport } from "react-icons/hi";

 
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