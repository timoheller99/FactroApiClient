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
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task GetProjectWriteRightsAsync_ValidRequest_ShouldReturnWriteRights()
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
            Func<Task> act = async () => getWriteRightsResponse = (await projectApi.GetProjectWriteRightsAsync(existingProject.Id)).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getWriteRightsResponse.Should().BeEquivalentTo(existingProjectWriteRights);
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task GetProjectWriteRightsAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.GetProjectWriteRightsAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetProjectWriteRightsAsync_BadRequest_ShouldReturnProjectApiException()
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
            Func<Task> act = async () => await projectApi.GetProjectWriteRightsAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
