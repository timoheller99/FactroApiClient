namespace FactroApiClient.IntegrationTests.AppointmentApi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment;
    using FactroApiClient.Appointment.Contracts;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Xunit;

    public partial class AppointmentApiTests
    {
        [Fact]
        public async Task UpdateAppointmentAsync_ValidUpdate_ShouldReturnUpdatedAppointment()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            var existingAppointment = await this.fixture.CreateTestAppointmentAsync(appointmentApi);

            var updatedSubject = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";
            var updateAppointmentRequest = new UpdateAppointmentRequest
            {
                Subject = updatedSubject,
            };

            var updateAppointmentResponse = new UpdateAppointmentResponse();

            // Act
            Func<Task> act = async () => updateAppointmentResponse = await appointmentApi.UpdateAppointmentAsync(existingAppointment.Id, updateAppointmentRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var appointments = (await appointmentApi.GetAppointmentsAsync())
                    .Where(x => x.Subject.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                appointments.Should().ContainEquivalentOf(updateAppointmentResponse);

                appointments.Single(x => x.Id == existingAppointment.Id).Subject.Should().Be(updatedSubject);
            }

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task UpdateAppointmentAsync_InvalidUpdate_ShouldNotUpdateAppointment()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            var existingAppointment = await this.fixture.CreateTestAppointmentAsync(appointmentApi);

            const string updatedSubject = null;
            var updateAppointmentRequest = new UpdateAppointmentRequest
            {
                Subject = updatedSubject,
            };

            // Act
            Func<Task> act = async () => await appointmentApi.UpdateAppointmentAsync(existingAppointment.Id, updateAppointmentRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var appointments = (await appointmentApi.GetAppointmentsAsync())
                    .Where(x => x.Subject.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                appointments.Single(x => x.Id == existingAppointment.Id).Should().BeEquivalentTo(existingAppointment);
            }

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task UpdateAppointmentAsync_NotExistingAppointmentId_ShouldThrowFactroApiException()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            var updatedSubject = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";
            var updateAppointmentRequest = new UpdateAppointmentRequest
            {
                Subject = updatedSubject,
            };

            var updateAppointmentResponse = default(UpdateAppointmentResponse);

            // Act
            Func<Task> act = async () => updateAppointmentResponse = await appointmentApi.UpdateAppointmentAsync(Guid.NewGuid().ToString(), updateAppointmentRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            using (new AssertionScope())
            {
                var appointments = (await appointmentApi.GetAppointmentsAsync())
                    .Where(x => x.Subject.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                appointments.All(x => x.Subject != updatedSubject).Should().BeTrue();

                updateAppointmentResponse.Should().BeNull();
            }

            await this.fixture.ClearFactroInstanceAsync();
        }
    }
}
