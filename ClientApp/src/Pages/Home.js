import React, {useEffect, useState} from 'react';
import CheckAuthentication from '../Utilities/CheckAuthentication';

const Home = () => {
    useEffect(() => {
        CheckAuthentication();
    }, []);

    return (
        <div className="page">
 
        </div>
    );
}

export default Home;