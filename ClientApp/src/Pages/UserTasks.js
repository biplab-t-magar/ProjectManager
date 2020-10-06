// import React, { useEffect, useState } from 'react';
// import PageDescription from "../Components/PageDescription.js";
// import ListColumns from "../Components/ListColumns.js";
// import CheckAuthentication from '../Utilities/CheckAuthentication.js';

// const Tasks = () => {
//     const [userProjects, setUserProjects] = useState([]);
//     const [userTasks, setUserTasks] = useState([]);

//     useEffect(() => {
//         CheckAuthentication();
//     }, [])

//     return (
//         <div className="page">
//             <div className="tasks">
//                 <PageDescription title="Your Tasks" description="This is a list of all of tasks assigned to you"/>
//             </div>
//             <div className="projects-list">
//                 {/*headers*/}
//                 <div className="project-list-header project-list-row">
//                     {headerColumns.map((item, index) => {
//                         return (
//                             <div key={index} className={`${item.cName} column`}>{item.name}</div>
//                         );
//                     })}
//                 </div>
//                 {dummyProjects.map((row,index) => {
//                     return (
                        
//                         <ListColumns key={index} row={row} cName="project-list-row"></ListColumns>
//                     );
//                 })}
//             </div>
//         </div>
//     );
// }

// export default Tasks;

