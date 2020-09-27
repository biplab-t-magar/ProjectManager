// using System.Collections.Generic;
// using ProjectManager.Data.Interfaces;
// using ProjectManager.Models;

// namespace ProjectManager.Data.MockRepositories
// {
//     public class MockProjectUsersRepo : IProjectUsersRepo
//     {   
//         private List<ProjectUser> projectUsers;
//         public MockProjectUsersRepo()
//         {
//             projectUsers = new List<ProjectUser>
//             {
//                 new ProjectUser{ProjectId=1, UserId=1, Role="Manager"},
//                 new ProjectUser{ProjectId=1, UserId=3, Role="Member"},
//                 new ProjectUser{ProjectId=1, UserId=4, Role="Spectator"},
//                 new ProjectUser{ProjectId=2, UserId=2, Role="Member"},
//                 new ProjectUser{ProjectId=2, UserId=5, Role="Member"},
//                 new ProjectUser{ProjectId=2, UserId=6, Role="Manager"},
//                 new ProjectUser{ProjectId=3, UserId=7, Role="Manager"},
//             };
//         }

//         public IEnumerable<ProjectUser> GetUsersByProject(int projectId)
//         {
//             List<ProjectUser> usersByProject = new List<ProjectUser>();
//             for(int i=0; i < projectUsers.Count; i++)
//             {
//                 if(projectUsers[i].ProjectId == projectId)
//                 {
//                     usersByProject.Add(projectUsers[i]);
//                 }
//             }
//             return usersByProject;
//         }

//         public IEnumerable<ProjectUser> GetProjectsByUser(int userId)
//         {
//             List<ProjectUser> projectByUsers = new List<ProjectUser>();
//             for(int i=0; i < projectUsers.Count; i++)
//             {
//                 if(projectUsers[i].ProjectId == userId)
//                 {
//                     projectByUsers.Add(projectUsers[i]);
//                 }
//             }
//             return projectByUsers;
//         }

//         public ProjectUser GetProjectUser(int projectId, int userId)
//         {
//             for(int i = 0; i < projectUsers.Count; i++)
//             {
//                 if(projectUsers[i].ProjectId == projectId && projectUsers[i].UserId == userId)
//                 {
//                     return projectUsers[i];
//                 }
//             }
//             return null;
//         }

        
//     }
// }