namespace FactroApiClient.UnitTests.Company
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
        public async Task GetCompaniesAsync_ValidRequest_ShouldReturnCompanyList()
        {
            // Arrange
            var companyList = new List<GetCompanyPayload>
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

            var expectedContent = new StringContent(JsonConvert.SerializeObject(companyList, this.fixture.JsonSerializerSettings));

            var response = new HttpResponseMessage
            {
                Content = expectedContent,
            };

            var companyApi = this.fixture.GetCompanyApi(response);

            var result = new List<GetCompanyPayload>();

            // Act
            Func<Task> act = async () => result = (await companyApi.GetCompaniesAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            result.Should().HaveCount(companyList.Count);
        }

        [Fact]
        public async Task GetCompaniesAsync_UnsuccessfulRequest_ResultShouldBeNull()
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

            var result = new List<GetCompanyPayload>();

            // Act
            Func<Task> act = async () => result = (await companyApi.GetCompaniesAsync())?.ToList();

            // Assert
            await act.Should().NotThrowAsync();

            result.Should().BeNull();
        }
    }
}
