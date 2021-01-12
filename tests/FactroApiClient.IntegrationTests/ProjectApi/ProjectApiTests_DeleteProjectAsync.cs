namespace FactroApiClient.IntegrationTests.ProjectApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Project;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task DeleteProjectAsync_ExistingProject_ShouldDeleteProject()
        {
            // Arrange
            var projectApi = this.fixture.GetService<IProjectApi>();

            var createdProject = await this.fixture.CreateTestProjectAsync(projectApi);

            var deleteProjectResponse = default(DeleteProjectResponse);

            // Act
            Func<Task> act = async () => deleteProjectResponse = await projectApi.DeleteProjectAsync(createdProject.Id);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                deleteProjectResponse.Should().NotBeNull();

                var projects = await this.fixture.GetProjectsAsync(projectApi);

                projects.Should().NotContain(project => project.Id == createdProject.Id);
            }
        }

        [Fact]
        public async Task DeleteProjectAsync_NotExistingProject_ShouldThrowFactroApiException()
        {
            // Arrange
            var projectApi = this.fixture.GetService<IProjectApi>();

            var notExistingProjectId = Guid.NewGuid().ToString();

            // Act
            Func<Task> act = async () => await projectApi.DeleteProjectAsync(notExistingProjectId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
