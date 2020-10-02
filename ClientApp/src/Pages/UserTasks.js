import React from 'react';
import PageDescription from "../Components/PageDescription.js";
import ListColumns from "../Components/ListColumns.js";

const Tasks = () => {
    // let numOfEntriesToShow = 20;

    const headerColumns = [
        {
            name: "Project Id",
            cName: "id",
        }, 
        {
            name: "Project Name",
            cName: "name",
        },
        {
            name: "Date Created",
            cName: "date",
        },
        {
            name: "Description",
            cName: "description",
        },
    ];



    return (
        <div className="page">
            <div className="tasks">
                <PageDescription title="Your Tasks" description="This is a list of all of your current tasks"/>
            </div>
            {/* <NumOfEntries /> */}
            <div className="projects-list">
                {/*headers*/}
                <div className="project-list-header project-list-row">
                    {headerColumns.map((item, index) => {
                        return (
                            <div key={index} className={`${item.cName} column`}>{item.name}</div>
                        );
                    })}
                </div>
                {dummyProjects.map((row,index) => {
                    return (
                        
                        <ListColumns key={index} row={row} cName="project-list-row"></ListColumns>
                    );
                })}
            </div>
        </div>
    );
}

export default Tasks;

const dummyProjects = [
    [
        {
            name: "1",
            cName: "id",
        }, 
        {
            name: "test 1",
            cName: "name",
        },
        {
            name: "jan 3",
            cName: "date",
        },
        {
            name: "This is a test project description 1",
            cName: "description",
        },
    ], 
    [
        {
            name: "2",
            cName: "id",
        }, 
        {
            name: "test 2",
            cName: "name",
        },
        {
            name: "jan 28",
            cName: "date",
        },
        {
            name: "The above example creates three equal-widt",
            cName: "description",
        },
    ],
    
];