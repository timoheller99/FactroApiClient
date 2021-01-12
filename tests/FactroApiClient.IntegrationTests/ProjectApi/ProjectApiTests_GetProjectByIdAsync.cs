namespace FactroApiClient.IntegrationTests.ProjectApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Project;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task GetProjectByIdAsync_ExistingProject_ShouldReturnExpectedProject()
        {
            // Arrange
            var projectApi = this.fixture.GetService<IProjectApi>();

            var createdProject = await this.fixture.CreateTestProjectAsync(projectApi);

            var getProjectByIdResponse = default(GetProjectByIdResponse);

            // Act
            Func<Task> act = async () => getProjectByIdResponse = await projectApi.GetProjectByIdAsync(createdProject.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getProjectByIdResponse.Should().BeEquivalentTo(createdProject);
        }

        [Fact]
        public async Task GetProjectByIdAsync_NotExistingProject_Should()
        {
            // Arrange
            var projectApi = this.fixture.GetService<IProjectApi>();

            var notExistingProjectId = Guid.NewGuid().ToString();

            var getProjectByIdResponse = default(GetProjectByIdResponse);

            // Act
            Func<Task> act = async () => getProjectByIdResponse = await projectApi.GetProjectByIdAsync(notExistingProjectId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            getProjectByIdResponse.Should().BeNull();
        }
    }
}
