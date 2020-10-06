import React, { useEffect } from "react";
import CheckAuthentication from "../Utilities/CheckAuthentication";

const Alerts = () => {

    useEffect(() => {
        CheckAuthentication();
    }, []);

    return(
        <div className="page">
            <div className="alerts">
                Alert
            </div>
        </div>
    );
}

export default Alerts;