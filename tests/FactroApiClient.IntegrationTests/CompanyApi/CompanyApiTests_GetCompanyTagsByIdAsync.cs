namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts.CompanyTag;

    using FluentAssertions;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task GetTagsOfCompanyAsync_ExistingCompanyWithTags_ShouldReturnExpectedCompany()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompany = await this.fixture.CreateTestCompanyAsync(companyApi);

            var existingCompanyTags = new List<CreateCompanyTagResponse>();

            const int companyTagCount = 5;

            for (var i = 0; i < companyTagCount; i++)
            {
                var createdCompanyTag = await this.fixture.CreateTestCompanyTagAsync(companyApi);
                existingCompanyTags.Add(createdCompanyTag);

                var addCompanyTagRequest = new AddCompanyTagAssociationRequest(createdCompanyTag.Id);
                await companyApi.AddTagToCompanyAsync(existingCompany.Id, addCompanyTagRequest);
            }

            var getCompanyTagsByIdResponse = new List<GetCompanyTagAssociationPayload>();

            // Act
            Func<Task> act = async () => getCompanyTagsByIdResponse = (await companyApi.GetTagsOfCompanyAsync(existingCompany.Id)).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getCompanyTagsByIdResponse.Should().BeEquivalentTo(existingCompanyTags);
        }

        [Fact]
        public async Task GetTagsOfCompanyAsync_ExistingCompanyWithoutTags_ShouldReturnExpectedCompany()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompany = await this.fixture.CreateTestCompanyAsync(companyApi);

            var getCompanyTagsByIdResponse = new List<GetCompanyTagAssociationPayload>();

            // Act
            Func<Task> act = async () => getCompanyTagsByIdResponse = (await companyApi.GetTagsOfCompanyAsync(existingCompany.Id)).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getCompanyTagsByIdResponse.Should().BeEmpty();
        }

        [Fact]
        public async Task GetTagsOfCompanyAsync_NotExistingCompany_ShouldReturnEmptyList()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var getCompanyTagsByIdResponse = new List<GetCompanyTagAssociationPayload>();

            // Act
            Func<Task> act = async () => getCompanyTagsByIdResponse = (await companyApi.GetTagsOfCompanyAsync(Guid.NewGuid().ToString())).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getCompanyTagsByIdResponse.Should().BeEmpty();
        }
    }
}
