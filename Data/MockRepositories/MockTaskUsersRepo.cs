// using System.Collections.Generic;
// using ProjectManager.Data.Interfaces;
// using ProjectManager.Models;

// namespace ProjectManager.Data.MockRepositories
// {
//     public class MockTaskUsersRepo : ITaskUsersRepo
//     {
//         private List<TaskUser> taskUsers;
//         public MockTaskUsersRepo()
//         {
//             taskUsers = new List<TaskUser>
//             {
//                 new TaskUser{ProjectId=1, TaskId=1, UserId=1},
//                 new TaskUser{ProjectId=1, TaskId=1, UserId=3},
//                 new TaskUser{ProjectId=1, TaskId=2, UserId=4},
//                 new TaskUser{ProjectId=1, TaskId=3, UserId=4},
//             };
//         }

//       public IEnumerable<TaskUser> GetTasksByUser(int userId)
//       {
//          throw new System.NotImplementedException();
//       }

//       public IEnumerable<TaskUser> GetTasksInProjectByUser(int projectId, int userId)
//       {
//          throw new System.NotImplementedException();
//       }

//       public IEnumerable<TaskUser> GetUsersByTask(int projectId, int taskId)
//       {
//          throw new System.NotImplementedException();
//       }
//    }
// }