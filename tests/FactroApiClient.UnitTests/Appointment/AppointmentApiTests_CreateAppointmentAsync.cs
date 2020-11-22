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
        public async Task CreateAppointment_ValidModel_ShouldReceiveExpectedAppointment()
        {
            // Arrange
            var appointmentToBeCreated = new CreateAppointmentRequest(Guid.NewGuid().ToString(), DateTime.UtcNow, DateTime.UtcNow.AddHours(1), "TestSubject");
            var expectedAppointment = new CreateAppointmentResponse
            {
                EmployeeId = appointmentToBeCreated.EmployeeId,
                StartDate = appointmentToBeCreated.StartDate,
                EndDate = appointmentToBeCreated.EndDate,
                Subject = appointmentToBeCreated.Subject,
            };
            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(expectedAppointment, this.fixture.JsonSerializerSettings));
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = expectedResponseContent,
            };
            var appointmentApi = this.fixture.GetAppointmentApi(response);

            var createdAppointment = default(CreateAppointmentResponse);

            // Act
            Func<Task> act = async () => createdAppointment = await appointmentApi.CreateAppointmentAsync(appointmentToBeCreated);

            // Assert
            await act.Should().NotThrowAsync();

            createdAppointment.Should().BeEquivalentTo(expectedAppointment);
        }

        [Fact]
        public async Task CreateAppointment_NullModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var appointmentApi = this.fixture.GetAppointmentApi();

            // Act
            Func<Task> act = async () => await appointmentApi.CreateAppointmentAsync(createAppointmentRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(AppointmentApiTestFixture.InvalidEmployeeIds), MemberType = typeof(AppointmentApiTestFixture))]
        public async Task CreateAppointmentAsync_InvalidEmployeeId_ShouldThrowArgumentNullException(string employeeId)
        {
            // Arrange
            var appointmentToBeCreated = new CreateAppointmentRequest(employeeId, DateTime.UtcNow, DateTime.UtcNow.AddHours(1), "TestSubject");
            var appointmentApi = this.fixture.GetAppointmentApi();

            // Act
            Func<Task> act = async () => await appointmentApi.CreateAppointmentAsync(appointmentToBeCreated);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateAppointmentAsync_NullSubject_ShouldThrowArgumentNullException()
        {
            // Arrange
            var appointmentToBeCreated = new CreateAppointmentRequest(Guid.NewGuid().ToString(), DateTime.UtcNow, DateTime.UtcNow.AddHours(1), null);

            var appointmentApi = this.fixture.GetAppointmentApi();

            // Act
            Func<Task> act = async () => await appointmentApi.CreateAppointmentAsync(appointmentToBeCreated);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateAppointment_UnsuccessfulRequest_ResultShouldBeNull()
        {
            // Arrange
            var appointmentToBeCreated = new CreateAppointmentRequest(Guid.NewGuid().ToString(), DateTime.UtcNow, DateTime.UtcNow.AddHours(1), "TestSubject");

            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var appointmentApi = this.fixture.GetAppointmentApi(response);

            var result = new CreateAppointmentResponse();

            // Act
            Func<Task> act = async () => result = await appointmentApi.CreateAppointmentAsync(appointmentToBeCreated);

            // Assert
            await act.Should().NotThrowAsync();

            result.Should().BeNull();
        }
    }
}
