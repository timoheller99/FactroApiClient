namespace FactroApiClient.UnitTests.Appointment
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
            var appointmentList = new List<GetAppointmentPayload>
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

            var expectedContent = new StringContent(JsonConvert.SerializeObject(appointmentList, this.fixture.JsonSerializerSettings));

            var response = new HttpResponseMessage
            {
                Content = expectedContent,
            };

            var appointmentApi = this.fixture.GetAppointmentApi(response);

            var result = new List<GetAppointmentPayload>();

            // Act
            Func<Task> act = async () => result = (await appointmentApi.GetAppointmentsAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            result.Should().HaveCount(appointmentList.Count);
        }

        [Fact]
        public async Task GetAppointmentsAsync_UnsuccessfulRequest_ShouldReturnNull()
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

            var result = new List<GetAppointmentPayload>();

            // Act
            Func<Task> act = async () => result = (await appointmentApi.GetAppointmentsAsync())?.ToList();

            // Assert
            await act.Should().NotThrowAsync();

            result.Should().BeNull();
        }
    }
}
