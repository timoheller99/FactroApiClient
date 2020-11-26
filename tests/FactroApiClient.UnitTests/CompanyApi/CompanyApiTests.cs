namespace FactroApiClient.UnitTests.CompanyApi
{
    using System.Diagnostics.CodeAnalysis;

    using Xunit;

    [SuppressMessage("Naming Rules", "MA0048", Justification = "Improve readability with partial class.")]
    [SuppressMessage("Naming Rules", "VSTHRD200", Justification = "Improve readability with partial class.")]
    public partial class CompanyApiTests : IClassFixture<CompanyApiTestFixture>
    {
        private readonly CompanyApiTestFixture fixture;

        public CompanyApiTests(CompanyApiTestFixture fixture)
        {
            this.fixture = fixture;
        }
    }
}
