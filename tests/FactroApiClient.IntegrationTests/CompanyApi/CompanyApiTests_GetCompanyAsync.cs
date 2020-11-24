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
        public async Task GetCompanyAsync_ExistingCompany_ShouldFetchCompany()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var name = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createCompanyRequest = new CreateCompanyRequest(name);
            var existingCompany = await companyApi.CreateCompanyAsync(createCompanyRequest);

            var fetchedCompany = new GetCompanyByIdResponse();

            // Act
            Func<Task> act = async () => fetchedCompany = await companyApi.GetCompanyByIdAsync(existingCompany.Id);

            // Assert
            await act.Should().NotThrowAsync();

            fetchedCompany.Should().BeEquivalentTo(existingCompany);
        }

        [Fact]
        public async Task GetCompanyAsync_NotExistingCompany_ResultShouldBeNull()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var fetchedCompany = new GetCompanyByIdResponse();

            // Act
            Func<Task> act = async () => fetchedCompany = await companyApi.GetCompanyByIdAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            fetchedCompany.Should().BeNull();
        }
    }
}
