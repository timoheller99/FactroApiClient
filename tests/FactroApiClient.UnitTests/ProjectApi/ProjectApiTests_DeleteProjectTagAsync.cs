namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Tag;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task DeleteProjectTagAsync_ValidRequest_ShouldDeleteProjectTag()
        {
            // Arrange
            var existingProjectTag = new GetProjectTagPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(existingProjectTag, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var deleteProjectTagResponse = new DeleteProjectTagResponse();

            // Act
            Func<Task> act = async () => deleteProjectTagResponse = await projectApi.DeleteProjectTagAsync(existingProjectTag.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deleteProjectTagResponse.Id.Should().Be(existingProjectTag.Id);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectTagIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task DeleteProjectTagAsync_InvalidProjectTagId_ShouldThrowArgumentNullException(string projectTagId)
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.DeleteProjectTagAsync(projectTagId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteProjectTagAsync_BadRequest_ShouldReturnProjectApiException()
        {
            // Arrange
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
            Func<Task> act = async () => await projectApi.DeleteProjectTagAsync(projectTagId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
