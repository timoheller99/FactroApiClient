namespace FactroApiClient.UnitTests.Company
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Company.Contracts.Basic;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task GetCompanyAsync_ValidId_ShouldReturnExpectedCompany()
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

            var getCompanyByIdResponse = new GetCompanyByIdResponse();

            // Act
            Func<Task> act = async () => getCompanyByIdResponse = await companyApi.GetCompanyByIdAsync(existingCompany.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getCompanyByIdResponse.Id.Should().Be(existingCompany.Id);
        }

        [Theory]
        [MemberData(nameof(CompanyApiTestFixture.InvalidCompanyIds), MemberType = typeof(CompanyApiTestFixture))]
        public async Task GetCompanyAsync_InvalidCompanyId_ShouldThrowArgumentNullException(string companyId)
        {
            // Arrange
            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.GetCompanyByIdAsync(companyId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetCompanyAsync_UnsuccessfulRequest_ShouldReturnNull()
        {
            // Arrange
            var expectedResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var companyApi = this.fixture.GetCompanyApi(expectedResponse);

            var getCompanyByIdResponse = new GetCompanyByIdResponse();

            // Act
            Func<Task> act = async () => getCompanyByIdResponse = await companyApi.GetCompanyByIdAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            getCompanyByIdResponse.Should().BeNull();
        }
    }
}
