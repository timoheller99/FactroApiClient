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
        public async Task GetAppointmentAsync_ExistingAppointment_ShouldReturnExpectedAppointment()
        {
            // Arrange
            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            const string employeeId = BaseTestFixture.ValidEmployeeId;
            var startDate = DateTime.Now;
            var endDate = startDate.AddHours(1);
            var subject = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, startDate, endDate, subject);

            var existingAppointment = await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            var getAppointmentByIdResponse = new GetAppointmentByIdResponse();

            // Act
            Func<Task> act = async () => getAppointmentByIdResponse = await appointmentApi.GetAppointmentByIdAsync(existingAppointment.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getAppointmentByIdResponse.Should().BeEquivalentTo(existingAppointment);
        }

        [Fact]
        public async Task GetAppointmentAsync_NotExistingAppointment_ResultShouldBeNull()
        {
            // Arrange
            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            var getAppointmentByIdResponse = new GetAppointmentByIdResponse();

            // Act
            Func<Task> act = async () => getAppointmentByIdResponse = await appointmentApi.GetAppointmentByIdAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            getAppointmentByIdResponse.Should().BeNull();
        }
    }
}
