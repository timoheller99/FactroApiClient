namespace FactroApiClient.IntegrationTests.AppointmentApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment;
    using FactroApiClient.Appointment.Contracts;

    using FluentAssertions;

    using Xunit;

    public partial class AppointmentApiTests
    {
        [Fact]
        public async Task DeleteAppointmentAsync_ExistingAppointment_ShouldDeleteExistingAppointment()
        {
            // Arrange
            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            var existingAppointment = await this.fixture.CreateTestAppointmentAsync(appointmentApi);

            var deleteAppointmentResponse = new DeleteAppointmentResponse();

            // Act
            Func<Task> act = async () => deleteAppointmentResponse = await appointmentApi.DeleteAppointmentAsync(existingAppointment.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deleteAppointmentResponse.Should().BeEquivalentTo(existingAppointment);
        }

        [Fact]
        public async Task DeleteAppointmentAsync_NotExistingAppointment_ShouldReturnNull()
        {
            // Arrange
            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            var deleteAppointmentResponse = new DeleteAppointmentResponse();

            // Act
            Func<Task> act = async () => deleteAppointmentResponse = await appointmentApi.DeleteAppointmentAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            deleteAppointmentResponse.Should().BeNull();
        }
    }
}
