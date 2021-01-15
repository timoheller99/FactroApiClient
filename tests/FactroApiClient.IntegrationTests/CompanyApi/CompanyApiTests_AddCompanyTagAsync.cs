namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts.CompanyTag;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task AddTagToCompanyAsync_ExistingCompany_ShouldAddCompanyTag()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompany = await this.fixture.CreateTestCompanyAsync(companyApi);

            var createdCompanyTag = await this.fixture.CreateTestCompanyTagAsync(companyApi);

            var addCompanyTagRequest = new AddCompanyTagAssociationRequest(createdCompanyTag.Id);

            // Act
            Func<Task> act = async () => await companyApi.AddTagToCompanyAsync(existingCompany.Id, addCompanyTagRequest);

            // Assert
            await act.Should().NotThrowAsync();

            (await companyApi.GetTagsOfCompanyAsync(existingCompany.Id)).Should().ContainEquivalentOf(createdCompanyTag);

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task AddTagToCompanyAsync_NotExistingCompany_ShouldNotAddCompanyTag()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var createdCompanyTag = await this.fixture.CreateTestCompanyTagAsync(companyApi);

            var addCompanyTagRequest = new AddCompanyTagAssociationRequest(createdCompanyTag.Id);

            var companyId = Guid.NewGuid().ToString();

            // Act
            Func<Task> act = async () => await companyApi.AddTagToCompanyAsync(companyId, addCompanyTagRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            (await companyApi.GetTagsOfCompanyAsync(companyId)).Should().BeEmpty();

            await this.fixture.ClearFactroInstanceAsync();
        }
    }
}
