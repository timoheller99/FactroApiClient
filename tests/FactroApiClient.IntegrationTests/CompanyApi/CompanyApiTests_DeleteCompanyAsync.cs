namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts;
    using FactroApiClient.Company.Contracts.Basic;

    using FluentAssertions;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task DeleteCompanyAsync_ExistingCompany_ShouldDeleteCompany()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var name = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createCompanyRequest = new CreateCompanyRequest(name);
            var existingCompany = await companyApi.CreateCompanyAsync(createCompanyRequest);

            var deletedCompany = new DeleteCompanyResponse();

            // Act
            Func<Task> act = async () => deletedCompany = await companyApi.DeleteCompanyAsync(existingCompany.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deletedCompany.Should().BeEquivalentTo(existingCompany);
        }

        [Fact]
        public async Task DeleteCompanyAsync_NotExistingCompany_ShouldDeleteCompany()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var deletedCompany = new DeleteCompanyResponse();

            // Act
            Func<Task> act = async () => deletedCompany = await companyApi.DeleteCompanyAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            deletedCompany.Should().BeNull();
        }
    }
}
