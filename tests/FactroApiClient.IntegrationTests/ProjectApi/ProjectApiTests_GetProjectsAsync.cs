namespace FactroApiClient.IntegrationTests.ProjectApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Project;
    using FactroApiClient.Project.Contracts.Base;

    using FluentAssertions;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task GetProjectsAsync_ExistingProjects_ShouldReturnExistingProjects()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var projectApi = this.fixture.GetService<IProjectApi>();

            var existingProjects = new List<CreateProjectResponse>();

            const int projectCount = 5;

            for (var i = 0; i < projectCount; i++)
            {
                existingProjects.Add(await this.fixture.CreateTestProjectAsync(projectApi));
            }

            var getProjectsResponse = new List<GetProjectPayload>();

            // Act
            Func<Task> act = async () => getProjectsResponse = (await projectApi.GetProjectsAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            foreach (var existingProject in existingProjects)
            {
                getProjectsResponse.Should().ContainEquivalentOf(existingProject);
            }
        }
    }
}
