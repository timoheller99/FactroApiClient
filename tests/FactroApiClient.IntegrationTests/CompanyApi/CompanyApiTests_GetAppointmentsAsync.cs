namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts;
    using FactroApiClient.Company.Contracts.Basic;

    using FluentAssertions;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task GetCompaniesAsync_ExistingCompanies_ShouldReturnExistingCompanies()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompanies = new List<CreateCompanyResponse>();

            const int companyCount = 5;
            var createCompanyRequest = new CreateCompanyRequest($"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}");
            for (var i = 0; i < companyCount; i++)
            {
                createCompanyRequest.Name = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";
                existingCompanies.Add(await companyApi.CreateCompanyAsync(createCompanyRequest));
            }

            var result = new List<GetCompanyPayload>();

            // Act
            Func<Task> act = async () => result = (await companyApi.GetCompaniesAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            foreach (var existingCompany in existingCompanies)
            {
                result.Should().ContainEquivalentOf(existingCompany);
            }
        }
    }
}
