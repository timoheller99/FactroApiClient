namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts.Basic;
    using FactroApiClient.Company.Contracts.CompanyTag;

    public sealed class CompanyApiTestFixture : BaseTestFixture
    {
        public CompanyApiTestFixture()
        {
            this.ClearFactroInstanceAsync().GetAwaiter().GetResult();
        }

        public async Task<CreateCompanyResponse> CreateTestCompanyAsync(ICompanyApi companyApi)
        {
            var name = $"{TestPrefix}{Guid.NewGuid().ToString()}";

            var createCompanyRequest = new CreateCompanyRequest(name);

            var createCompanyResponse = await companyApi.CreateCompanyAsync(createCompanyRequest);

            return createCompanyResponse;
        }

        public async Task<CreateCompanyTagResponse> CreateTestCompanyTagAsync(ICompanyApi companyApi)
        {
            var name = $"{TestPrefix}{Guid.NewGuid().ToString()}";

            var createCompanyTagRequest = new CreateCompanyTagRequest(name);

            var createCompanyTagResponse = await companyApi.CreateCompanyTagAsync(createCompanyTagRequest);

            return createCompanyTagResponse;
        }

        public async Task<IEnumerable<GetCompanyTagPayload>> GetCompanyTagsAsync(ICompanyApi companyApi)
        {
            return (await companyApi.GetCompanyTagsAsync()).Where(x => x.Name.StartsWith(TestPrefix));
        }

        protected override async Task ClearFactroInstanceAsync()
        {
            var tasks = new[]
            {
                this.ClearCompaniesAsync(),
                this.ClearCompanyTagsAsync(),
            };

            await Task.WhenAll(tasks);
        }

        private async Task ClearCompaniesAsync()
        {
            var service = this.GetService<ICompanyApi>();

            var companies = await service.GetCompaniesAsync();

            var companiesToRemove = companies.Where(x => x.Name.StartsWith(TestPrefix));

            var tasks = companiesToRemove.Select(companyPayload => service.DeleteCompanyAsync(companyPayload.Id));

            await Task.WhenAll(tasks);
        }

        private async Task ClearCompanyTagsAsync()
        {
            var service = this.GetService<ICompanyApi>();

            var companyTags = await service.GetCompanyTagsAsync();

            var companyTagsToRemove = companyTags.Where(x => x.Name.StartsWith(TestPrefix));

            var tasks = companyTagsToRemove.Select(companyTagPayload => service.DeleteCompanyTagAsync(companyTagPayload.Id));

            await Task.WhenAll(tasks);
        }
    }
}
