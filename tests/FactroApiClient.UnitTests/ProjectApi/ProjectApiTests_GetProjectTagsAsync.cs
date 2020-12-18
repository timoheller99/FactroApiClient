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
        public async Task GetProjectTagsAsync_ValidRequest_ShouldReturnProjectTags()
        {
            // Arrange
            var existingProjectTagsList = new List<GetProjectTagPayload>
            {
                new GetProjectTagPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
                new GetProjectTagPayload
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

            var getProjectTagsResponse = new List<GetProjectTagPayload>();

            // Act
            Func<Task> act = async () => getProjectTagsResponse = (await projectApi.GetProjectTagsAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getProjectTagsResponse.Should().HaveCount(existingProjectTagsList.Count);
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task GetProjectTagsAsync_BadRequest_ShouldReturnProjectApiException()
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
            Func<Task> act = async () => await projectApi.GetProjectTagsAsync();

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
