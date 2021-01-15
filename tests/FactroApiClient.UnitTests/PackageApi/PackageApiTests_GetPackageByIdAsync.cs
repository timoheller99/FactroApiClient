namespace FactroApiClient.UnitTests.PackageApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        public async Task GetPackageByIdAsyncOneParameter_ValidRequest_ShouldReturnExpectedPackage()
        {
            // Arrange
            var existingPackages = new List<GetPackagePayload>
            {
                new GetPackagePayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
                new GetPackagePayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
            };

            var expectedPackage = existingPackages.First();

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedPackage, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            var getPackageByIdResponse = default(GetPackageByIdResponse);

            // Act
            Func<Task> act = async () => getPackageByIdResponse = await packageApi.GetPackageByIdAsync(expectedPackage.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getPackageByIdResponse.Should().BeEquivalentTo(expectedPackage);
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GetPackageByIdAsyncOneParameter_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GetPackageByIdAsync(packageId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetPackageByIdAsyncOneParameter_UnsuccessfulRequest_ShouldThrowFactroApiException()
        {
            // Arrange
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
            Func<Task> act = async () => await packageApi.GetPackageByIdAsync(packageId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }

        [Fact]
        public async Task GetPackageByIdAsyncTwoParameters_ValidRequest_ShouldReturnExpectedPackage()
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
                },
            };

            var expectedPackage = existingPackages.Single(package => package.ProjectId == existingProject.Id);

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedPackage, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            var getPackageByIdResponse = default(GetPackageByIdResponse);

            // Act
            Func<Task> act = async () => getPackageByIdResponse = await packageApi.GetPackageByIdAsync(existingProject.Id, expectedPackage.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getPackageByIdResponse.Should().BeEquivalentTo(expectedPackage);
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GetPackageByIdAsyncTwoParameters_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };
            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GetPackageByIdAsync(projectId, existingPackage.Id);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GetPackageByIdAsyncTwoParameters_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };
            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GetPackageByIdAsync(existingProject.Id, packageId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetPackageByIdAsyncTwoParameters_UnsuccessfulRequest_ShouldThrowFactroApiException()
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
            Func<Task> act = async () => await packageApi.GetPackageByIdAsync(projectId, packageId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
