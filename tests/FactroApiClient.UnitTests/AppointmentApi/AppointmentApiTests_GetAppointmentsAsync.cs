namespace FactroApiClient.UnitTests.AppointmentApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        public async Task GetAppointmentsAsync_ValidRequest_ShouldReturnAppointments()
        {
            // Arrange
            var existingAppointmentsList = new List<GetAppointmentPayload>
            {
                new GetAppointmentPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
                new GetAppointmentPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
            };

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(existingAppointmentsList, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var appointmentApi = this.fixture.GetAppointmentApi(expectedResponse);

            var getAppointmentsResponse = new List<GetAppointmentPayload>();

            // Act
            Func<Task> act = async () => getAppointmentsResponse = (await appointmentApi.GetAppointmentsAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getAppointmentsResponse.Should().HaveCount(existingAppointmentsList.Count);
        }

        [Fact(Skip = "Throw of exception is not implemented yet.")]
        public async Task GetAppointmentsAsync_UnsuccessfulRequest_ShouldThrowAppointmentApiException()
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
            Func<Task> act = async () => await appointmentApi.GetAppointmentsAsync();

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
