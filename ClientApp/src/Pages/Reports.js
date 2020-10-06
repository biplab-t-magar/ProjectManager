import React, { useEffect } from 'react';
import CheckAuthentication from '../Utilities/CheckAuthentication';

const Reports = () => {
    useEffect(() => {
        CheckAuthentication();
    }, []);

    return (
        <div>
            <h1>Report</h1>
        </div>
    );
}

export default Reports;