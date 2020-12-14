namespace FactroApiClient.UnitTests.CompanyApi
{
    using System;
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
        public async Task CreateCompanyTagAsync_ValidModel_ShouldReturnExpectedCompany()
        {
            // Arrange
            var createCompanyTagRequest = new CreateCompanyTagRequest(Guid.NewGuid().ToString());

            var expectedCompany = new CreateCompanyTagResponse
            {
                Name = createCompanyTagRequest.Name,
            };

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(expectedCompany, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var companyApi = this.fixture.GetCompanyApi(expectedResponse);

            var createCompanyTagResponse = default(CreateCompanyTagResponse);

            // Act
            Func<Task> act = async () => createCompanyTagResponse = await companyApi.CreateCompanyTagAsync(createCompanyTagRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createCompanyTagResponse.Should().BeEquivalentTo(expectedCompany);
        }

        [Fact]
        public async Task CreateCompanyTagAsync_NullModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.CreateCompanyTagAsync(createCompanyTagRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateCompanyTagAsync_NullName_ShouldThrowArgumentNullException()
        {
            // Arrange
            var createCompanyTagRequest = new CreateCompanyTagRequest(null);

            var companyApi = this.fixture.GetCompanyApi();

            // Act
            Func<Task> act = async () => await companyApi.CreateCompanyTagAsync(createCompanyTagRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateCompanyTagAsync_UnsuccessfulRequest_ShouldReturnNull()
        {
            // Arrange
            var createCompanyTagRequest = new CreateCompanyTagRequest(Guid.NewGuid().ToString());

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var companyApi = this.fixture.GetCompanyApi(response);

            var createCompanyTagResponse = new CreateCompanyTagResponse();

            // Act
            Func<Task> act = async () => createCompanyTagResponse = await companyApi.CreateCompanyTagAsync(createCompanyTagRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createCompanyTagResponse.Should().BeNull();
        }
    }
}
