import React from "react";

const ListColumns = (props) => {
    return(
        // className="list-columns"
        <div > 
            <div className="row">
                {props.columns.map((item, index) => {
                    return (
                        <div key={index} className={props.cName}>{item}</div>
                    );
                    
                })}
                
            </div>
        </div>
    );
}

export default ListColumns;