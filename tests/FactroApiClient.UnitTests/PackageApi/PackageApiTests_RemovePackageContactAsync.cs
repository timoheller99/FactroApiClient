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

    using Xunit;

    public partial class PackageApiTests
    {
        [Fact]
        public async Task RemovePackageContactAsync_ValidRequest_ShouldReturnVoid()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
                ProjectId = existingProject.Id,
            };

            var expectedResponseMessage = new HttpResponseMessage();

            var packageApi = this.fixture.GetPackageApi(expectedResponseMessage);

            // Act
            Func<Task> act = async () => await packageApi.RemovePackageContactAsync(existingProject.Id, existingPackage.Id);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task RemovePackageContactAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
                ProjectId = existingProject.Id,
            };

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.RemovePackageContactAsync(projectId, existingPackage.Id);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task RemovePackageContactAsync_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.RemovePackageContactAsync(existingProject.Id, packageId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task RemovePackageContactAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var packageId = Guid.NewGuid().ToString();

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
            Func<Task> act = async () => await packageApi.RemovePackageContactAsync(projectId, packageId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
