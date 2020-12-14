namespace FactroApiClient.IntegrationTests.ContactApi
{
    using System.Diagnostics.CodeAnalysis;

    using Xunit;

    [SuppressMessage("Naming Rules", "MA0048", Justification = "Improve readability with partial class.")]
    [SuppressMessage("Naming Rules", "VSTHRD200", Justification = "Improve readability with partial class.")]
    public partial class ContactApiTests : IClassFixture<ContactApiTestFixture>
    {
        private readonly ContactApiTestFixture fixture;

        public ContactApiTests(ContactApiTestFixture fixture)
        {
            this.fixture = fixture;
        }
    }
}
