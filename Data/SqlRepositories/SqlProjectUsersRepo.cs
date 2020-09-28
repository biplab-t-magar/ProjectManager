using System.Collections.Generic;
using System.Linq;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.SqlRepositories
{
    public class SqlProjectUsersRepo : IProjectUsersRepo
    {
        private readonly ProjectManagerContext _context;

        public SqlProjectUsersRepo(ProjectManagerContext context)
        {
            _context = context;
        }

        public List<Project> GetProjectsByUser(int userId)
        {

            //first get all the projectuser entries that have the given user id
            var projectUsers = _context.ProjectUsers.Where(p => p.UserId == userId).ToList();
            
            //to store all projects that match the user id
            List<Project> projects = new List<Project>();

            //loop through all the project ids and store matching projects
            for(int i = 0; i < projectUsers.Count; i++)
            {
                projects.Add(_context.Projects.Find(projectUsers[i].ProjectId));
            }

            //return the queried values
            return projects;
            
        }

        public List<User> GetUsersByProject(int projectId)
        {
            //first get all the user ids that are paired with the given project id
            var projectUsers = _context.ProjectUsers.Where(p => p.ProjectId == projectId).ToList();

            //to store all users with the returns user ids
            List<User> users = new List<User>();

            //loop through all user ids and store corresponding user entries
            for(int i = 0; i < projectUsers.Count; i++)
            {
                users.Add(_context.Users.Find(projectUsers[i].UserId));
            }

            //return the queried values
            return users;
        }

        public List<User> GetUsersByRole(int projectId, string role)
        {
            
            var projectUsers = _context.ProjectUsers.Where(p => (p.ProjectId == projectId && p.Role == role)).ToList();

            //store all users corresponding to the given projectId and role
            List<User> users = new List<User>();

            //loop through all users and store corresponding user entries 
            for(int i = 0; i < projectUsers.Count; i++)
            {
                users.Add(_context.Users.Find(projectUsers[i].UserId));
            }

            //return the queried values
            return users;
        }
    }
}