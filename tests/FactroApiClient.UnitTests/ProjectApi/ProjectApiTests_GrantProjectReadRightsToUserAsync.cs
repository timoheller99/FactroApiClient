namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.AccessRights;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task GrantProjectReadRightsToUserAsync_ValidRequest_ShouldReturnReadRightInformation()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var employeeId = Guid.NewGuid().ToString();
            var addProjectReadRightsForUserRequest = new AddProjectReadRightsForUserRequest(employeeId);

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(addProjectReadRightsForUserRequest, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var getReadRightsResponse = new AddProjectReadRightsForUserResponse();

            // Act
            Func<Task> act = async () => getReadRightsResponse = await projectApi.GrantProjectReadRightsToUserAsync(existingProject.Id, addProjectReadRightsForUserRequest);

            // Assert
            await act.Should().NotThrowAsync();

            getReadRightsResponse.Should().BeEquivalentTo(addProjectReadRightsForUserRequest);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task GrantProjectReadRightsToUserAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var employeeId = Guid.NewGuid().ToString();
            var addProjectReadRightsForUserRequest = new AddProjectReadRightsForUserRequest(employeeId);

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GrantProjectReadRightsToUserAsync(projectId, addProjectReadRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GrantProjectReadRightsToUserAsync_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GrantProjectReadRightsToUserAsync(existingProject.Id, addProjectReadRightsForUserRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GrantProjectReadRightsToUserAsync_NullRequestModelEmployeeId_ShouldThrowArgumentNullException()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var addProjectReadRightsForUserRequest = new AddProjectReadRightsForUserRequest(employeeId: null);

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GrantProjectReadRightsToUserAsync(existingProject.Id, addProjectReadRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GrantProjectReadRightsToUserAsync_BadRequest_ShouldReturnProjectApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var addProjectReadRightsForUserRequest = new AddProjectReadRightsForUserRequest(Guid.NewGuid().ToString());

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
            Func<Task> act = async () => await projectApi.GrantProjectReadRightsToUserAsync(projectId, addProjectReadRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
