namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        public async Task GetProjectCommentsAsync_ValidRequest_ShouldGetProjectComments()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingProjectCommentsList = new List<GetProjectCommentPayload>
            {
                new GetProjectCommentPayload
                {
                    Id = Guid.NewGuid().ToString(),
                    ProjectId = existingProject.Id,
                },
                new GetProjectCommentPayload
                {
                    Id = Guid.NewGuid().ToString(),
                    ProjectId = existingProject.Id,
                },
            };

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(existingProjectCommentsList, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var getProjectsResponse = new List<GetProjectCommentPayload>();

            // Act
            Func<Task> act = async () => getProjectsResponse = (await projectApi.GetProjectCommentsAsync(existingProject.Id)).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getProjectsResponse.Should().HaveCount(existingProjectCommentsList.Count);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task GetProjectCommentsAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GetProjectCommentsAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task GetProjectCommentsAsync_BadRequest_ShouldReturnProjectApiException()
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
            Func<Task> act = async () => await projectApi.GetProjectCommentsAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
