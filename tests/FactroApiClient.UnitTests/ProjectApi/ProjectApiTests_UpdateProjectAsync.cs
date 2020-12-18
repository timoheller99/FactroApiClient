namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Base;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task UpdateProjectAsync_ValidRequest_ShouldReturnUpdatedProject()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
                Description = "TestDescription",
            };

            var updateProjectRequest = new UpdateProjectRequest
            {
                Description = "NewDescription",
            };

            var expectedUpdatedProject = new UpdateProjectResponse
            {
                Id = existingProject.Id,
                Description = updateProjectRequest.Description,
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedUpdatedProject, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var updateProjectResponse = new UpdateProjectResponse();

            // Act
            Func<Task> act = async () =>
                updateProjectResponse = await projectApi.UpdateProjectAsync(existingProject.Id, updateProjectRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                updateProjectResponse.Id.Should().Be(existingProject.Id);
                updateProjectResponse.Description.Should().Be(expectedUpdatedProject.Description);
            }
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task UpdateProjectAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            var updateProjectRequest = new UpdateProjectRequest();

            // Act
            Func<Task> act = async () => await projectApi.UpdateProjectAsync(projectId, updateProjectRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateProjectAsync_NullRequestModel_ShouldReturnUpdatedProject()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.UpdateProjectAsync(existingProject.Id, updateProjectRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task UpdateProjectAsync_BadRequest_ShouldReturnProjectApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();

            var updateProjectRequest = new UpdateProjectRequest();

            var expectedResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            // Act
            Func<Task> act = async () => await projectApi.UpdateProjectAsync(projectId, updateProjectRequest);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
