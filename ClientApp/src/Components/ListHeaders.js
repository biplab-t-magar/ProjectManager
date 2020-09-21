import React from "react";
import ListColumns from "./ListColumns";

const ListHeaders = (props) => {
    return(
        <div className="">
            <ListColumns columns={props.headers} cName="header-item list-item" />
        </div>
        
    );
}

export default ListHeaders;