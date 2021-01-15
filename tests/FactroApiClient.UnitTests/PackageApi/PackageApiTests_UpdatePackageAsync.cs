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
    using FluentAssertions.Execution;

    using Newtonsoft.Json;

    using Xunit;

    public partial class PackageApiTests
    {
        [Fact]
        public async Task UpdatePackageAsync_ValidRequest_ShouldReturnUpdatePackage()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
                Description = "TestDescription",
            };

            var updatePackageRequest = new UpdatePackageRequest
            {
                Description = "NewDescription",
            };

            var expectedUpdatedPackage = new UpdatePackageResponse
            {
                Id = existingPackage.Id,
                Description = updatePackageRequest.Description,
            };

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(expectedUpdatedPackage, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            var updatePackageResponse = new UpdatePackageResponse();

            // Act
            Func<Task> act = async () =>
                updatePackageResponse = await packageApi.UpdatePackageAsync(existingProject.Id, existingPackage.Id, updatePackageRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                updatePackageResponse.Id.Should().Be(existingPackage.Id);
                updatePackageResponse.Description.Should().Be(expectedUpdatedPackage.Description);
            }
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task UpdatePackageAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
                Description = "TestDescription",
            };

            var updatePackageRequest = new UpdatePackageRequest
            {
                Description = "NewDescription",
            };

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.UpdatePackageAsync(projectId, existingPackage.Id, updatePackageRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task UpdatePackageAsync_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var updatePackageRequest = new UpdatePackageRequest
            {
                Description = "NewDescription",
            };

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.UpdatePackageAsync(existingProject.Id, packageId, updatePackageRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdatePackageAsync_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
                Description = "TestDescription",
            };

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.UpdatePackageAsync(existingProject.Id, existingPackage.Id, updatePackageRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdatePackageAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var packageId = Guid.NewGuid().ToString();

            var updateProjectRequest = new UpdatePackageRequest();

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
            Func<Task> act = async () => await packageApi.UpdatePackageAsync(projectId, packageId, updateProjectRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
