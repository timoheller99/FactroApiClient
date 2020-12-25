namespace FactroApiClient.IntegrationTests.ProjectApi
{
    using System.Diagnostics.CodeAnalysis;

    using Xunit;

    [SuppressMessage("Naming Rules", "MA0048", Justification = "Improve readability with partial class.")]
    [SuppressMessage("Naming Rules", "VSTHRD200", Justification = "Improve readability with partial class.")]
    public partial class ProjectApiTests : IClassFixture<ProjectApiTestFixture>
    {
        private readonly ProjectApiTestFixture fixture;

        public ProjectApiTests(ProjectApiTestFixture fixture)
        {
            this.fixture = fixture;
        }
    }
}
