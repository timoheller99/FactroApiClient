namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.Project.Contracts.Tag;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task RemoveTagFromProjectAsync_ValidRequest_ShouldReturnVoid()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingTag = new GetProjectTagPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedResponse = new HttpResponseMessage();

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            // Act
            Func<Task> act = async () => await projectApi.RemoveTagFromProjectAsync(existingProject.Id, existingTag.Id);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task RemoveTagFromProjectAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingTag = new GetProjectTagPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.RemoveTagFromProjectAsync(projectId, existingTag.Id);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectTagIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task RemoveTagFromProjectAsync_InvalidTagId_ShouldThrowArgumentNullException(string projectTagId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.RemoveTagFromProjectAsync(existingProject.Id, projectTagId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task RemoveTagFromProjectAsync_BadRequest_ShouldThrowProjectApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var projectTagId = Guid.NewGuid().ToString();

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
            Func<Task> act = async () => await projectApi.RemoveTagFromProjectAsync(projectId, projectTagId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
