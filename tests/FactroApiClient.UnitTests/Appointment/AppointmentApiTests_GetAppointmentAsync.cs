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
        public async Task GetAppointmentAsync_ValidId_ShouldReturnExpectedAppointment()
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

            var result = new GetAppointmentByIdResponse();

            // Act
            Func<Task> act = async () => result = await appointmentApi.GetAppointmentByIdAsync(appointmentId);

            // Assert
            await act.Should().NotThrowAsync();

            result.Id.Should().Be(appointment.Id);
        }

        [Theory]
        [MemberData(nameof(AppointmentApiTestFixture.InvalidAppointmentIds), MemberType = typeof(AppointmentApiTestFixture))]
        public async Task GetAppointmentAsync_InvalidAppointmentId_ShouldThrowArgumentNullException(string appointmentId)
        {
            // Arrange
            var appointmentApi = this.fixture.GetAppointmentApi();

            // Act
            Func<Task> act = async () => await appointmentApi.GetAppointmentByIdAsync(appointmentId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetAppointmentAsync_UnsuccessfulRequest_ShouldReturnNull()
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

            var result = new GetAppointmentByIdResponse();

            // Act
            Func<Task> act = async () => result = await appointmentApi.GetAppointmentByIdAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().NotThrowAsync();

            result.Should().BeNull();
        }
    }
}
