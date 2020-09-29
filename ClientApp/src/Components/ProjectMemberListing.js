import React, { useEffect } from "react";
import {Link} from "react-router-dom"

const ProjectMemberListing = (props) => {

    return (
        <div className="project-member-listing">
            <Link to={`/user/${props.user.userId}`}>
                <div>
                    <div className="name">{props.user.firstName} {props.user.middleName} {props.user.lastName} </div>
                </div>
            </Link>
            <div>
                {}
            </div>
        </div>
        

    );
};

export default ProjectMemberListing;