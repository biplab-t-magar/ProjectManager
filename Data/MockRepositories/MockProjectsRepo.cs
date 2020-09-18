using System.Collections.Generic;
using ProjectManager.Data.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Data.MockRepositories
{
    public class MockProjectsRepo : IProjectsRepo
    {
        private List<Project> projects;

        public MockProjectsRepo()
        {
            projects = new List<Project>
            {
                new Project{ProjectId=1, Name="Biplab's Senior Project", DateCreated="06/31/2020 09:43:32", OrganizationId=1},
                new Project{ProjectId=2, Name="Business Startup", DateCreated="08/31/2020 09:43:32", OrganizationId=2},
                new Project{ProjectId=3, Name="Indie Game Project", DateCreated="03/31/2020 09:43:32"}
            };
        }
        public Project GetProjectById(int projectId)
        {
            for(int i = 0; i < projects.Count; i++)
            {
                if(projects[i].ProjectId == projectId)
                {
                    return projects[i];
                }
            }
            return null;
        }

        public IEnumerable<Project> GetProjectsByOrganization(int organizationId)
        {
            List<Project> projectsByOrganization = new List<Project>();
            for(int i = 0; i < projects.Count; i++)
            {
                if(projects[i].OrganizationId == organizationId)
                {
                    projectsByOrganization.Add(projects[i]);
                }
            }
            return projectsByOrganization;
        }
    }
}