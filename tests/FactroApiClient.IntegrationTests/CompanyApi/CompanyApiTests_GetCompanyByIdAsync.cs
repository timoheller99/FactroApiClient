namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts.Basic;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task GetCompanyAsync_ExistingCompany_ShouldReturnExpectedCompany()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompany = await this.fixture.CreateTestCompanyAsync(companyApi);

            var getCompanyByIdResponse = new GetCompanyByIdResponse();

            // Act
            Func<Task> act = async () => getCompanyByIdResponse = await companyApi.GetCompanyByIdAsync(existingCompany.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getCompanyByIdResponse.Should().BeEquivalentTo(existingCompany);
        }

        [Fact]
        public async Task GetCompanyAsync_NotExistingCompany_ShouldThrowFactroApiException()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var getCompanyByIdResponse = default(GetCompanyByIdResponse);

            // Act
            Func<Task> act = async () => getCompanyByIdResponse = await companyApi.GetCompanyByIdAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            getCompanyByIdResponse.Should().BeNull();
        }
    }
}
