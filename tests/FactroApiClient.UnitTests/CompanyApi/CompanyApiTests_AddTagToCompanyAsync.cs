namespace FactroApiClient.UnitTests.CompanyApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Company.Contracts.CompanyTag;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task AddTagToCompanyAsync_ValidRequest_ShouldNotThrow()
        {
            // Arrange
            var tagId = Guid.NewGuid().ToString();
            var addCompanyTagRequest = new AddCompanyTagAssociationRequest(tagId);
            var companyId = Guid.NewGuid().ToString();

            var expectedResponse = new HttpResponseMessage();
            var companyApi = this.fixture.GetCompanyApi(expectedResponse);

            // Act
            Func<Task> act = async () => await companyApi.AddTagToCompanyAsync(companyId, addCompanyTagRequest);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [MemberData(nameof(CompanyApiTestFixture.InvalidCompanyIds), MemberType = typeof(CompanyApiTestFixture))]
        public async Task AddTagToCompanyAsync_InvalidCompanyId_ShouldThrowArgumentNullException(string companyId)
        {
            // Arrange
            var tagId = Guid.NewGuid().ToString();
            var addCompanyTagRequest = new AddCompanyTagAssociationRequest(tagId);

            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.AddTagToCompanyAsync(companyId, addCompanyTagRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddTagToCompanyAsync_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var companyId = Guid.NewGuid().ToString();

            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.AddTagToCompanyAsync(companyId, addCompanyTagAssociationRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddTagToCompanyAsync_NullRequestModelTagId_ShouldThrowArgumentNullException()
        {
            // Arrange
            var companyId = Guid.NewGuid().ToString();
            var addCompanyTagRequest = new AddCompanyTagAssociationRequest(tagId: null);

            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.AddTagToCompanyAsync(companyId, addCompanyTagRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddTagToCompanyAsync_UnsuccessfulRequest_ShouldThrowCompanyApiException()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var companyApi = this.fixture.GetCompanyApi(response);

            // Act
            Func<Task> act = async () => await companyApi.AddTagToCompanyAsync(
                Guid.NewGuid().ToString(),
                new AddCompanyTagAssociationRequest(Guid.NewGuid().ToString()));

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
