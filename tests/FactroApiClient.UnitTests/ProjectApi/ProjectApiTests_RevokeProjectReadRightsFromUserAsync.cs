namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task RevokeProjectReadRightsFromUserAsync_ValidRequest_ShouldReturnVoid()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var employeeId = Guid.NewGuid().ToString();

            var expectedResponse = new HttpResponseMessage();

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            // Act
            Func<Task> act = async () => await projectApi.RevokeProjectReadRightsFromUserAsync(existingProject.Id, employeeId);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task RevokeProjectReadRightsFromUserAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var employeeId = Guid.NewGuid().ToString();

            var expectedResponse = new HttpResponseMessage();

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            // Act
            Func<Task> act = async () => await projectApi.RevokeProjectReadRightsFromUserAsync(projectId, employeeId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidEmployeeIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task RevokeProjectReadRightsFromUserAsync_InvalidEmployeeId_ShouldThrowArgumentNullException(string employeeId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedResponse = new HttpResponseMessage();

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            // Act
            Func<Task> act = async () => await projectApi.RevokeProjectReadRightsFromUserAsync(existingProject.Id, employeeId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task RevokeProjectReadRightsFromUserAsync_BadRequest_ShouldThrowProjectApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var employeeId = Guid.NewGuid().ToString();

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
            Func<Task> act = async () => await projectApi.RevokeProjectReadRightsFromUserAsync(projectId, employeeId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
