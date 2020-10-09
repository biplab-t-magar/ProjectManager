/* ProjectMemberValidation.cs
 This file contains the ProjectMemberValidation class. The ProjectMemberValidation class is used to determine whether a user
 is a valid member/administrator of a project
*/

using System.Linq;

namespace ProjectManager.Data.Services
{
    public class ProjectMemberValidation
    {
        //a database context object needed to access the database and check for user's association to projects
        private readonly ProjectManagerContext _context;

        /**/
        /*
        * NAME:
        *      ProjectMemberValidation - constructor for the ProjectMemberValidation class
        * SYNOPSIS:
                ProjectMemberValidation(ProjectManagerContext context)
        *           context --> the database context that is injected as a dependency injection
        * DESCRIPTION:
                Initializes the ProjectMemberValidation class
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/16/2020 
        * /
        /**/
        public ProjectMemberValidation(ProjectManagerContext context)
        {
            _context = context;
        }

        /**/
        /*
        * NAME:
        *      userIsProjectMember - checks if a given user is a member of the project
        * SYNOPSIS:
                userIsProjectMember(string userId, int projectId)
        *           userId --> the id of the user to be checked
                    projectId --> the id of the project to be checked
        * DESCRIPTION:
                checks if a given user is a member of the project, and thus whether the user has access to project information
        * RETURNS
                true if the user is a member of the project, false otherwise
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/16/2020 
        * /
        /**/
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

        /**/
        /*
        * NAME:
        *      userIsProjectAdministrator - checks if a given user is an administrator of the project
        * SYNOPSIS:
                userIsProjectAdministrator(string userId, int projectId)
        *           userId --> the id of the user to be checked
                    projectId --> the id of the project to be checked
        * DESCRIPTION:
                checks if a given user is an administrator of the project, and thus whether the user has access to special privileges in the project
        * RETURNS
                true if the user is an administrator of the project, false otherwise
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      09/16/2020 
        * /
        /**/
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