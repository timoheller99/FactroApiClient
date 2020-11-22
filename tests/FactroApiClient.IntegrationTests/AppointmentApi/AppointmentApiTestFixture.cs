namespace FactroApiClient.IntegrationTests.AppointmentApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment;

    public class AppointmentApiTestFixture : BaseTestFixture
    {
        public static IEnumerable<object[]> InvalidEmployeeIds { get; } = new[]
        {
            new object[] { null },
            new object[] { string.Empty },
            new object[] { " " },
        };

        public override async Task ClearFactroInstanceAsync()
        {
            await this.ClearAppointmentsAsync();
        }

        private async Task ClearAppointmentsAsync()
        {
            var service = this.GetService<AppointmentApi>();

            var appointments = await service.GetAppointmentsAsync();

            var tasks = appointments.Select(x => service.DeleteAppointmentAsync(x.Id));

            await Task.WhenAll(tasks);
        }
    }
}
