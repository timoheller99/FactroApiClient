namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.Project.Contracts.Comment;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task DeleteProjectCommentAsync_ValidRequest_ShouldDeleteProjectComment()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingProjectComment = new GetProjectCommentPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(existingProjectComment, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var deleteProjectCommentResponse = new DeleteProjectCommentResponse();

            // Act
            Func<Task> act = async () => deleteProjectCommentResponse = await projectApi.DeleteProjectCommentAsync(existingProject.Id, existingProjectComment.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deleteProjectCommentResponse.Id.Should().Be(existingProjectComment.Id);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task DeleteProjectCommentAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingProjectComment = new GetProjectCommentPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.DeleteProjectCommentAsync(projectId, existingProjectComment.Id);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectCommentIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task DeleteProjectCommentAsync_InvalidCommentId_ShouldThrowArgumentNullException(string commentId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.DeleteProjectCommentAsync(existingProject.Id, commentId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task DeleteProjectCommentAsync_BadRequest_ShouldReturnProjectApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var commentId = Guid.NewGuid().ToString();

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
            Func<Task> act = async () => await projectApi.DeleteProjectCommentAsync(projectId, commentId);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
