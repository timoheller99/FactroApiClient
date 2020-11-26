namespace FactroApiClient.UnitTests.CompanyApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        public async Task GetCompaniesAsync_ValidRequest_ShouldReturnCompanies()
        {
            // Arrange
            var existingCompaniesList = new List<GetCompanyPayload>
            {
                new GetCompanyPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
                new GetCompanyPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
            };

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(existingCompaniesList, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var companyApi = this.fixture.GetCompanyApi(expectedResponse);

            var getCompaniesResponse = new List<GetCompanyPayload>();

            // Act
            Func<Task> act = async () => getCompaniesResponse = (await companyApi.GetCompaniesAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getCompaniesResponse.Should().BeEquivalentTo(existingCompaniesList);
        }

        [Fact]
        public async Task GetCompaniesAsync_UnsuccessfulRequest_ShouldReturnNull()
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

            var getCompaniesResponse = new List<GetCompanyPayload>();

            // Act
            Func<Task> act = async () => getCompaniesResponse = (await companyApi.GetCompaniesAsync())?.ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getCompaniesResponse.Should().BeNull();
        }
    }
}
