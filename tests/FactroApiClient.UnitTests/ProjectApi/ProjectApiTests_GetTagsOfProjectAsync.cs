namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        public async Task GetTagsOfProjectAsync_ValidRequest_ShouldReturnTagsOfProject()
        {
            // Arrange
            var existingProject = new GetProjectTagPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingProjectTagsList = new List<GetAssignedProjectTagPayload>
            {
                new GetAssignedProjectTagPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
                new GetAssignedProjectTagPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
            };

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(existingProjectTagsList, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var getProjectTagsResponse = new List<GetAssignedProjectTagPayload>();

            // Act
            Func<Task> act = async () => getProjectTagsResponse = (await projectApi.GetTagsOfProjectAsync(existingProject.Id)).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getProjectTagsResponse.Should().HaveCount(existingProjectTagsList.Count);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task GetTagsOfProjectAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GetTagsOfProjectAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task GetTagsOfProjectAsync_BadRequest_ShouldReturnProjectApiException()
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
            Func<Task> act = async () => await projectApi.GetTagsOfProjectAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
