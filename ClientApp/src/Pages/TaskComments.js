import React, { useEffect, useState } from "react"
import PageDescription from "../Components/PageDescription";
import CheckAuthentication from "../Utilities/CheckAuthentication";
import ConvertDate from "../Utilities/ConvertDate.js";
import ConvertTime from "../Utilities/ConvertTime.js";
import LoadingSpinner from "../Utilities/LoadingSpinner";
import "../CSS/TaskComments.css";
import { Link } from "react-router-dom";


const TaskComments = ({match}) => {
    const [taskDetails, setTaskDetails] = useState({});
    const [taskComments, setTaskComments] = useState([]);
    const [projectUsers, setProjectUsers] = useState([]);
    const [contentLoaded, setContentLoaded] = useState(false);
    const [newTaskComment, setNewTaskComment] = useState("");
    const [commentError, setCommentError] = useState("");

    useEffect(() => {
        CheckAuthentication();
        fetchTaskDetails();
        fetchTaskComments();
        fetchProjectUsers();
    }, []);


    const fetchTaskComments = async () => {
        const res = await fetch(`/project/${match.params.taskId}/comments`);
        const data = await res.json();
        setTaskComments(data);
        setContentLoaded(true);
    }

    const fetchTaskDetails = async () => {
        const res = await fetch(`/project/task/${match.params.taskId}`);
        const data = await res.json();
        setTaskDetails(data);
    }

    const fetchProjectUsers = async () => {
        const res = await fetch(`/project/${match.params.projectId}/users`);
        const data = await res.json();
        setProjectUsers(data); 
    }

    const getUserNameById = (userId) => {
        for(let i = 0; i < projectUsers.length; i++) {
            if(projectUsers[i].id == userId) {
                return projectUsers[i].firstName + " " + projectUsers[i].lastName;
            }
        }
    }

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
                                        <Link to={`/user/${comment.appUserId}`}>
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