namespace FactroApiClient.IntegrationTests.AppointmentApi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment;
    using FactroApiClient.Appointment.Contracts;

    public sealed class AppointmentApiTestFixture : BaseTestFixture, IDisposable
    {
        public AppointmentApiTestFixture()
        {
            this.ClearFactroInstanceAsync().GetAwaiter().GetResult();
        }

        public override async Task ClearFactroInstanceAsync()
        {
            await this.ClearAppointmentsAsync();
        }

        public void Dispose()
        {
            this.ClearFactroInstanceAsync().GetAwaiter().GetResult();
        }

        public async Task<CreateAppointmentResponse> CreateTestAppointmentAsync(IAppointmentApi appointmentApi)
        {
            const string employeeId = ValidEmployeeId;
            var startDate = DateTime.Now;
            var endDate = startDate.AddHours(1);
            var subject = $"{TestPrefix}{Guid.NewGuid().ToString()}";

            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, startDate, endDate, subject);

            var createAppointmentResponse = await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            return createAppointmentResponse;
        }

        private async Task ClearAppointmentsAsync()
        {
            var service = this.GetService<IAppointmentApi>();

            var appointments = await service.GetAppointmentsAsync();

            var appointmentsToRemove = appointments.Where(x => x.Subject.StartsWith(TestPrefix));

            foreach (var appointmentPayload in appointmentsToRemove)
            {
                await service.DeleteAppointmentAsync(appointmentPayload.Id);
            }
        }
    }
}
