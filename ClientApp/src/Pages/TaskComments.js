/**/
/*
 * This file represents the TaskComments page in the web application
 * It consists of the TaskComments functional component that handles the rendering of the 
 * page display and also the communication with the server to receive information on all the comments made on a task
 * / 
/**/

import React, { useEffect, useState } from "react"
import PageDescription from "../Components/PageDescription";
import CheckAuthentication from "../Utilities/CheckAuthentication";
import ConvertDate from "../Utilities/ConvertDate.js";
import ConvertTime from "../Utilities/ConvertTime.js";
import LoadingSpinner from "../Utilities/LoadingSpinner";
import "../CSS/TaskComments.css";
import { Link } from "react-router-dom";

/**/
/*
 * NAME:
 *      TaskComments() - React functional component corresponding to the TaskComments page
 * SYNOPSIS:
 *      TaskComments({match})
 *          match.params --> the parameters passed by the Router component to this component. The parameters contained 
 *                              in this object are retreived from the parameters in the specified route path for this page
 *          match.params.projectId --> the id of the project whose task comments are to be rendered
 *          match.params.taskId --> the id of the task whose comments are to be rendered
 * DESCRIPTION:
 *      A React functional component that generates JSX to render the page to list all the taskcomments in a task and to 
 *      post a new comment on the task. This components handles the retrieval of all data needed to display all the task comments
 *      and the sending of data related to a new comment to the server
 * RETURNS
 *      JSX that renders the needed page
 * AUTHOR
 *      Biplab Thapa Magar
 * DATE
 *      10/05/2020 
 * /
 /**/
const TaskComments = ({match}) => {
    //useState hooks
    const [taskDetails, setTaskDetails] = useState({});
    const [taskComments, setTaskComments] = useState([]);
    const [projectUsers, setProjectUsers] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);
    const [newTaskComment, setNewTaskComment] = useState("");
    const [commentError, setCommentError] = useState("");

    //useEffect hook to be called on the first render of the page
    useEffect(() => {
        CheckAuthentication();
        fetchTaskDetails();
        fetchTaskComments();
        fetchProjectUsers();
    }, []);


    /**/
    /*
    * NAME:
    *      fetchTaskComments() - async function to retrieve all the comments in a task
    * SYNOPSIS:
    *      fetchTaskComments()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing a list of all the comments
    *       made under a task
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
    const fetchTaskComments = async () => {
        const res = await fetch(`/project/${match.params.taskId}/comments`);
        const data = await res.json();
        setTaskComments(data);
        setContentLoaded(true);
    }

    /**/
    /*
    * NAME:
    *      fetchTaskDetails() - async function to retrieve data on the current task
    * SYNOPSIS:
    *      fetchTaskDetails()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing information on the task
    *      Sets the taskDetails state
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
    const fetchTaskDetails = async () => {
        const res = await fetch(`/project/task/${match.params.taskId}`);
        const data = await res.json();
        setTaskDetails(data);
    }

    /**/
    /*
    * NAME:
    *      fetchProjectUsers() - async function to retrieve information on all the users in a project
    * SYNOPSIS:
    *      fetchProjectUsers()
    * DESCRIPTION:
    *      Makes a GET request to server to receive response containing all the users in the project
    *      Sets the projectMembers state corresponding to retrieved information
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
    const fetchProjectUsers = async () => {
        const res = await fetch(`/project/${match.params.projectId}/users`);
        const data = await res.json();
        setProjectUsers(data); 
    }

    /**/
    /*
    * NAME:
    *      getUserNameById() - gets the user name given the id of the user
    * SYNOPSIS:
    *      getUserNameById(userId)
    *           userId     -->    the id of the user whose name is to be found
    * DESCRIPTION:
    *      Gets the name of the user who id is given
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
    const getUserNameById = (userId) => {
        for(let i = 0; i < projectUsers.length; i++) {
            if(projectUsers[i].id == userId) {
                return projectUsers[i].firstName + " " + projectUsers[i].lastName;
            }
        }
    }

    /**/
    /*
    * NAME:
    *      handleSubmit() - handles the submission of the new task comment form
    * SYNOPSIS:
    *      handleSubmit(e)
    *           e --> the JavaScript event generated when submitting the form
    * DESCRIPTION:
    *      This function executes the action to be taken once the user has filled out the comment form and hit submit.
    *      First it validates the user input in the forms, and sets the error message if user input is not valid.
    *      If user input is valid, it sends a request to the server to add the comment to the task.
    *      Finally, it updates the taskComments state to re-render the page and display the new task
    * RETURNS
    * AUTHOR
    *      Biplab Thapa Magar
    * DATE
    *      10/05/2020 
    * /
    /**/
    const handleSubmit = async (e) => {
        let errorsExist = false;
        //prevent default action
        e.preventDefault();
        //check comment error
        if(newTaskComment.length === 0) {
            setCommentError("Your comment cannot be empty");
            errorsExist = true;
        } else if(newTaskComment.length > 300) {
            setCommentError("Your comment cannot be more than 300 characters long");
            errorsExist = true;
        } 
        else {
            setCommentError("");
        }

        if(errorsExist === false) {   
            //this payload adheres to the UtilityTaskCommentModel in the server        
            const payload = {
                TaskId: taskDetails.taskId,
                Comment: newTaskComment
            }
            console.log(payload);
            //making post request to server
            const response = await fetch(`/project/task/comment/add` , {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(payload)
            });
            const data = await response.json();
            if(response.ok) {
                //update the states in the component
                let updatedComments = [...taskComments];
                updatedComments.push(data);
                setTaskComments(updatedComments);
                setCommentError("");
                setNewTaskComment(""); 
            } else {    
                console.log(data);
            }
        }
    }

    //return the JSX that generates the page. 
    return (
        <div className="page">
            <div className="task-comment">
                <PageDescription 
                    title={`Comments for ${taskDetails.name}`} 
                    description={(`View and add comments to this task`)}
                />
                <div className="comments-listing">
                    <div className="comments-listing-header comments-listing-row">
                        <div className="name column">Comment By</div>
                        <div className="date column">Time</div>
                        <div className="comment column">Comment</div>
                    </div>
                    {contentLoaded ? 
                        taskComments.map((comment, index) => {
                            return (
                                <div key={index} className="comments-listing-row">
                                    <div className="name column">
                                        <Link to={`/profile/${comment.appUserId}`}>
                                            {getUserNameById(comment.appUserId)}
                                        </Link>
                                    </div>
                                    <div className="date column">
                                        {ConvertDate(comment.timeAdded) + " at " + ConvertTime(comment.timeAdded)}
                                    </div>
                                    <div className="comment column">
                                        {comment.comment}
                                    </div>
                                </div>
                            );
                        }) 
                        : <div className="spinner"><LoadingSpinner /> </div>}
                </div>
                <div className="add-comment">
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label htmlFor="task-comment">Enter your comment:</label>
                            <textarea 
                                type="text" 
                                className="form-control" 
                                id="task-comment" 
                                placeholder="Comment on the task" 
                                value={newTaskComment} 
                                onChange={(e) => setNewTaskComment(e.target.value)}
                            />
                            <small className="error-message">
                                {commentError ? commentError : ""}
                            </small>
                            <button type="submit" className="btn btn-primary create">Add Comment</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default TaskComments;