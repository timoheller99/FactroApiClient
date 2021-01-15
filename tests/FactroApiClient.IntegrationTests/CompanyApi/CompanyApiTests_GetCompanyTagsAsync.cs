namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts.CompanyTag;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Xunit;

    public partial class CompanyApiTests
    {
        [Fact]
        public async Task GetCompanyTagsAsync_ExistingCompanyTags_ShouldReturnExistingCompanyTags()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var existingCompanyTags = new List<CreateCompanyTagResponse>();

            const int companyTagCount = 5;

            for (var i = 0; i < companyTagCount; i++)
            {
                existingCompanyTags.Add(await this.fixture.CreateTestCompanyTagAsync(companyApi));
            }

            var getCompanyTagsResponse = new List<GetCompanyTagPayload>();

            // Act
            Func<Task> act = async () => getCompanyTagsResponse = (await companyApi.GetCompanyTagsAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                foreach (var existingCompanyTag in existingCompanyTags)
                {
                    getCompanyTagsResponse.Should().ContainEquivalentOf(existingCompanyTag);
                }
            }
        }
    }
}
