namespace FactroApiClient.UnitTests.PackageApi
{
    using System;
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
        public async Task GrantPackageReadRightsToUserAsync_ValidRequest_ShouldReturnPackageReadRightsForUserInformation()
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

            var existingEmployeeId = Guid.NewGuid().ToString();

            var addPackageReadRightsForUserRequest = new AddPackageReadRightsForUserRequest(existingEmployeeId);

            var expectedPackageReadRight = new AddPackageReadRightsForUserResponse
            {
                EmployeeId = existingEmployeeId,
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedPackageReadRight, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            var addPackageReadRightsForUserResponse = default(AddPackageReadRightsForUserResponse);

            // Act
            Func<Task> act = async () => addPackageReadRightsForUserResponse = await packageApi.GrantPackageReadRightsToUserAsync(existingProject.Id, existingPackage.Id, addPackageReadRightsForUserRequest);

            // Assert
            await act.Should().NotThrowAsync();

            addPackageReadRightsForUserResponse.Should().BeEquivalentTo(expectedPackageReadRight);
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GrantPackageReadRightsToUserAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingEmployeeId = Guid.NewGuid().ToString();

            var addPackageReadRightsForUserRequest = new AddPackageReadRightsForUserRequest(existingEmployeeId);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GrantPackageReadRightsToUserAsync(projectId, existingPackage.Id, addPackageReadRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GrantPackageReadRightsToUserAsync_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingEmployeeId = Guid.NewGuid().ToString();

            var addPackageReadRightsForUserRequest = new AddPackageReadRightsForUserRequest(existingEmployeeId);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GrantPackageReadRightsToUserAsync(existingProject.Id, packageId, addPackageReadRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GrantPackageReadRightsToUserAsync_NullRequestModel_ShouldThrowArgumentNullException()
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
            Func<Task> act = async () => await packageApi.GrantPackageReadRightsToUserAsync(existingProject.Id, existingPackage.Id, addPackageReadRightsForUserRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidEmployeeIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GrantPackageReadRightsToUserAsync_InvalidRequestModelEmployeeId_ShouldThrowArgumentNullException(string employeeId)
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

            var addPackageReadRightsForUserRequest = new AddPackageReadRightsForUserRequest(employeeId);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GrantPackageReadRightsToUserAsync(existingProject.Id, existingPackage.Id, addPackageReadRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GrantPackageReadRightsToUserAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var packageId = Guid.NewGuid().ToString();
            var addPackageReadRightsForUserRequest = new AddPackageReadRightsForUserRequest(Guid.NewGuid().ToString());

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
            Func<Task> act = async () => await packageApi.GrantPackageReadRightsToUserAsync(projectId, packageId, addPackageReadRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
