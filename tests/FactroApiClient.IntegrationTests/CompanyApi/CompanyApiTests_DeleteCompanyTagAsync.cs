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
        public async Task DeleteCompanyTagAsync_ExistingCompanyTag_ShouldDeleteCompanyTag()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompanyTag = await this.fixture.CreateTestCompanyTagAsync(companyApi);

            var deleteCompanyTagResponse = new DeleteCompanyTagResponse();

            // Act
            Func<Task> act = async () => deleteCompanyTagResponse = await companyApi.DeleteCompanyTagAsync(existingCompanyTag.Id);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                deleteCompanyTagResponse.Should().BeEquivalentTo(existingCompanyTag);

                var companyTags = await this.fixture.GetCompanyTagsAsync(companyApi);
                companyTags.Should().NotContain(x => x.Id == existingCompanyTag.Id);
            }
        }

        [Fact]
        public async Task DeleteCompanyTagAsync_NotExistingCompanyTag_ShouldReturnNull()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var deleteCompanyTagResponse = new DeleteCompanyTagResponse();

            // Act
            Func<Task> act = async () => deleteCompanyTagResponse = await companyApi.DeleteCompanyTagAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            deleteCompanyTagResponse.Should().BeNull();
        }

        [Fact]
        public async Task DeleteCompanyTagAsync_ExistingCompanyTagWithAssociation_ShouldDeleteCompanyTag()
        {
            // Arrange
            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompany = await this.fixture.CreateTestCompanyAsync(companyApi);
            var existingCompanyTag = await this.fixture.CreateTestCompanyTagAsync(companyApi);

            var addCompanyTagRequest = new AddCompanyTagAssociationRequest(existingCompanyTag.Id);
            await companyApi.AddTagToCompanyAsync(existingCompany.Id, addCompanyTagRequest);

            var deleteCompanyTagResponse = new DeleteCompanyTagResponse();

            // Act
            Func<Task> act = async () => deleteCompanyTagResponse = await companyApi.DeleteCompanyTagAsync(existingCompanyTag.Id);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                deleteCompanyTagResponse.Should().BeEquivalentTo(existingCompanyTag);

                (await this.fixture.GetCompanyTagsAsync(companyApi)).Should().NotContain(x => x.Id == existingCompanyTag.Id);
            }
        }
    }
}
