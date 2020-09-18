using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IOrganizationsRepo
    {
        Organization GetOrganizationById(int organizationId);
    } 
}