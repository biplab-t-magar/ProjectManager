import React from "react";

const PageDescription = (props) => {
    return (
        <div className="page-description">
            <h1>{props.title}</h1>
            <span>{props.description}</span>
        </div>
    );
};

export default PageDescription;