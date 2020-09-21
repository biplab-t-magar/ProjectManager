import React from "react";

const ListHeader = (props) => {
    return(
        <div className="list-header">
            <div className="row">
                {props.headers.map((header, index) => {
                    return (
                        <div>
                            <div key={index} className="">{header}</div>
                        </div>
                    );
                    
                })}
                
            </div>
        </div>
    );
}