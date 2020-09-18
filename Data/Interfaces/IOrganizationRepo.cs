using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data.Interfaces
{
    public interface IOrganizationRepo
    {
        string GetOrganizationName(int organizationId);
    } 
}