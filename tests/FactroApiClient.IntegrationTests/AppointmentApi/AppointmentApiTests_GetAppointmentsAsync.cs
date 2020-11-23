namespace FactroApiClient.IntegrationTests.AppointmentApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment;
    using FactroApiClient.Appointment.Contracts;

    using FluentAssertions;

    using Xunit;

    public partial class AppointmentApiTests
    {
        [Fact]
        public async Task GetAppointmentsAsync_ExistingAppointments_ShouldReturnExistingAppointments()
        {
            // Arrange
            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            const string employeeId = BaseTestFixture.ValidEmployeeId;

            var existingAppointments = new List<CreateAppointmentResponse>();

            const int appointmentCount = 5;
            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, DateTime.Now, DateTime.Now.AddHours(1), $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}");
            for (var i = 0; i < appointmentCount; i++)
            {
                createAppointmentRequest.Subject = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";
                existingAppointments.Add(await appointmentApi.CreateAppointmentAsync(createAppointmentRequest));
            }

            var result = new List<GetAppointmentPayload>();

            // Act
            Func<Task> act = async () => result = (await appointmentApi.GetAppointmentsAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            foreach (var existingAppointment in existingAppointments)
            {
                result.Should().ContainEquivalentOf(existingAppointment);
            }
        }
    }
}
