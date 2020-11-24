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
        public async Task DeleteCompanyAsync_ValidId_ShouldReturnDeletedCompany()
        {
            // Arrange
            var company = new GetCompanyPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedContent =
                new StringContent(JsonConvert.SerializeObject(company, this.fixture.JsonSerializerSettings));

            var response = new HttpResponseMessage
            {
                Content = expectedContent,
            };

            var companyApi = this.fixture.GetCompanyApi(response);

            var companyId = company.Id;

            var deletedCompany = new DeleteCompanyResponse();

            // Act
            Func<Task> act = async () => deletedCompany = await companyApi.DeleteCompanyAsync(companyId);

            // Assert
            await act.Should().NotThrowAsync();

            deletedCompany.Id.Should().Be(company.Id);
        }

        [Theory]
        [MemberData(nameof(CompanyApiTestFixture.InvalidCompanyIds), MemberType = typeof(CompanyApiTestFixture))]
        public async Task DeleteCompanyAsync_InvalidCompanyId_ShouldThrowArgumentNullException(string companyId)
        {
            // Arrange
            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.DeleteCompanyAsync(companyId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteCompanyAsync_UnsuccessfulRequest_ResultShouldBeNull()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var companyApi = this.fixture.GetCompanyApi(response);

            var result = new DeleteCompanyResponse();

            // Act
            Func<Task> act = async () => result = await companyApi.DeleteCompanyAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            result.Should().BeNull();
        }
    }
}
