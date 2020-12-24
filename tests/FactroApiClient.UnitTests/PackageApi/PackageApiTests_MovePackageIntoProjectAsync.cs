namespace FactroApiClient.UnitTests.PackageApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Package.Contracts;
    using FactroApiClient.Package.Contracts.Association;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class PackageApiTests
    {
        [Fact]
        public async Task MovePackageIntoProjectAsync_ValidRequest_ShouldReturnVoid()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var parentPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var setProjectAssociationRequest = new SetProjectAssociationRequest(parentPackage.Id);

            var expectedResponse = new HttpResponseMessage();

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            // Act
            Func<Task> act = async () => await packageApi.MovePackageIntoProjectAsync(existingProject.Id, existingPackage.Id, setProjectAssociationRequest);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task MovePackageIntoProjectAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var parentPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var setProjectAssociationRequest = new SetProjectAssociationRequest(parentPackage.Id);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.MovePackageIntoProjectAsync(projectId, existingPackage.Id, setProjectAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task MovePackageIntoProjectAsync_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var parentPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var setProjectAssociationRequest = new SetProjectAssociationRequest(parentPackage.Id);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.MovePackageIntoProjectAsync(existingProject.Id, packageId, setProjectAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task MovePackageIntoProjectAsync_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.MovePackageIntoProjectAsync(existingProject.Id, existingPackage.Id, setProjectAssociationRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task MovePackageIntoProjectAsync_InvalidRequestModelParentPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var setProjectAssociationRequest = new SetProjectAssociationRequest(packageId);

            var expectedResponse = new HttpResponseMessage();

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            // Act
            Func<Task> act = async () => await packageApi.MovePackageIntoProjectAsync(existingProject.Id, existingPackage.Id, setProjectAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task MovePackageIntoProjectAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var packageId = Guid.NewGuid().ToString();
            var setProjectAssociationRequest = new SetProjectAssociationRequest(Guid.NewGuid().ToString());

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
            Func<Task> act = async () => await packageApi.MovePackageIntoProjectAsync(projectId, packageId, setProjectAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
