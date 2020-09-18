using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IUsersRepo
    {
        User GetUserById(int userId);
        IEnumerable<User> GetUsersByOrganization(int organizationId);
        
        
    }
}