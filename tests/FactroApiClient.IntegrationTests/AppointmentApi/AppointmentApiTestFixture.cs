namespace FactroApiClient.IntegrationTests.AppointmentApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment;

    public sealed class AppointmentApiTestFixture : BaseTestFixture, IDisposable
    {
        public AppointmentApiTestFixture()
        {
            this.ClearFactroInstanceAsync().GetAwaiter().GetResult();
        }

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

        public void Dispose()
        {
            this.ClearFactroInstanceAsync().GetAwaiter().GetResult();
        }

        private async Task ClearAppointmentsAsync()
        {
            var service = this.GetService<AppointmentApi>();

            var appointments = await service.GetAppointmentsAsync();

            var appointmentsToRemove = appointments.Where(x => x.Subject.StartsWith(TestPrefix));

            foreach (var appointmentPayload in appointmentsToRemove)
            {
                await service.DeleteAppointmentAsync(appointmentPayload.Id);
            }
        }
    }
}
