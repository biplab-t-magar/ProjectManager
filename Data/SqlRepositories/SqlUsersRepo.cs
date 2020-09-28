 using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlUsersRepo : IUsersRepo
    {
        private readonly ProjectManagerContext _context;

        public SqlUsersRepo(ProjectManagerContext context)
        {   
            _context = context;
        }
        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }
    }
}