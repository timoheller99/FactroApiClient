namespace FactroApiClient.UnitTests.PackageApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Contact.Contracts;
    using FactroApiClient.Package.Contracts;
    using FactroApiClient.Package.Contracts.Association;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class PackageApiTests
    {
        [Fact]
        public async Task SetPackageContactAsync_ValidRequest_ShouldReturnVoid()
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

            var existingContact = new GetContactPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var setContactAssociationRequest = new SetContactAssociationRequest(existingContact.Id);

            var expectedResponseMessage = new HttpResponseMessage();

            var packageApi = this.fixture.GetPackageApi(expectedResponseMessage);

            // Act
            Func<Task> act = async () => await packageApi.SetPackageContactAsync(existingProject.Id, existingPackage.Id, setContactAssociationRequest);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task SetPackageContactAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
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

            var existingContact = new GetContactPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var setContactAssociationRequest = new SetContactAssociationRequest(existingContact.Id);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.SetPackageContactAsync(projectId, existingPackage.Id, setContactAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task SetPackageContactAsync_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingContact = new GetContactPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var setContactAssociationRequest = new SetContactAssociationRequest(existingContact.Id);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.SetPackageContactAsync(existingProject.Id, packageId, setContactAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task SetPackageContactAsync_NullRequestModel_ShouldThrowArgumentNullException()
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
            Func<Task> act = async () => await packageApi.SetPackageContactAsync(existingProject.Id, existingPackage.Id, setContactAssociationRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task SetPackageContactAsync_NullRequestModelContactId_ShouldThrowArgumentNullException()
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

            var setContactAssociationRequest = new SetContactAssociationRequest(contactId: null);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.SetPackageContactAsync(existingProject.Id, existingPackage.Id, setContactAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task SetPackageContactAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var packageId = Guid.NewGuid().ToString();
            var setContactAssociationRequest = new SetContactAssociationRequest(Guid.NewGuid().ToString());

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
            Func<Task> act = async () => await packageApi.SetPackageContactAsync(projectId, packageId, setContactAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
