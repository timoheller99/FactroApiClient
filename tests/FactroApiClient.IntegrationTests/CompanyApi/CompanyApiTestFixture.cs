namespace FactroApiClient.IntegrationTests.CompanyApi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Company;

    public sealed class CompanyApiTestFixture : BaseTestFixture, IDisposable
    {
        public CompanyApiTestFixture()
        {
            this.ClearFactroInstanceAsync().GetAwaiter().GetResult();
        }

        public override async Task ClearFactroInstanceAsync()
        {
            await this.ClearCompaniesAsync();
        }

        public void Dispose()
        {
            this.ClearFactroInstanceAsync().GetAwaiter().GetResult();
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
