namespace FactroApiClient.IntegrationTests.AppointmentApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment;

    using FluentAssertions;

    using Xunit;

    public partial class AppointmentApiTests
    {
        [Fact]
        public async Task GetAppointmentsAsync()
        {
            // Arrange
            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            // Act
            Func<Task> act = async () => await appointmentApi.GetAppointmentsAsync();

            // Assert
            await act.Should().NotThrowAsync();
        }
    }
}
