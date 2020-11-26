namespace FactroApiClient.UnitTests.CompanyApi
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
        public async Task CreateCompany_ValidModel_ShouldReturnExpectedCompany()
        {
            // Arrange
            var createCompanyRequest = new CreateCompanyRequest(Guid.NewGuid().ToString());

            var expectedCompany = new CreateCompanyResponse
            {
                Name = createCompanyRequest.Name,
            };

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(expectedCompany, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var companyApi = this.fixture.GetCompanyApi(expectedResponse);

            var createCompanyResponse = default(CreateCompanyResponse);

            // Act
            Func<Task> act = async () => createCompanyResponse = await companyApi.CreateCompanyAsync(createCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createCompanyResponse.Should().BeEquivalentTo(expectedCompany);
        }

        [Fact]
        public async Task CreateCompany_NullModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.CreateCompanyAsync(createCompanyRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateCompanyAsync_NullName_ShouldThrowArgumentNullException()
        {
            // Arrange
            var createCompanyRequest = new CreateCompanyRequest(null);

            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.CreateCompanyAsync(createCompanyRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateCompany_UnsuccessfulRequest_ShouldReturnNull()
        {
            // Arrange
            var createCompanyRequest = new CreateCompanyRequest(Guid.NewGuid().ToString());

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var companyApi = this.fixture.GetCompanyApi(response);

            var createCompanyResponse = new CreateCompanyResponse();

            // Act
            Func<Task> act = async () => createCompanyResponse = await companyApi.CreateCompanyAsync(createCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createCompanyResponse.Should().BeNull();
        }
    }
}
