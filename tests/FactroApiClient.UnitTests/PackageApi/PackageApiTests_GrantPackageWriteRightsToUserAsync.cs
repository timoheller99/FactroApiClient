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
        public async Task GrantPackageWriteRightsToUserAsync_ValidRequest_ShouldReturnPackageWriteRightsForUserInformation()
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

            var addPackageWriteRightsForUserRequest = new AddPackageWriteRightsForUserRequest(existingEmployeeId);

            var expectedPackageWriteRight = new AddPackageWriteRightsForUserResponse
            {
                EmployeeId = existingEmployeeId,
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedPackageWriteRight, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            var addPackageWriteRightsForUserResponse = default(AddPackageWriteRightsForUserResponse);

            // Act
            Func<Task> act = async () => addPackageWriteRightsForUserResponse = await packageApi.GrantPackageWriteRightsToUserAsync(existingProject.Id, existingPackage.Id, addPackageWriteRightsForUserRequest);

            // Assert
            await act.Should().NotThrowAsync();

            addPackageWriteRightsForUserResponse.Should().BeEquivalentTo(expectedPackageWriteRight);
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GrantPackageWriteRightsToUserAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingEmployeeId = Guid.NewGuid().ToString();

            var addPackageWriteRightsForUserRequest = new AddPackageWriteRightsForUserRequest(existingEmployeeId);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GrantPackageWriteRightsToUserAsync(projectId, existingPackage.Id, addPackageWriteRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GrantPackageWriteRightsToUserAsync_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingEmployeeId = Guid.NewGuid().ToString();

            var addPackageWriteRightsForUserRequest = new AddPackageWriteRightsForUserRequest(existingEmployeeId);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GrantPackageWriteRightsToUserAsync(existingProject.Id, packageId, addPackageWriteRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GrantPackageWriteRightsToUserAsync_NullRequestModel_ShouldThrowArgumentNullException()
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
            Func<Task> act = async () => await packageApi.GrantPackageWriteRightsToUserAsync(existingProject.Id, existingPackage.Id, addPackageWriteRightsForUserRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidEmployeeIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GrantPackageWriteRightsToUserAsync_InvalidRequestModelEmployeeId_ShouldThrowArgumentNullException(string employeeId)
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

            var addPackageWriteRightsForUserRequest = new AddPackageWriteRightsForUserRequest(employeeId);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GrantPackageWriteRightsToUserAsync(existingProject.Id, existingPackage.Id, addPackageWriteRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GrantPackageWriteRightsToUserAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var packageId = Guid.NewGuid().ToString();
            var addPackageWriteRightsForUserRequest = new AddPackageWriteRightsForUserRequest(Guid.NewGuid().ToString());

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
            Func<Task> act = async () => await packageApi.GrantPackageWriteRightsToUserAsync(projectId, packageId, addPackageWriteRightsForUserRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
