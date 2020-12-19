namespace FactroApiClient.UnitTests.AppointmentApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment.Contracts;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class AppointmentApiTests
    {
        [Fact]
        public async Task GetAppointmentAsync_ValidId_ShouldReturnExpectedAppointment()
        {
            // Arrange
            var existingAppointment = new GetAppointmentPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(existingAppointment, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var appointmentApi = this.fixture.GetAppointmentApi(expectedResponse);

            var getAppointmentByIdResponse = new GetAppointmentByIdResponse();

            // Act
            Func<Task> act = async () => getAppointmentByIdResponse = await appointmentApi.GetAppointmentByIdAsync(existingAppointment.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getAppointmentByIdResponse.Id.Should().Be(existingAppointment.Id);
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
        public async Task GetAppointmentAsync_UnsuccessfulRequest_ShouldThrowAppointmentApiException()
        {
            // Arrange
            var appointmentId = Guid.NewGuid().ToString();

            var expectedResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var appointmentApi = this.fixture.GetAppointmentApi(expectedResponse);

            // Act
            Func<Task> act = async () => await appointmentApi.GetAppointmentByIdAsync(appointmentId);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
