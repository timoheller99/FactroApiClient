namespace FactroApiClient.UnitTests.CompanyApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task RemoveCompanyTagAsync_ValidRequest_ShouldNotThrow()
        {
            // Arrange
            var tagId = Guid.NewGuid().ToString();
            var companyId = Guid.NewGuid().ToString();

            var expectedResponse = new HttpResponseMessage();
            var companyApi = this.fixture.GetCompanyApi(expectedResponse);

            // Act
            Func<Task> act = async () => await companyApi.RemoveCompanyTagAsync(companyId, tagId);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [MemberData(nameof(CompanyApiTestFixture.InvalidCompanyIds), MemberType = typeof(CompanyApiTestFixture))]
        public async Task RemoveCompanyTagAsync_InvalidCompanyId_ShouldThrowArgumentNullException(string companyId)
        {
            // Arrange
            var tagId = Guid.NewGuid().ToString();

            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.RemoveCompanyTagAsync(companyId, tagId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(CompanyApiTestFixture.InvalidCompanyTagIds), MemberType = typeof(CompanyApiTestFixture))]
        public async Task RemoveCompanyTagAsync_InvalidTagId_ShouldThrowArgumentNullException(string tagId)
        {
            // Arrange
            var companyId = Guid.NewGuid().ToString();

            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.RemoveCompanyTagAsync(companyId, tagId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task RemoveCompanyTagAsync_UnsuccessfulRequest_ShouldReturnNull()
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
            Func<Task> act = async () => await companyApi.RemoveCompanyTagAsync(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();
        }
    }
}
