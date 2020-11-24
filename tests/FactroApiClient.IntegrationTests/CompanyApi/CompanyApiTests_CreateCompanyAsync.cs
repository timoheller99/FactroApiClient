namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts;
    using FactroApiClient.Company.Contracts.Basic;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task CreateCompanyAsync_ValidCompany_ShouldStoreCompany()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var name = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createCompanyRequest = new CreateCompanyRequest(name);

            var createCompanyResponse = default(CreateCompanyResponse);

            // Act
            Func<Task> act = async () => createCompanyResponse = await companyApi.CreateCompanyAsync(createCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var companies = (await companyApi.GetCompaniesAsync())
                    .Where(x => x.Name.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                companies.Should().ContainEquivalentOf(createCompanyResponse);
            }
        }

        [Fact]
        public async Task CreateCompanyAsync_TwoIdenticalCompanies_ShouldStoreBothCompanies()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var name = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createCompanyRequest = new CreateCompanyRequest(name);

            await companyApi.CreateCompanyAsync(createCompanyRequest);

            // Act
            Func<Task> act = async () => await companyApi.CreateCompanyAsync(createCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var companies = (await companyApi.GetCompaniesAsync())
                    .Where(x => x.Name.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                var matchingCompanies = companies.Where(x => x.Name == name);
                matchingCompanies.Should().HaveCount(2);
            }
        }
    }
}
