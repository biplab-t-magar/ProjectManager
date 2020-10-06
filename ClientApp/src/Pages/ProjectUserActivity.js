import React, { useEffect } from "react";
import CheckAuthentication from "../Utilities/CheckAuthentication";

const ProjectUserActivity = () => {
    useEffect(() => {
        CheckAuthentication();
    }, []);

    return(
        <div>
            ProjectUserActivity
        </div>
    );
};

export default ProjectUserActivity;