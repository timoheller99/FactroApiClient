namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
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
            await this.fixture.ClearFactroInstanceAsync();

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

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task CreateCompanyAsync_TwoCompaniesWithSameName_ShouldStoreBothCompanies()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var name = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createCompanyRequest = new CreateCompanyRequest(name);

            var firstCreatedCompany = await companyApi.CreateCompanyAsync(createCompanyRequest);

            var secondCreatedCompany = default(CreateCompanyResponse);

            // Act
            Func<Task> act = async () => secondCreatedCompany = await companyApi.CreateCompanyAsync(createCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                secondCreatedCompany.Should().NotBeNull();

                var companies = (await companyApi.GetCompaniesAsync())
                    .Where(x => x.Name.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                companies.Should().Contain(x => x.Id == firstCreatedCompany.Id)
                    .And.Contain(x => x.Id == secondCreatedCompany.Id);
            }
        }
    }
}
