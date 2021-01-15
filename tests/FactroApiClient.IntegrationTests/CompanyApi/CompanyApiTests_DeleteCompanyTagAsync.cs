namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts.CompanyTag;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task DeleteCompanyTagAsync_ExistingCompanyTag_ShouldDeleteCompanyTag()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

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

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task DeleteCompanyTagAsync_NotExistingCompanyTag_ShouldThrowFactroApiException()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var deleteCompanyTagResponse = default(DeleteCompanyTagResponse);

            // Act
            Func<Task> act = async () => deleteCompanyTagResponse = await companyApi.DeleteCompanyTagAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            deleteCompanyTagResponse.Should().BeNull();

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task DeleteCompanyTagAsync_ExistingCompanyTagWithAssociation_ShouldDeleteCompanyTag()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

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

            await this.fixture.ClearFactroInstanceAsync();
        }
    }
}
