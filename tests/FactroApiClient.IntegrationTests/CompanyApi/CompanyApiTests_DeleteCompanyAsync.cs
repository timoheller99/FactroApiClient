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
        public async Task DeleteCompanyAsync_ExistingCompany_ShouldDeleteCompany()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompany = await this.fixture.CreateTestCompanyAsync(companyApi);

            var deleteCompanyResponse = new DeleteCompanyResponse();

            // Act
            Func<Task> act = async () => deleteCompanyResponse = await companyApi.DeleteCompanyAsync(existingCompany.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deleteCompanyResponse.Should().BeEquivalentTo(existingCompany);
        }

        [Fact]
        public async Task DeleteCompanyAsync_NotExistingCompany_ShouldThrowFactroApiException()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var deleteCompanyResponse = default(DeleteCompanyResponse);

            // Act
            Func<Task> act = async () => deleteCompanyResponse = await companyApi.DeleteCompanyAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            deleteCompanyResponse.Should().BeNull();
        }
    }
}
