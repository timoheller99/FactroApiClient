namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task GetProjectsAsync_ValidRequest_ShouldReturnProjects()
        {
            // Arrange
            var existingProjectsList = new List<GetProjectPayload>
            {
                new GetProjectPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
                new GetProjectPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
            };

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(existingProjectsList, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var getProjectsResponse = new List<GetProjectPayload>();

            // Act
            Func<Task> act = async () => getProjectsResponse = (await projectApi.GetProjectsAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getProjectsResponse.Should().HaveCount(existingProjectsList.Count);
        }

        [Fact]
        public async Task GetProjectsAsync_BadRequest_ShouldReturnProjectApiException()
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
            Func<Task> act = async () => await projectApi.GetProjectsAsync();

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
