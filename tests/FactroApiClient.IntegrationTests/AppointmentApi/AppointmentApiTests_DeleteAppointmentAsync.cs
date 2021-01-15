namespace FactroApiClient.IntegrationTests.AppointmentApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment;
    using FactroApiClient.Appointment.Contracts;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class AppointmentApiTests
    {
        [Fact]
        public async Task DeleteAppointmentAsync_ExistingAppointment_ShouldDeleteExistingAppointment()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            var existingAppointment = await this.fixture.CreateTestAppointmentAsync(appointmentApi);

            var deleteAppointmentResponse = new DeleteAppointmentResponse();

            // Act
            Func<Task> act = async () => deleteAppointmentResponse = await appointmentApi.DeleteAppointmentAsync(existingAppointment.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deleteAppointmentResponse.Should().BeEquivalentTo(existingAppointment);

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task DeleteAppointmentAsync_NotExistingAppointment_ShouldThrowFactroApiException()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            var deleteAppointmentResponse = default(DeleteAppointmentResponse);

            // Act
            Func<Task> act = async () => deleteAppointmentResponse = await appointmentApi.DeleteAppointmentAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            deleteAppointmentResponse.Should().BeNull();

            await this.fixture.ClearFactroInstanceAsync();
        }
    }
}
