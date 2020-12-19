namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts.CompanyTag;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task RemoveTagFromCompanyAsync_ExistingCompanyWithAssociation_ShouldRemoveCompanyTag()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompany = await this.fixture.CreateTestCompanyAsync(companyApi);

            var createdCompanyTag = await this.fixture.CreateTestCompanyTagAsync(companyApi);

            var addCompanyTagRequest = new AddCompanyTagAssociationRequest(createdCompanyTag.Id);

            await companyApi.AddTagToCompanyAsync(existingCompany.Id, addCompanyTagRequest);

            // Act
            Func<Task> act = async () => await companyApi.RemoveTagFromCompanyAsync(existingCompany.Id, createdCompanyTag.Id);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                (await companyApi.GetTagsOfCompanyAsync(existingCompany.Id)).Should().NotContain(x => x.Id == createdCompanyTag.Id);

                (await this.fixture.GetCompanyTagsAsync(companyApi)).Should().ContainEquivalentOf(createdCompanyTag);
            }
        }

        [Fact]
        public async Task RemoveTagFromCompanyAsync_ExistingCompanyWithoutAssociation_ShouldNotThrow()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompany = await this.fixture.CreateTestCompanyAsync(companyApi);

            var createdCompanyTag = await this.fixture.CreateTestCompanyTagAsync(companyApi);

            // Act
            Func<Task> act = async () => await companyApi.RemoveTagFromCompanyAsync(existingCompany.Id, createdCompanyTag.Id);

            // Assert
            await act.Should().NotThrowAsync();

            (await companyApi.GetTagsOfCompanyAsync(existingCompany.Id)).Should().NotContain(x => x.Id == createdCompanyTag.Id);
        }

        [Fact]
        public async Task RemoveTagFromCompanyAsync_NotExistingCompany_ShouldNotThrow()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var createdCompanyTag = await this.fixture.CreateTestCompanyTagAsync(companyApi);

            var companyId = Guid.NewGuid().ToString();

            // Act
            Func<Task> act = async () => await companyApi.RemoveTagFromCompanyAsync(companyId, createdCompanyTag.Id);

            // Assert
            await act.Should().NotThrowAsync();
        }
    }
}
