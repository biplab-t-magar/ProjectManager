import React, {useState, useEffect} from 'react';
import '../CSS/Projects.css';
import PageDescription from "../Components/PageDescription";
import ListColumns from '../Components/ListColumns';
import NumOfEntries from '../Components/NumOfEntries';


const Projects = () => {
    const [numOfEntries, setNumOfEntries] = useState(10);
    let items = {};

    //on intial render, get the object of all 
    useEffect(() => {
        fetch('/projects/1')
            .then(res => res.json())
            .then(json => {
                items = json;
            });
    }, []);
    
    console.log(items);
    
    return (
        <div className="page">
            <div className="projects">
                <PageDescription title="Your Projects" description="This is a list of all of your projects so far"/>
                <button type="button" className="btn btn-lg create-button create-button">+ Create New Project</button>
                <NumOfEntries numOfEntries={numOfEntries} onNumOfEntriesChange={setNumOfEntries}/>
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
        </div>
    );
}

export default Projects;

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