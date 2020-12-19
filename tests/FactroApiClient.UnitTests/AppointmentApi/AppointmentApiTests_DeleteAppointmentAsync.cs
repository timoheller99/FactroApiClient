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
        public async Task DeleteAppointmentAsync_ValidId_ShouldReturnDeletedAppointment()
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

            var deleteAppointmentResponse = new DeleteAppointmentResponse();

            // Act
            Func<Task> act = async () => deleteAppointmentResponse = await appointmentApi.DeleteAppointmentAsync(existingAppointment.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deleteAppointmentResponse.Id.Should().Be(existingAppointment.Id);
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
        public async Task DeleteAppointmentAsync_UnsuccessfulRequest_ShouldThrowAppointmentApiException()
        {
            // Arrange
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
            Func<Task> act = async () => await appointmentApi.DeleteAppointmentAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
