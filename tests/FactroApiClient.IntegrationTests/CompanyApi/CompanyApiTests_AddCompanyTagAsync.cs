namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts.CompanyTag;

    using FluentAssertions;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task AddCompanyTagAsync_ExistingCompany_ShouldAddCompanyTag()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompany = await this.fixture.CreateTestCompanyAsync(companyApi);

            var createdCompanyTag = await this.fixture.CreateTestCompanyTagAsync(companyApi);

            var addCompanyTagRequest = new AddCompanyTagAssociationRequest(createdCompanyTag.Id);

            // Act
            Func<Task> act = async () => await companyApi.AddCompanyTagAsync(existingCompany.Id, addCompanyTagRequest);

            // Assert
            await act.Should().NotThrowAsync();

            (await companyApi.GetCompanyTagsByIdAsync(existingCompany.Id)).Should().ContainEquivalentOf(createdCompanyTag);
        }

        [Fact]
        public async Task AddCompanyTagAsync_NotExistingCompany_ShouldNotAddCompanyTag()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var createdCompanyTag = await this.fixture.CreateTestCompanyTagAsync(companyApi);

            var addCompanyTagRequest = new AddCompanyTagAssociationRequest(createdCompanyTag.Id);

            var companyId = Guid.NewGuid().ToString();

            // Act
            Func<Task> act = async () => await companyApi.AddCompanyTagAsync(companyId, addCompanyTagRequest);

            // Assert
            await act.Should().NotThrowAsync();

            (await companyApi.GetCompanyTagsByIdAsync(companyId)).Should().BeEmpty();
        }
    }
}
