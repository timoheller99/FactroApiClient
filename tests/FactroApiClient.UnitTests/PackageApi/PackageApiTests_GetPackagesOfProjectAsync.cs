namespace FactroApiClient.UnitTests.PackageApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Package.Contracts;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class PackageApiTests
    {
        [Fact]
        public async Task GetPackagesOfProjectAsync_ValidRequest_ShouldGetPackagesOfProject()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingPackages = new List<GetPackagePayload>
            {
                new GetPackagePayload
                {
                    Id = Guid.NewGuid().ToString(),
                    ProjectId = existingProject.Id,
                },
                new GetPackagePayload
                {
                    Id = Guid.NewGuid().ToString(),
                    ProjectId = existingProject.Id,
                },
            };

            var expectedPackages = existingPackages.Where(package => package.ProjectId == existingProject.Id);

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedPackages, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            var getPackagesOfProjectResponse = default(IEnumerable<GetPackagePayload>);

            // Act
            Func<Task> act = async () => getPackagesOfProjectResponse = await packageApi.GetPackagesOfProjectAsync(existingProject.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getPackagesOfProjectResponse.Should().BeEquivalentTo(existingPackages);
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GetPackagesOfProjectAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GetPackagesOfProjectAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetPackagesOfProjectAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
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

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            // Act
            Func<Task> act = async () => await packageApi.GetPackagesOfProjectAsync(projectId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
