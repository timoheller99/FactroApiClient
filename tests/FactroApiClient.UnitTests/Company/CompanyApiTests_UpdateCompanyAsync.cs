namespace FactroApiClient.UnitTests.Company
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

            var expectedContent = new StringContent(JsonConvert.SerializeObject(expectedUpdatedCompany, this.fixture.JsonSerializerSettings));
            var response = new HttpResponseMessage
            {
                Content = expectedContent,
            };
            var companyApi = this.fixture.GetCompanyApi(response);

            var updatedCompany = new UpdateCompanyResponse();

            // Act
            Func<Task> act = async () =>
                updatedCompany = await companyApi.UpdateCompanyAsync(existingCompany.Id, updateCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                updatedCompany.Id.Should().Be(existingCompany.Id);
                updatedCompany.Name.Should().Be(expectedUpdatedCompany.Name);
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
        public async Task UpdateCompanyAsync_UnsuccessfulRequest_ResultShouldBeNull()
        {
            // Arrange
            var companyId = Guid.NewGuid().ToString();
            var updateCompanyRequest = new UpdateCompanyRequest();

            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var companyApi = this.fixture.GetCompanyApi(response);

            var result = new UpdateCompanyResponse();

            // Act
            Func<Task> act = async () => result = await companyApi.UpdateCompanyAsync(companyId, updateCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            result.Should().BeNull();
        }
    }
}
