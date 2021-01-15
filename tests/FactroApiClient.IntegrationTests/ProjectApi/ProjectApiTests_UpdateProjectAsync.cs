namespace FactroApiClient.IntegrationTests.ProjectApi
{
    using System;
    using System.Linq;
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
        public async Task UpdateProjectAsync_ValidUpdate_ShouldUpdateProject()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var projectApi = this.fixture.GetService<IProjectApi>();

            var existingProject = await this.fixture.CreateTestProjectAsync(projectApi);

            var updatedTitle = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";
            var updateProjectRequest = new UpdateProjectRequest
            {
                Title = updatedTitle,
            };

            var updateProjectResponse = new UpdateProjectResponse();

            // Act
            Func<Task> act = async () => updateProjectResponse = await projectApi.UpdateProjectAsync(existingProject.Id, updateProjectRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var projects = (await this.fixture.GetProjectsAsync(projectApi)).ToList();

                projects.Should().ContainEquivalentOf(updateProjectResponse);

                projects.Single(x => x.Id == existingProject.Id).Title.Should().Be(updatedTitle);
            }
        }

        [Fact]
        public async Task UpdateProjectAsync_InvalidUpdate_ShouldNotUpdateeProject()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var projectApi = this.fixture.GetService<IProjectApi>();

            var existingProject = await this.fixture.CreateTestProjectAsync(projectApi);

            var notExistingOfficerId = Guid.NewGuid().ToString();

            var updateProjectRequest = new UpdateProjectRequest
            {
                OfficerId = notExistingOfficerId,
            };

            // Act
            Func<Task> act = async () => await projectApi.UpdateProjectAsync(existingProject.Id, updateProjectRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            using (new AssertionScope())
            {
                var projects = (await this.fixture.GetProjectsAsync(projectApi)).ToList();

                projects.Should().ContainEquivalentOf(existingProject);
            }
        }

        [Fact]
        public async Task UpdateProjectAsync_NotExistingProject_Should()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var projectApi = this.fixture.GetService<IProjectApi>();

            var notExistingProjectId = Guid.NewGuid().ToString();

            var updatedTitle = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";
            var updateProjectRequest = new UpdateProjectRequest
            {
                Title = updatedTitle,
            };

            // Act
            Func<Task> act = async () => await projectApi.UpdateProjectAsync(notExistingProjectId, updateProjectRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            using (new AssertionScope())
            {
                var projects = (await this.fixture.GetProjectsAsync(projectApi)).ToList();

                projects.Should().NotContain(project => project.Id == notExistingProjectId);
            }
        }
    }
}
