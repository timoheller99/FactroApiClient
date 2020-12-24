namespace FactroApiClient.UnitTests.PackageApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Company.Contracts.Basic;
    using FactroApiClient.Package.Contracts;
    using FactroApiClient.Package.Contracts.Association;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class PackageApiTests
    {
        [Fact]
        public async Task SetPackageCompanyAsync_ValidRequest_ShouldReturnVoid()
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

            var existingCompany = new GetCompanyPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var setCompanyAssociationRequest = new SetCompanyAssociationRequest(existingCompany.Id);

            var expectedResponseMessage = new HttpResponseMessage();

            var packageApi = this.fixture.GetPackageApi(expectedResponseMessage);

            // Act
            Func<Task> act = async () => await packageApi.SetPackageCompanyAsync(existingProject.Id, existingPackage.Id, setCompanyAssociationRequest);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task SetPackageCompanyAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
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

            var existingCompany = new GetCompanyPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var setCompanyAssociationRequest = new SetCompanyAssociationRequest(existingCompany.Id);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.SetPackageCompanyAsync(projectId, existingPackage.Id, setCompanyAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task SetPackageCompanyAsync_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingCompany = new GetCompanyPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var setCompanyAssociationRequest = new SetCompanyAssociationRequest(existingCompany.Id);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.SetPackageCompanyAsync(existingProject.Id, packageId, setCompanyAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task SetPackageCompanyAsync_NullRequestModel_ShouldThrowArgumentNullException()
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
            Func<Task> act = async () => await packageApi.SetPackageCompanyAsync(existingProject.Id, existingPackage.Id, setCompanyAssociationRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task SetPackageCompanyAsync_NullRequestModelCompanyId_ShouldThrowArgumentNullException()
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

            var setCompanyAssociationRequest = new SetCompanyAssociationRequest(companyId: null);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.SetPackageCompanyAsync(existingProject.Id, existingPackage.Id, setCompanyAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task SetPackageCompanyAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var packageId = Guid.NewGuid().ToString();
            var setCompanyAssociationRequest = new SetCompanyAssociationRequest(Guid.NewGuid().ToString());

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
            Func<Task> act = async () => await packageApi.SetPackageCompanyAsync(projectId, packageId, setCompanyAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
