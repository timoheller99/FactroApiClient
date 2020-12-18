namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.Project.Contracts.Tag;

    using FluentAssertions;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task AddTagToProjectAsync_ValidRequest_ShouldReturnVoid()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingProjectTag = new GetProjectTagPayload
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var addProjectTagAssociationRequest = new AddProjectTagAssociationRequest(existingProjectTag.Name);

            var expectedResponse = new HttpResponseMessage();

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            // Act
            Func<Task> act = async () => await projectApi.AddTagToProjectAsync(existingProject.Id, addProjectTagAssociationRequest);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task AddTagToProjectAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var existingProjectTag = new GetProjectTagPayload
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var addProjectTagAssociationRequest = new AddProjectTagAssociationRequest(existingProjectTag.Name);

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.AddTagToProjectAsync(projectId, addProjectTagAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectTagIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task AddTagToProjectAsync_InvalidTagId_ShouldThrowArgumentNullException(string tagId)
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var addProjectTagAssociationRequest = new AddProjectTagAssociationRequest(tagId);

            var projectApi = this.fixture.GetProjectApi();

            // Act
            Func<Task> act = async () => await projectApi.AddTagToProjectAsync(existingProject.Id, addProjectTagAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task AddTagToProjectAsync_BadRequest_ShouldThrowProjectApiException()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var existingProjectTag = new GetProjectTagPayload
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var addProjectTagAssociationRequest = new AddProjectTagAssociationRequest(existingProjectTag.Name);

            var expectedResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            // Act
            Func<Task> act = async () => await projectApi.AddTagToProjectAsync(existingProject.Id, addProjectTagAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
