namespace FactroApiClient.UnitTests.CompanyApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Company.Contracts.CompanyTag;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task GetCompanyTagsByIdAsync_ValidRequest_ShouldReturnCompanies()
        {
            // Arrange
            var existingCompaniesList = new List<GetCompanyTagAssociationPayload>
            {
                new GetCompanyTagAssociationPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
                new GetCompanyTagAssociationPayload
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

            var getCompanyTagsResponse = new List<GetCompanyTagAssociationPayload>();

            // Act
            Func<Task> act = async () => getCompanyTagsResponse = (await companyApi.GetCompanyTagsByIdAsync(Guid.NewGuid().ToString())).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getCompanyTagsResponse.Should().BeEquivalentTo(existingCompaniesList);
        }

        [Theory]
        [MemberData(nameof(CompanyApiTestFixture.InvalidCompanyIds), MemberType = typeof(CompanyApiTestFixture))]
        public async Task GetCompanyTagsByIdAsync_InvalidCompanyId_ShouldThrowArgumentNullException(string companyId)
        {
            // Arrange
            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.GetCompanyTagsByIdAsync(companyId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetCompanyTagsByIdAsync_UnsuccessfulRequest_ShouldReturnNull()
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

            var getCompanyTagsResponse = new List<GetCompanyTagAssociationPayload>();

            // Act
            Func<Task> act = async () => getCompanyTagsResponse = (await companyApi.GetCompanyTagsByIdAsync(Guid.NewGuid().ToString()))?.ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getCompanyTagsResponse.Should().BeNull();
        }
    }
}
