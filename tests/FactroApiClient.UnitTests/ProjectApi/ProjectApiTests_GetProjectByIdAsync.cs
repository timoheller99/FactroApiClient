namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task GetProjectByIdAsync_ValidRequest_ShouldReturnExpectedProject()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(existingProject, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var getProjectByIdResponse = new GetProjectByIdResponse();

            // Act
            Func<Task> act = async () => getProjectByIdResponse = await projectApi.GetProjectByIdAsync(existingProject.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getProjectByIdResponse.Id.Should().Be(existingProject.Id);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task GetProjectByIdAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GetProjectByIdAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetProjectByIdAsync_BadRequest_ShouldReturnProjectApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();

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
            Func<Task> act = async () => await projectApi.GetProjectByIdAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
