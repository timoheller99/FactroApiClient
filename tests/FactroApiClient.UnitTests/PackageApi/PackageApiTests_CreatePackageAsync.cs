namespace FactroApiClient.UnitTests.PackageApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Package.Contracts.Base;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class PackageApiTests
    {
        [Fact]
        public async Task CreatePackageAsync_ValidRequest_ShouldReturnCreatedPackage()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var title = Guid.NewGuid().ToString();

            var createPackageRequest = new CreatePackageRequest(title);

            var expectedPackage = new CreatePackageResponse
            {
                Title = title,
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedPackage, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            var createPackageResponse = default(CreatePackageResponse);

            // Act
            Func<Task> act = async () => createPackageResponse = await packageApi.CreatePackageAsync(existingProject.Id, createPackageRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createPackageResponse.Should().BeEquivalentTo(expectedPackage);
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task CreatePackageAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var title = Guid.NewGuid().ToString();

            var createPackageRequest = new CreatePackageRequest(title);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.CreatePackageAsync(projectId, createPackageRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreatePackageAsync_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.CreatePackageAsync(existingProject.Id, createPackageRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreatePackageAsync_NullRequestModelTitle_ShouldThrowArgumentNullException()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var createPackageRequest = new CreatePackageRequest(title: null);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.CreatePackageAsync(existingProject.Id, createPackageRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreatePackageAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var createProjectRequest = new CreatePackageRequest(Guid.NewGuid().ToString());

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
            Func<Task> act = async () => await packageApi.CreatePackageAsync(projectId, createProjectRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
