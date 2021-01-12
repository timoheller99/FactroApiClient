namespace FactroApiClient.IntegrationTests.ProjectApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Project;
    using FactroApiClient.Project.Contracts.Base;

    public sealed class ProjectApiTestFixture : BaseTestFixture
    {
        public ProjectApiTestFixture()
        {
            this.ClearFactroInstanceAsync().GetAwaiter().GetResult();
        }

        public async Task<CreateProjectResponse> CreateTestProjectAsync(IProjectApi projectApi)
        {
            var title = $"{TestPrefix}{Guid.NewGuid().ToString()}";

            var createProjectRequest = new CreateProjectRequest(title);

            var createProjectResponse = await projectApi.CreateProjectAsync(createProjectRequest);

            return createProjectResponse;
        }

        public async Task<IEnumerable<GetProjectPayload>> GetProjectsAsync(IProjectApi projectApi)
        {
            return (await projectApi.GetProjectsAsync()).Where(x => x.Title.StartsWith(TestPrefix));
        }

        protected override async Task ClearFactroInstanceAsync()
        {
            await this.ClearProjectsAsync();
        }

        private async Task ClearProjectsAsync()
        {
            var service = this.GetService<IProjectApi>();

            var projects = await service.GetProjectsAsync();

            var projectsToRemove = projects.Where(x => x.Title.StartsWith(TestPrefix));

            var tasks = projectsToRemove.Select(x => service.DeleteProjectAsync(x.Id));

            await Task.WhenAll(tasks);
        }
    }
}
