namespace FactroApiClient.UnitTests.CompanyApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Company.Contracts.Basic;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Newtonsoft.Json;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task UpdateCompanyAsync_ValidRequest_ShouldReturnUpdatedCompany()
        {
            // Arrange
            var existingCompany = new GetCompanyPayload
            {
                Id = Guid.NewGuid().ToString(),
                Name = "TestName",
            };

            var updateCompanyRequest = new UpdateCompanyRequest
            {
                Name = "NewName",
            };

            var expectedUpdatedCompany = new UpdateCompanyResponse
            {
                Id = existingCompany.Id,
                Name = updateCompanyRequest.Name,
            };

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(expectedUpdatedCompany, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var companyApi = this.fixture.GetCompanyApi(expectedResponse);

            var updateCompanyResponse = new UpdateCompanyResponse();

            // Act
            Func<Task> act = async () =>
                updateCompanyResponse = await companyApi.UpdateCompanyAsync(existingCompany.Id, updateCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                updateCompanyResponse.Id.Should().Be(existingCompany.Id);
                updateCompanyResponse.Name.Should().Be(expectedUpdatedCompany.Name);
            }
        }

        [Theory]
        [MemberData(nameof(CompanyApiTestFixture.InvalidCompanyIds), MemberType = typeof(CompanyApiTestFixture))]
        public async Task UpdateCompanyAsync_InvalidCompanyId_ShouldThrowArgumentNullException(string companyId)
        {
            // Arrange
            var companyApi = this.fixture.GetCompanyApi();

            var updateCompanyRequest = new UpdateCompanyRequest();

            // Act
            Func<Task> act = async () => await companyApi.UpdateCompanyAsync(companyId, updateCompanyRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateCompanyAsync_UnsuccessfulRequest_ShouldReturnNull()
        {
            // Arrange
            var companyId = Guid.NewGuid().ToString();
            var updateCompanyRequest = new UpdateCompanyRequest();

            var expectedResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var companyApi = this.fixture.GetCompanyApi(expectedResponse);

            var updateCompanyResponse = new UpdateCompanyResponse();

            // Act
            Func<Task> act = async () => updateCompanyResponse = await companyApi.UpdateCompanyAsync(companyId, updateCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            updateCompanyResponse.Should().BeNull();
        }
    }
}
