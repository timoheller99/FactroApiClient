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

            var existingAppointments = new List<CreateAppointmentResponse>();

            const int appointmentCount = 5;

            for (var i = 0; i < appointmentCount; i++)
            {
                existingAppointments.Add(await this.fixture.CreateTestAppointmentAsync(appointmentApi));
            }

            var getAppointmentsResponse = new List<GetAppointmentPayload>();

            // Act
            Func<Task> act = async () => getAppointmentsResponse = (await appointmentApi.GetAppointmentsAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            foreach (var existingAppointment in existingAppointments)
            {
                getAppointmentsResponse.Should().ContainEquivalentOf(existingAppointment);
            }
        }
    }
}
