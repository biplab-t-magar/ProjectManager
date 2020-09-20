import React from 'react';
import { AiFillHome, AiOutlineFundProjectionScreen, FaTasks } from "react-icons/ai"; 
import { GoTasklist } from "react-icons/go";
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
        icon: <AiOutlineFundProjectionScreen />,
        className: 'nav-text'
    }, 
    {
        title: 'Tasks',
        path: '/tasks',
        icon: <GoTasklist />,
        className: 'nav-text'
    }, 
    {
        title: 'Reports',
        path: '/reports',
        icon: <HiDocumentReport />,
        className: 'nav-text',
    }
    
]