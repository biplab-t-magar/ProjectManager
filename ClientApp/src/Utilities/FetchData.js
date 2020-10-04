import React from "react";

const FetchData = {
    fetchProjectDetails: async ({projectId, setProjectDetails}) => {
        const res = await fetch(`/project/${projectId}`);
        const data = await res.json();
        setProjectDetails(data);
    },
    
    fetchProjectMembers: async ({projectId, setProjectMembers}) => {
        const res = await fetch(`/project/${projectId}/users`);
        const data = await res.json();
        setProjectMembers(data);
    },
    
    fetchUserRoles: async ({projectId, setProjectUserRoles}) => {
        const res = await fetch(`/project/${projectId}/roles`);
        const data = await res.json();
        setProjectUserRoles(data);
    },
    
    fetchProjectTasks: async ({projectId, setProjectTasks}) => {
    
    },
    
    fetchRecentProjectTasks: async ({projectId, setRecentProjectTasks, numOfTasks}) => {
        //get latest 15 tasks from project
        const res = await fetch(`/project/${projectId}/tasks?numOfTasks=${numOfTasks}`);
        const data = await res.json();
        setRecentProjectTasks(data);
    },
    
    fetchTaskTypes: async ({projectId, setProjectTaskTypes}) => {
        const res = await fetch(`/project/${projectId}/taskTypes`);
        const data = await res.json();
        setProjectTaskTypes(data);
    }
}


export default FetchData;