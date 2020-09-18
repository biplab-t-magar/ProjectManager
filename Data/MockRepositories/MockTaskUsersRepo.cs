using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.MockRepositories
{
    public class MockTaskUsersRepo : ITaskUsersRepo
    {
        private List<MockTaskUsersRepo> taskUsers;
        public MockTaskUsersRepo()
        {
            taskUsers = new List<TaskUser>
            {
                new TaskUser{},
            };
        }
    }
}