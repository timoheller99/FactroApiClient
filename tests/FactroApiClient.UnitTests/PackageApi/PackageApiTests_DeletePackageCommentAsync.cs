namespace FactroApiClient.UnitTests.PackageApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Package.Contracts;
    using FactroApiClient.Package.Contracts.Comment;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class PackageApiTests
    {
        [Fact]
        public async Task DeletePackageCommentAsync_ValidRequest_ShouldReturnDeletedPackageComment()
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
            };

            var expectedPackageComment =
                existingPackageComments.First(comment => comment.TaskPackageId == existingPackage.Id);

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedPackageComment, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var packageApi = this.fixture.GetPackageApi(expectedResponse);

            var deletePackageCommentResponse = default(DeletePackageCommentResponse);

            // Act
            Func<Task> act = async () => deletePackageCommentResponse = await packageApi.DeletePackageCommentAsync(existingProject.Id, existingPackage.Id, expectedPackageComment.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deletePackageCommentResponse.Should().BeEquivalentTo(expectedPackageComment);
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidProjectIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task DeletePackageCommentAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
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
            };

            var expectedPackageComment =
                existingPackageComments.First(comment => comment.TaskPackageId == existingPackage.Id);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.DeletePackageCommentAsync(projectId, existingPackage.Id, expectedPackageComment.Id);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task DeletePackageCommentAsync_InvalidPackageId_ShouldThrowArgumentNullException(string packageId)
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
            };

            var expectedPackageComment =
                existingPackageComments.First(comment => comment.TaskPackageId == existingPackage.Id);

            var packageApi = this.fixture.GetPackageApi();

            // Act
            Func<Task> act = async () => await packageApi.DeletePackageCommentAsync(existingProject.Id, packageId, expectedPackageComment.Id);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(PackageApiTestFixture.InvalidPackageCommentIds), MemberType = typeof(PackageApiTestFixture))]
        public async Task DeletePackageCommentAsync_InvalidCommentId_ShouldThrowArgumentNullException(string packageCommentId)
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
            Func<Task> act = async () => await packageApi.DeletePackageCommentAsync(existingProject.Id, existingPackage.Id, packageCommentId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeletePackageCommentAsync_UnsuccessfulRequest_ShouldThrowFactroApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var packageId = Guid.NewGuid().ToString();
            var commentId = Guid.NewGuid().ToString();

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
            Func<Task> act = async () => await packageApi.DeletePackageCommentAsync(projectId, packageId, commentId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
