using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.MockRepositories
{
    public class MockOrganizationsRepo : IOrganizationsRepo
    {
        private List<Organization> organizations;

        public MockOrganizationsRepo()
        {
            organizations = new List<Organization>
            {
                new Organization{OrganizationId=1, Name="Biplab and Co."},
                new Organization{OrganizationId=2, Name="Quintech"}
            };
        }
        public Organization GetOrganizationById(int organizationId)
        {
            for(int i = 0; i < organizations.Count; i++)
            {
                if(organizations[i].OrganizationId == organizationId)
                {
                    return organizations[i];
                }
            }
            return null;
        }
    }
}