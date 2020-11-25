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

            const string employeeId = BaseTestFixture.ValidEmployeeId;
            var startDate = DateTime.Now;
            var endDate = startDate.AddHours(1);
            var subject = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, startDate, endDate, subject);
            var existingAppointment = await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            var deletedAppointment = new DeleteAppointmentResponse();

            // Act
            Func<Task> act = async () => deletedAppointment = await appointmentApi.DeleteAppointmentAsync(existingAppointment.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deletedAppointment.Should().BeEquivalentTo(existingAppointment);
        }

        [Fact]
        public async Task DeleteAppointmentAsync_NotExistingAppointment_ShouldReturnNull()
        {
            // Arrange
            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            var deletedAppointment = new DeleteAppointmentResponse();

            // Act
            Func<Task> act = async () => deletedAppointment = await appointmentApi.DeleteAppointmentAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            deletedAppointment.Should().BeNull();
        }
    }
}
