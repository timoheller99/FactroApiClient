namespace FactroApiClient.UnitTests.Appointment
{
    using System.Diagnostics.CodeAnalysis;

    using Xunit;

    [SuppressMessage("Naming Rules", "MA0048", Justification = "Improve readability with partial class.")]
    [SuppressMessage("Naming Rules", "VSTHRD200", Justification = "Improve readability with partial class.")]
    public partial class AppointmentApiTests : IClassFixture<AppointmentApiTestFixture>
    {
        private readonly AppointmentApiTestFixture fixture;

        public AppointmentApiTests(AppointmentApiTestFixture fixture)
        {
            this.fixture = fixture;
        }
    }
}
