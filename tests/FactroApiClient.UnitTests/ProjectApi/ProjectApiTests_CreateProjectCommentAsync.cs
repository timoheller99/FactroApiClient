namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.Project.Contracts.Comment;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task CreateProjectComment_ValidRequest_ShouldReturnCreatedProjectComment()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var text = Guid.NewGuid().ToString();

            var createProjectCommentRequest = new CreateProjectCommentRequest(text);

            var expectedProjectComment = new CreateProjectCommentResponse
            {
                Text = text,
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedProjectComment, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var createProjectCommentResponse = default(CreateProjectCommentResponse);

            // Act
            Func<Task> act = async () => createProjectCommentResponse = await projectApi.CreateProjectCommentAsync(existingProject.Id, createProjectCommentRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createProjectCommentResponse.Should().BeEquivalentTo(expectedProjectComment);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task CreateProjectComment_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var text = Guid.NewGuid().ToString();

            var createProjectCommentRequest = new CreateProjectCommentRequest(text);

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.CreateProjectCommentAsync(projectId, createProjectCommentRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateProjectComment_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.CreateProjectCommentAsync(Guid.NewGuid().ToString(), createProjectCommentRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateProjectComment_NullRequestModelText_ShouldThrowArgumentNullException()
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            var createProjectCommentRequest = new CreateProjectCommentRequest(text: null);

            // Act
            Func<Task> act = async () => await projectApi.CreateProjectCommentAsync(Guid.NewGuid().ToString(), createProjectCommentRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateProjectComment_BadRequest_ShouldReturnProjectApiException()
        {
            // Arrange
            var text = Guid.NewGuid().ToString();

            var createProjectRequest = new CreateProjectCommentRequest(text);

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
            Func<Task> act = async () => await projectApi.CreateProjectCommentAsync(Guid.NewGuid().ToString(), createProjectRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
