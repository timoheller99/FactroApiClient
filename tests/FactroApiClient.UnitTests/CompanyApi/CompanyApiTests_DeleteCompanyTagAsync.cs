namespace FactroApiClient.UnitTests.CompanyApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Company.Contracts.Basic;
    using FactroApiClient.Company.Contracts.CompanyTag;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task DeleteCompanyTagAsync_ValidId_ShouldReturnDeletedCompany()
        {
            // Arrange
            var existingCompany = new GetCompanyPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(existingCompany, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var companyApi = this.fixture.GetCompanyApi(expectedResponse);

            var deleteCompanyTagResponse = new DeleteCompanyTagResponse();

            // Act
            Func<Task> act = async () => deleteCompanyTagResponse = await companyApi.DeleteCompanyTagAsync(existingCompany.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deleteCompanyTagResponse.Id.Should().Be(existingCompany.Id);
        }

        [Theory]
        [MemberData(nameof(CompanyApiTestFixture.InvalidCompanyTagIds), MemberType = typeof(CompanyApiTestFixture))]
        public async Task DeleteCompanyTagAsync_InvalidCompanyId_ShouldThrowArgumentNullException(string companyId)
        {
            // Arrange
            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.DeleteCompanyTagAsync(companyId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task DeleteCompanyTagAsync_UnsuccessfulRequest_ShouldThrowCompanyApiException()
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
            Func<Task> act = async () => await companyApi.DeleteCompanyTagAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
