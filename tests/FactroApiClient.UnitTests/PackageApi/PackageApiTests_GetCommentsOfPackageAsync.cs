namespace FactroApiClient.UnitTests.PackageApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Package.Contracts.Base;
    using FactroApiClient.Package.Contracts.Comment;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class PackageApiTests
    {
        [Fact]
        public async Task GetCommentsOfPackageAsync_ValidRequest_ShouldReturnCommentsOfPackage()
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

            var existingPackageComments = new List<GetPackageCommentPayload>
            {
                new GetPackageCommentPayload
                {
                    Id = Guid.NewGuid().ToString(),
                    TaskPackageId = existingPackage.Id,
                },
                new GetPackageCommentPayload
                {
                    Id = Guid.NewGuid().ToString(),
                    TaskPackageId = existingPackage.Id,
                },
            };

            var expectedPackageComments = existingPackageComments.Where(comment => comment.TaskPackageId == existingPackage.Id);

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(expectedPackageComments, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            var getCommentsOfPackageResponse = default(IEnumerable<GetPackageCommentPayload>);

            // Act
            Func<Task> act = async () => getCommentsOfPackageResponse = await packageApi.GetCommentsOfPackageAsync(existingProject.Id, existingPackage.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getCommentsOfPackageResponse.Should().BeEquivalentTo(existingPackageComments);
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GetCommentsOfPackageAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GetCommentsOfPackageAsync(projectId, existingPackage.Id);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task GetCommentsOfPackageAsync_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.GetCommentsOfPackageAsync(existingProject.Id, packageId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetCommentsOfPackageAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
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
            Func<Task> act = async () => await packageApi.GetCommentsOfPackageAsync(projectId, packageId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
