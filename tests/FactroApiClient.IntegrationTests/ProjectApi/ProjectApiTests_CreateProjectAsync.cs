namespace FactroApiClient.IntegrationTests.ProjectApi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Project;
    using FactroApiClient.Project.Contracts.Base;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task CreateProjectAsync_ValidProject_ShouldCreateProject()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var projectApi = this.fixture.GetService<IProjectApi>();

            var title = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createProjectRequest = new CreateProjectRequest(title);

            var createProjectResponse = default(CreateProjectResponse);

            // Act
            Func<Task> act = async () => createProjectResponse = await projectApi.CreateProjectAsync(createProjectRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var projects = await this.fixture.GetProjectsAsync(projectApi);

                projects.Should().ContainEquivalentOf(createProjectResponse);
            }
        }

        [Fact]
        public async Task CreateProjectAsync_TwoProjectsWithSameTitle_ShouldCreateProjectTwice()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var projectApi = this.fixture.GetService<IProjectApi>();

            var title = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createProjectRequest = new CreateProjectRequest(title);

            await projectApi.CreateProjectAsync(createProjectRequest);

            // Act
            Func<Task> act = async () => await projectApi.CreateProjectAsync(createProjectRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var projects = await this.fixture.GetProjectsAsync(projectApi);

                projects.Where(project => project.Title == title).Should().HaveCount(2);
            }
        }
    }
}
