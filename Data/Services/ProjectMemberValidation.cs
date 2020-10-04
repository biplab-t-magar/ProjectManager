using System.Linq;

namespace ProjectManager.Data.Services
{
    public class ProjectMemberValidation
    {
        private readonly ProjectManagerContext _context;

        public ProjectMemberValidation(ProjectManagerContext context)
        {
            _context = context;
        }

        public bool userIsProjectMember(string userId, int projectId)
        {
            //find the projectUser entry with the given userId and projectId's in their given fields
            var projectUsers = _context.ProjectUsers.Where(pu => pu.ProjectId == projectId && pu.AppUserId == userId).ToList();

            //if no such entry is found, then the user is not a member of the project
            if(projectUsers.Count == 0) 
            {
                return false;
            }
            return true;
        }

        public bool userIsProjectAdministrator(string userId, int projectId)
        {
            //find the projectUser entry with the given userId and projectIds in their given fields
            var projectUsers = _context.ProjectUsers.Where(pu => pu.ProjectId == projectId && pu.AppUserId == userId).ToList();

            //if no such entry is found, return false
            if(projectUsers.Count == 0)
            {
                return false;
            }

            //check if role is set to administrator
            for(int i = 0; i < projectUsers.Count; i++)
            {
                if(projectUsers[i].Role == "Administrator")
                {
                    return true;
                }
            }
            return false;
        }

    }
}