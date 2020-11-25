namespace FactroApiClient.UnitTests.Appointment
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment.Contracts;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Newtonsoft.Json;

    using Xunit;

    public partial class AppointmentApiTests
    {
        [Fact]
        public async Task UpdateAppointmentAsync_ValidRequest_ShouldReturnUpdatedAppointment()
        {
            // Arrange
            var existingAppointment = new GetAppointmentPayload
            {
                Id = Guid.NewGuid().ToString(),
                Subject = "TestSubject",
            };

            var updateAppointmentRequest = new UpdateAppointmentRequest
            {
                Subject = "NewSubject",
            };

            var expectedUpdatedAppointment = new UpdateAppointmentResponse
            {
                Id = existingAppointment.Id,
                Subject = updateAppointmentRequest.Subject,
            };

            var expectedContent = new StringContent(JsonConvert.SerializeObject(expectedUpdatedAppointment, this.fixture.JsonSerializerSettings));
            var response = new HttpResponseMessage
            {
                Content = expectedContent,
            };
            var appointmentApi = this.fixture.GetAppointmentApi(response);

            var updatedAppointment = new UpdateAppointmentResponse();

            // Act
            Func<Task> act = async () =>
                updatedAppointment = await appointmentApi.UpdateAppointmentAsync(existingAppointment.Id, updateAppointmentRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                updatedAppointment.Id.Should().Be(existingAppointment.Id);
                updatedAppointment.Subject.Should().Be(expectedUpdatedAppointment.Subject);
            }
        }

        [Theory]
        [MemberData(nameof(AppointmentApiTestFixture.InvalidAppointmentIds), MemberType = typeof(AppointmentApiTestFixture))]
        public async Task UpdateAppointmentAsync_InvalidAppointmentId_ShouldThrowArgumentNullException(string appointmentId)
        {
            // Arrange
            var appointmentApi = this.fixture.GetAppointmentApi();

            var updateAppointmentRequest = new UpdateAppointmentRequest();

            // Act
            Func<Task> act = async () => await appointmentApi.UpdateAppointmentAsync(appointmentId, updateAppointmentRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateAppointmentAsync_UnsuccessfulRequest_ShouldReturnNull()
        {
            // Arrange
            var appointmentId = Guid.NewGuid().ToString();
            var updateAppointmentRequest = new UpdateAppointmentRequest();

            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var appointmentApi = this.fixture.GetAppointmentApi(response);

            var result = new UpdateAppointmentResponse();

            // Act
            Func<Task> act = async () => result = await appointmentApi.UpdateAppointmentAsync(appointmentId, updateAppointmentRequest);

            // Assert
            await act.Should().NotThrowAsync();

            result.Should().BeNull();
        }
    }
}
