namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Tag;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task CreateProjectTagAsync_ValidRequest_ShouldCreateProjectTag()
        {
            // Arrange
            var tagName = Guid.NewGuid().ToString();

            var createProjectTagRequest = new CreateProjectTagRequest(tagName);

            var expectedProjectTag = new CreateProjectTagResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = tagName,
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedProjectTag, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var createProjectTagResponse = default(CreateProjectTagResponse);

            // Act
            Func<Task> act = async () => createProjectTagResponse = await projectApi.CreateProjectTagAsync(createProjectTagRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createProjectTagResponse.Should().BeEquivalentTo(expectedProjectTag);
        }

        [Fact]
        public async Task CreateProjectTagAsync_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.CreateProjectTagAsync(createProjectTagRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateProjectTagAsync_NullRequestModelName_ShouldThrowArgumentNullException()
        {
            // Arrange
            var createProjectTagRequest = new CreateProjectTagRequest(name: null);

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.CreateProjectTagAsync(createProjectTagRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task CreateProjectTagAsync_BadRequest_ShouldThrowProjectApiException()
        {
            // Arrange
            var existingProjectTag = new GetProjectTagPayload
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var createProjectTagRequest = new CreateProjectTagRequest(existingProjectTag.Name);

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
            Func<Task> act = async () => await projectApi.CreateProjectTagAsync(createProjectTagRequest);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
