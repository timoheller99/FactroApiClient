namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Base;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task DeleteProjectAsync_ValidId_ShouldReturnDeletedProject()
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

            var deleteProjectResponse = new DeleteProjectResponse();

            // Act
            Func<Task> act = async () => deleteProjectResponse = await projectApi.DeleteProjectAsync(existingProject.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deleteProjectResponse.Id.Should().Be(existingProject.Id);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task DeleteProjectAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.DeleteProjectAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteProjectAsync_UnsuccessfulRequest_ShouldReturnNull()
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

            var deleteProjectResponse = new DeleteProjectResponse();

            // Act
            Func<Task> act = async () => deleteProjectResponse = await projectApi.DeleteProjectAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            deleteProjectResponse.Should().BeNull();
        }
    }
}
