namespace FactroApiClient.UnitTests.ProjectApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.Association;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class ProjectApiTests
    {
        [Fact]
        public async Task SetProjectCompanyAsync_ValidRequest_ShouldReturnVoid()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var companyId = Guid.NewGuid().ToString();
            var setProjectCompanyAssociationRequest = new SetProjectCompanyAssociationRequest(companyId);

            var expectedResponse = new HttpResponseMessage();

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            // Act
            Func<Task> act = async () => await projectApi.SetProjectCompanyAsync(existingProject.Id, setProjectCompanyAssociationRequest);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [MemberData(nameof(ProjectApiTestFixture.InvalidProjectIds), MemberType = typeof(ProjectApiTestFixture))]
        public async Task SetProjectCompanyAsync_InvalidProjectId_ShouldThrowArgumentNullException(string projectId)
        {
            // Arrange
            var companyId = Guid.NewGuid().ToString();
            var setProjectCompanyAssociationRequest = new SetProjectCompanyAssociationRequest(companyId);

            var expectedResponse = new HttpResponseMessage();

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            // Act
            Func<Task> act = async () => await projectApi.SetProjectCompanyAsync(projectId, setProjectCompanyAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task SetProjectCompanyAsync_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedResponse = new HttpResponseMessage();

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            // Act
            Func<Task> act = async () => await projectApi.SetProjectCompanyAsync(existingProject.Id, setProjectCompanyAssociationRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task SetProjectCompanyAsync_NullRequestModelCompanyId_ShouldThrowArgumentNullException()
        {
            // Arrange
            var existingProject = new GetProjectPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var setProjectCompanyAssociationRequest = new SetProjectCompanyAssociationRequest(companyId: null);

            var expectedResponse = new HttpResponseMessage();

            var projectApi = this.fixture.GetProjectApi(expectedResponse);

            // Act
            Func<Task> act = async () => await projectApi.SetProjectCompanyAsync(existingProject.Id, setProjectCompanyAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task SetProjectCompanyAsync_BadRequest_ShouldThrowProjectApiException()
        {
            // Arrange
            var projectId = Guid.NewGuid().ToString();
            var companyId = Guid.NewGuid().ToString();
            var setProjectCompanyAssociationRequest = new SetProjectCompanyAssociationRequest(companyId);

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
            Func<Task> act = async () => await projectApi.SetProjectCompanyAsync(projectId, setProjectCompanyAssociationRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
