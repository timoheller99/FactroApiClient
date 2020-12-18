namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.AccessRights;
    using FactroApiClient.Project.Contracts.Base;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task GetWriteRightsAsync_ValidRequest_ShouldReturnWriteRights()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingProjectWriteRights = new List<GetProjectWriteRightsResponse>
            {
                new GetProjectWriteRightsResponse
                {
                    EmployeeId = Guid.NewGuid().ToString(),
                },
            };

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(existingProjectWriteRights, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            var getWriteRightsResponse = new List<GetProjectWriteRightsResponse>();

            // Act
            Func<Task> act = async () => getWriteRightsResponse = (await projectApi.GetWriteRightsAsync(existingProject.Id)).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getWriteRightsResponse.Should().BeEquivalentTo(existingProjectWriteRights);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task GetWriteRightsAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GetWriteRightsAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task GetWriteRightsAsync_BadRequest_ShouldReturnProjectApiException()
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
            Func<Task> act = async () => await projectApi.GetWriteRightsAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
