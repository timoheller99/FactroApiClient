namespace FactroApiClient.UnitTests.PackageApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Package.Contracts.AccessRights;
    using FactroApiClient.Package.Contracts.Base;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class PackageApiTests
    {
        [Fact]
        public async Task GetPackageReadRightsAsync_ValidRequest_ShouldReturnPackageReadRightsInformation()
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

            var existingReadRights = new List<GetPackageReadRightsResponse>
            {
                new GetPackageReadRightsResponse
                {
                    AccessRights = new List<AccessRightReason>
                    {
                        new AccessRightReason
                        {
                            PackageId = existingPackage.Id,
                            ProjectId = existingProject.Id,
                            Reason = AccessReason.IsProjectOfficer,
                            TaskId = Guid.NewGuid().ToString(),
                        },
                    },
                    EmployeeId = Guid.NewGuid().ToString(),
                },
            };

            var expectedPackageReadRights = existingReadRights
                .Where(element => element.AccessRights.Any(right => right.PackageId == existingPackage.Id));

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedPackageReadRights, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            var getPackageReadRightsResponse = default(IEnumerable<GetPackageReadRightsResponse>);

            // Act
            Func<Task> act = async () => getPackageReadRightsResponse = await packageApi.GetPackageReadRightsAsync(existingProject.Id, existingPackage.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getPackageReadRightsResponse.Should().BeEquivalentTo(expectedPackageReadRights);
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GetPackageReadRightsAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GetPackageReadRightsAsync(projectId, existingPackage.Id);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GetPackageReadRightsAsync_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GetPackageReadRightsAsync(existingProject.Id, packageId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetPackageReadRightsAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
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
            Func<Task> act = async () => await packageApi.GetPackageReadRightsAsync(projectId, packageId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
