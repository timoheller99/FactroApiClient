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
        public async Task CreateCompanyTagAsync_ValidCompanyTag_ShouldCreateCompanyTag()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var name = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createCompanyTagRequest = new CreateCompanyTagRequest(name);

            var createCompanyTagResponse = default(CreateCompanyTagResponse);

            // Act
            Func<Task> act = async () => createCompanyTagResponse = await companyApi.CreateCompanyTagAsync(createCompanyTagRequest);

            // Assert
            await act.Should().NotThrowAsync();

            var companyTags = await this.fixture.GetCompanyTagsAsync(companyApi);

            companyTags.Should().ContainEquivalentOf(createCompanyTagResponse);

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task CreateCompanyTagAsync_TwoCompanyTagsWithSameName_ShouldStoreBothCompanyTags()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var companyApi = this.fixture.GetService<ICompanyApi>();

            var name = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createCompanyTagRequest = new CreateCompanyTagRequest(name);

            var firstCreatedCompanyTag = await companyApi.CreateCompanyTagAsync(createCompanyTagRequest);

            var secondCreatedCompanyTag = default(CreateCompanyTagResponse);

            // Act
            Func<Task> act = async () => secondCreatedCompanyTag = await companyApi.CreateCompanyTagAsync(createCompanyTagRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                secondCreatedCompanyTag.Should().NotBeNull();

                var companyTags = await this.fixture.GetCompanyTagsAsync(companyApi);

                companyTags.Should()
                    .Contain(x => x.Id == firstCreatedCompanyTag.Id).And
                    .Contain(x => x.Id == secondCreatedCompanyTag.Id);
            }

            await this.fixture.ClearFactroInstanceAsync();
        }
    }
}
