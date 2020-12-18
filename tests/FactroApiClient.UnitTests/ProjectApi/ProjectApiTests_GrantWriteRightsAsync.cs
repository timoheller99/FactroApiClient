namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.AccessRights;
    using FactroApiClient.Project.Contracts.Base;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task GrantWriteRightsAsync_ValidRequest_ShouldReturnWriteRightInformation()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var employeeId = Guid.NewGuid().ToString();
            var addProjectWriteRightsForUserRequest = new AddProjectWriteRightsForUserRequest(employeeId);

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(addProjectWriteRightsForUserRequest, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var getWriteRightsResponse = new AddProjectWriteRightsForUserResponse();

            // Act
            Func<Task> act = async () => getWriteRightsResponse = await projectApi.GrantWriteRightsToUserAsync(existingProject.Id, addProjectWriteRightsForUserRequest);

            // Assert
            await act.Should().NotThrowAsync();

            getWriteRightsResponse.Should().BeEquivalentTo(addProjectWriteRightsForUserRequest);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task GrantWriteRightsAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var employeeId = Guid.NewGuid().ToString();
            var addProjectWriteRightsForUserRequest = new AddProjectWriteRightsForUserRequest(employeeId);

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GrantWriteRightsToUserAsync(projectId, addProjectWriteRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GrantWriteRightsAsync_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GrantWriteRightsToUserAsync(existingProject.Id, addProjectWriteRightsForUserRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GrantWriteRightsAsync_NullRequestModelEmployeeId_ShouldThrowArgumentNullException()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var addProjectWriteRightsForUserRequest = new AddProjectWriteRightsForUserRequest(employeeId: null);

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GrantWriteRightsToUserAsync(existingProject.Id, addProjectWriteRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task GrantWriteRightsAsync_BadRequest_ShouldReturnProjectApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var addProjectWriteRightsForUserRequest = new AddProjectWriteRightsForUserRequest(Guid.NewGuid().ToString());

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
            Func<Task> act = async () => await projectApi.GrantWriteRightsToUserAsync(projectId, addProjectWriteRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
