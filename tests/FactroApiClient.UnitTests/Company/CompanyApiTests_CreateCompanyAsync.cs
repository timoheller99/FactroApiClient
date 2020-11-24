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
        public async Task CreateCompany_ValidModel_ShouldReceiveExpectedCompany()
        {
            // Arrange
            var companyToBeCreated = new CreateCompanyRequest(Guid.NewGuid().ToString());
            var expectedCompany = new CreateCompanyResponse
            {
                Name = companyToBeCreated.Name,
            };
            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(expectedCompany, this.fixture.JsonSerializerSettings));
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = expectedResponseContent,
            };
            var companyApi = this.fixture.GetCompanyApi(response);

            var createdCompany = default(CreateCompanyResponse);

            // Act
            Func<Task> act = async () => createdCompany = await companyApi.CreateCompanyAsync(companyToBeCreated);

            // Assert
            await act.Should().NotThrowAsync();

            createdCompany.Should().BeEquivalentTo(expectedCompany);
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
            var companyToBeCreated = new CreateCompanyRequest(null);

            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.CreateCompanyAsync(companyToBeCreated);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateCompany_UnsuccessfulRequest_ResultShouldBeNull()
        {
            // Arrange
            var companyToBeCreated = new CreateCompanyRequest(Guid.NewGuid().ToString());

            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var companyApi = this.fixture.GetCompanyApi(response);

            var result = new CreateCompanyResponse();

            // Act
            Func<Task> act = async () => result = await companyApi.CreateCompanyAsync(companyToBeCreated);

            // Assert
            await act.Should().NotThrowAsync();

            result.Should().BeNull();
        }
    }
}
