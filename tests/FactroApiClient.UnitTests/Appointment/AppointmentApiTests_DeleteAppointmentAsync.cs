namespace FactroApiClient.UnitTests.Appointment
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment.Contracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class AppointmentApiTests
    {
        [Fact]
        public async Task DeleteAppointmentAsync_ValidId_ShouldReturnDeletedAppointment()
        {
            // Arrange
            var appointment = new GetAppointmentPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedContent =
                new StringContent(JsonConvert.SerializeObject(appointment, this.fixture.JsonSerializerSettings));

            var response = new HttpResponseMessage
            {
                Content = expectedContent,
            };

            var appointmentApi = this.fixture.GetAppointmentApi(response);

            var appointmentId = appointment.Id;

            var deletedAppointment = new DeleteAppointmentResponse();

            // Act
            Func<Task> act = async () => deletedAppointment = await appointmentApi.DeleteAppointmentAsync(appointmentId);

            // Assert
            await act.Should().NotThrowAsync();

            deletedAppointment.Id.Should().Be(appointment.Id);
        }

        [Theory]
        [MemberData(nameof(AppointmentApiTestFixture.InvalidAppointmentIds), MemberType = typeof(AppointmentApiTestFixture))]
        public async Task DeleteAppointmentAsync_InvalidAppointmentId_ShouldThrowArgumentNullException(string appointmentId)
        {
            // Arrange
            var appointmentApi = this.fixture.GetAppointmentApi();

            // Act
            Func<Task> act = async () => await appointmentApi.DeleteAppointmentAsync(appointmentId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteAppointmentAsync_UnsuccessfulRequest_ResultShouldBeNull()
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var appointmentApi = this.fixture.GetAppointmentApi(response);

            var result = new DeleteAppointmentResponse();

            // Act
            Func<Task> act = async () => result = await appointmentApi.DeleteAppointmentAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            result.Should().BeNull();
        }
    }
}
