namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts.Basic;

    using FluentAssertions;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task GetCompanyAsync_ExistingCompany_ShouldReturnExpectedCompany()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var name = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createCompanyRequest = new CreateCompanyRequest(name);

            var existingCompany = await companyApi.CreateCompanyAsync(createCompanyRequest);

            var getCompanyByIdResponse = new GetCompanyByIdResponse();

            // Act
            Func<Task> act = async () => getCompanyByIdResponse = await companyApi.GetCompanyByIdAsync(existingCompany.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getCompanyByIdResponse.Should().BeEquivalentTo(existingCompany);
        }

        [Fact]
        public async Task GetCompanyAsync_NotExistingCompany_ShouldReturnNull()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var getCompanyByIdResponse = new GetCompanyByIdResponse();

            // Act
            Func<Task> act = async () => getCompanyByIdResponse = await companyApi.GetCompanyByIdAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            getCompanyByIdResponse.Should().BeNull();
        }
    }
}
