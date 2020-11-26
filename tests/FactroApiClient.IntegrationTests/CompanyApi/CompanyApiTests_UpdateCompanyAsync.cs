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
        public async Task UpdateCompanyAsync_ValidUpdate_ShouldReturnUpdatedCompany()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompany = await this.fixture.CreateTestCompanyAsync(companyApi);

            var updatedName = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";
            var updateCompanyRequest = new UpdateCompanyRequest
            {
                Name = updatedName,
            };

            var updateCompanyResponse = new UpdateCompanyResponse();

            // Act
            Func<Task> act = async () => updateCompanyResponse = await companyApi.UpdateCompanyAsync(existingCompany.Id, updateCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var companies = (await companyApi.GetCompaniesAsync())
                    .Where(x => x.Name.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                companies.Should().ContainEquivalentOf(updateCompanyResponse);

                companies.Single(x => x.Id == existingCompany.Id).Name.Should().Be(updatedName);
            }
        }

        [Fact]
        public async Task UpdateCompanyAsync_InvalidUpdate_ShouldNotUpdateCompany()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompany = await this.fixture.CreateTestCompanyAsync(companyApi);

            const string updatedName = null;
            var updateCompanyRequest = new UpdateCompanyRequest
            {
                Name = updatedName,
            };

            // Act
            Func<Task> act = async () => await companyApi.UpdateCompanyAsync(existingCompany.Id, updateCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var companies = (await companyApi.GetCompaniesAsync())
                    .Where(x => x.Name.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                companies.Single(x => x.Id == existingCompany.Id).Should().BeEquivalentTo(existingCompany);
            }
        }

        [Fact]
        public async Task UpdateCompanyAsync_NotExistingCompanyId_ShouldReturnNull()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var updatedName = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";
            var updateCompanyRequest = new UpdateCompanyRequest
            {
                Name = updatedName,
            };

            var updateCompanyResponse = new UpdateCompanyResponse();

            // Act
            Func<Task> act = async () => updateCompanyResponse = await companyApi.UpdateCompanyAsync(Guid.NewGuid().ToString(), updateCompanyRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var companies = (await companyApi.GetCompaniesAsync())
                    .Where(x => x.Name.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                companies.All(x => x.Name != updatedName).Should().BeTrue();

                updateCompanyResponse.Should().BeNull();
            }
        }
    }
}
