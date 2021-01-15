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
        public async Task CreateAppointmentAsync_ValidAppointment_ShouldStoreAppointment()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            const string employeeId = BaseTestFixture.ValidEmployeeId;
            var startDate = DateTime.Now;
            var endDate = startDate.AddHours(1);
            var subject = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, startDate, endDate, subject);

            var createAppointmentResponse = default(CreateAppointmentResponse);

            // Act
            Func<Task> act = async () => createAppointmentResponse = await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var appointments = (await appointmentApi.GetAppointmentsAsync())
                    .Where(x => x.Subject.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                appointments.Should().ContainEquivalentOf(createAppointmentResponse);
            }

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task CreateAppointmentAsync_NotExistingEmployeeId_ShouldThrowFactroApiException()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            var employeeId = Guid.NewGuid().ToString();
            var startDate = DateTime.Now;
            var endDate = startDate.AddHours(1);
            var subject = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, startDate, endDate, subject);

            var createAppointmentResponse = default(CreateAppointmentResponse);

            // Act
            Func<Task> act = async () => createAppointmentResponse = await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            using (new AssertionScope())
            {
                var appointments = (await appointmentApi.GetAppointmentsAsync())
                    .Where(x => x.Subject.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                appointments.Should().NotContain(x => x.EmployeeId == employeeId && x.Subject == subject);
                createAppointmentResponse.Should().BeNull();
            }

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task CreateAppointmentAsync_TwoIdenticalAppointments_ShouldStoreBothAppointments()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            const string employeeId = BaseTestFixture.ValidEmployeeId;
            var startDate = DateTime.Now;
            var endDate = startDate.AddHours(1);
            var subject = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, startDate, endDate, subject);

            await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            // Act
            Func<Task> act = async () => await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var appointments = (await appointmentApi.GetAppointmentsAsync())
                    .Where(x => x.Subject.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                var matchingAppointments = appointments.Where(x => x.Subject == subject);
                matchingAppointments.Should().HaveCount(2);
            }

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task CreateAppointmentAsync_EndTimeBeforeStartTime_ShouldNotStoreAppointmentAndThrowFactroApiException()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var appointmentApi = this.fixture.GetService<IAppointmentApi>();

            const string employeeId = BaseTestFixture.ValidEmployeeId;
            var startDate = DateTime.Now;
            var endDate = startDate.AddHours(-1);
            var subject = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, startDate, endDate, subject);

            var createAppointmentResponse = default(CreateAppointmentResponse);

            // Act
            Func<Task> act = async () => createAppointmentResponse = await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            using (new AssertionScope())
            {
                var appointments = (await appointmentApi.GetAppointmentsAsync())
                    .Where(x => x.Subject.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                appointments.Should().NotContain(x => x.EmployeeId == employeeId && x.Subject == subject);

                createAppointmentResponse.Should().BeNull();
            }

            await this.fixture.ClearFactroInstanceAsync();
        }
    }
}
