namespace FactroApiClient.UnitTests.PackageApi
{
    using System;
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
        public async Task CreatePackageCommentAsync_ValidRequest_ShouldReturnCreatedPackageComment()
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

            var text = Guid.NewGuid().ToString();

            var createPackageCommentRequest = new CreatePackageCommentRequest(text);

            var expectedPackageComment = new CreatePackageCommentResponse
            {
                Text = text,
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedPackageComment, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            var createPackageCommentResponse = default(CreatePackageCommentResponse);

            // Act
            Func<Task> act = async () => createPackageCommentResponse = await packageApi.CreatePackageCommentAsync(existingProject.Id, existingPackage.Id, createPackageCommentRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createPackageCommentResponse.Should().BeEquivalentTo(expectedPackageComment);
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task CreatePackageCommentAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingPackage = new GetPackagePayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var text = Guid.NewGuid().ToString();

            var createPackageCommentRequest = new CreatePackageCommentRequest(text);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.CreatePackageCommentAsync(projectId, existingPackage.Id, createPackageCommentRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task CreatePackageCommentAsync_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var text = Guid.NewGuid().ToString();

            var createPackageCommentRequest = new CreatePackageCommentRequest(text);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.CreatePackageCommentAsync(existingProject.Id, packageId, createPackageCommentRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreatePackageCommentAsync_NullRequestModel_ShouldThrowArgumentNullException()
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
            Func<Task> act = async () => await packageApi.CreatePackageCommentAsync(existingProject.Id, existingPackage.Id, createPackageCommentRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreatePackageCommentAsync_NullRequestModelText_ShouldThrowArgumentNullException()
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

            var createPackageCommentRequest = new CreatePackageCommentRequest(text: null);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.CreatePackageCommentAsync(existingProject.Id, existingPackage.Id, createPackageCommentRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreatePackageCommentAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var packageId = Guid.NewGuid().ToString();

            var createPackageCommentRequest = new CreatePackageCommentRequest(Guid.NewGuid().ToString());

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
            Func<Task> act = async () => await packageApi.CreatePackageCommentAsync(projectId, packageId, createPackageCommentRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
