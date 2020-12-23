namespace FactroApiClient.UnitTests.PackageApi
{
    using System.Diagnostics.CodeAnalysis;

    using Xunit;

    [SuppressMessage("Naming Rules", "MA0048", Justification = "Improve readability with partial class.")]
    [SuppressMessage("Naming Rules", "VSTHRD200", Justification = "Improve readability with partial class.")]
    public partial class PackageApiTests : IClassFixture<PackageApiTestFixture>
    {
        private readonly PackageApiTestFixture fixture;

        public PackageApiTests(PackageApiTestFixture fixture)
        {
            this.fixture = fixture;
        }
    }
}
