namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.Project.Contracts.Structure;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task GetProjectStructureAsync_ValidRequest_ShouldReturnProjectStructure()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingProjectStructure = new GetProjectStructureResponse
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(existingProjectStructure, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var getProjectStructureResponse = new GetProjectStructureResponse();

            // Act
            Func<Task> act = async () =>
                getProjectStructureResponse = await projectApi.GetProjectStructureAsync(existingProject.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getProjectStructureResponse.Id.Should().Be(existingProjectStructure.Id);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task GetProjectStructureAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GetProjectStructureAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task GetProjectStructureAsync_BadRequest_ShouldReturnProjectApiException()
        {
            // Arrange
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
            Func<Task> act = async () => await projectApi.GetProjectStructureAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
