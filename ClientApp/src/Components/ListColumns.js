import React from "react";

const ListColumns = (props) => {
    return(
        // className="list-columns"
        <div > 
            <div className={props.cName}>
                {props.row.map((item, index) => {
                    return (
                        <div key={index} className={`${item.cName} column`}>{item.name}</div>
                    );
                })}
            </div>
           
        </div>
    );
};

export default ListColumns;