namespace FactroApiClient.UnitTests.CompanyApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Company.Contracts.CompanyTag;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task GetCompanyTagsAsync_ValidRequest_ShouldReturnCompanies()
        {
            // Arrange
            var existingCompaniesList = new List<GetCompanyTagPayload>
            {
                new GetCompanyTagPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
                new GetCompanyTagPayload
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

            var getCompanyTagsResponse = new List<GetCompanyTagPayload>();

            // Act
            Func<Task> act = async () => getCompanyTagsResponse = (await companyApi.GetCompanyTagsAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getCompanyTagsResponse.Should().BeEquivalentTo(existingCompaniesList);
        }

        [Fact]
        public async Task GetCompanyTagsAsync_UnsuccessfulRequest_ShouldThrowCompanyApiException()
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
            Func<Task> act = async () => await companyApi.GetCompanyTagsAsync();

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
