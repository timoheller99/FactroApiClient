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
        public async Task CreateProject_ValidRequest_ShouldReturnCreatedProject()
        {
            // Arrange
            var title = Guid.NewGuid().ToString();

            var createProjectRequest = new CreateProjectRequest(title);

            var expectedProject = new CreateProjectResponse
            {
                Title = title,
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedProject, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var createProjectResponse = default(CreateProjectResponse);

            // Act
            Func<Task> act = async () => createProjectResponse = await projectApi.CreateProjectAsync(createProjectRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createProjectResponse.Should().BeEquivalentTo(expectedProject);
        }

        [Fact]
        public async Task CreateProject_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.CreateProjectAsync(createProjectRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateProjectAsync_NullRequestModelTitle_ShouldThrowArgumentNullException()
        {
            // Arrange
            var createProjectRequest = new CreateProjectRequest(title: null);

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.CreateProjectAsync(createProjectRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task CreateProject_BadRequest_ShouldReturnProjectApiException()
        {
            // Arrange
            var title = Guid.NewGuid().ToString();

            var createProjectRequest = new CreateProjectRequest(title);

            var expectedResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var createProjectResponse = new CreateProjectResponse();

            // Act
            Func<Task> act = async () => createProjectResponse = await projectApi.CreateProjectAsync(createProjectRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createProjectResponse.Should().BeNull();
        }
    }
}
