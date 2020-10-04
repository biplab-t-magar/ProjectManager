using Microsoft.AspNetCore.Authorization;

namespace ProjectManager.ProjectManagarUtilities.AuthorizationUtilities
{
    public class IsProjectMemberRequirement : IAuthorizationRequirement
    {
        //This class is used by the mechanism for tracking whether authorization is successful
        //This class does not require any methods or data, it only requires a handler, because the handler does the work 
        //  of checking whether a user id and project id appear together in the ProjectUser class. This class is simply
        //  used to create an authorization policy and map that policy to the corresponding handler

        //This requirement class is used by an AuthorizationHandler to determine whether a user is a member of a project or not

    }
}