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
            await this.fixture.ClearFactroInstanceAsync();

            var projectApi = this.fixture.GetService<IProjectApi>();

            var createdProject = await this.fixture.CreateTestProjectAsync(projectApi);

            var getProjectByIdResponse = default(GetProjectByIdResponse);

            // Act
            Func<Task> act = async () => getProjectByIdResponse = await projectApi.GetProjectByIdAsync(createdProject.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getProjectByIdResponse.Should().BeEquivalentTo(createdProject);

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task GetProjectByIdAsync_NotExistingProject_Should()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var projectApi = this.fixture.GetService<IProjectApi>();

            var notExistingProjectId = Guid.NewGuid().ToString();

            var getProjectByIdResponse = default(GetProjectByIdResponse);

            // Act
            Func<Task> act = async () => getProjectByIdResponse = await projectApi.GetProjectByIdAsync(notExistingProjectId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            getProjectByIdResponse.Should().BeNull();

            await this.fixture.ClearFactroInstanceAsync();
        }
    }
}
