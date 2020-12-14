namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Company;
    using FactroApiClient.Company.Contracts.Basic;

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

            foreach (var companyPayload in companiesToRemove)
            {
                await service.DeleteCompanyAsync(companyPayload.Id);
            }
        }
    }
}
