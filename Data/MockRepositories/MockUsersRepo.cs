using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.MockRepositories
{
    public class MockUsersRepo : IUsersRepo
    {
        private List<User> users;
        public MockUsersRepo()
        {
            users = new List<User>
            {
                new User{UserId=1, FirstName="Biplab", LastName="Thapa Magar", OrganizationId=1},
                new User{UserId=3, FirstName="Henry", LastName="Stevens", MiddleName="J.", OrganizationId=1},
                new User{UserId=2, FirstName="Sam", LastName="Smith", OrganizationId=2},
                new User{UserId=4, FirstName="Michael", LastName="Snow", OrganizationId=1},
                new User{UserId=5, FirstName="Wallace", LastName="Stevens", OrganizationId=2},
                new User{UserId=6, FirstName="William", LastName="Williams", MiddleName="Carlos", OrganizationId=2},
                new User{UserId=7, FirstName="John", LastName="Smith", MiddleName="Michael"},
            };
        }

        public User GetUserById(int userId)
        {
            for(int i=0; i < users.Count; i++)
            {
                if(users[i].UserId == userId)
                {
                    return users[i];
                }
            }
            return null;
        }

        public IEnumerable<User> GetUsersByOrganization(int organizationId)
        {
            List<User> usersInOrg = new List<User>();
            for(int i=0; i < users.Count; i++)
            {
                if(users[i].OrganizationId == organizationId)
                {
                    usersInOrg.Add(users[i]);
                }
            }
            return usersInOrg;
        }
    }
}