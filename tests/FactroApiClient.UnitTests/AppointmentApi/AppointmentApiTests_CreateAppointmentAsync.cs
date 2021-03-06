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
        public async Task CreateAppointment_ValidRequest_ShouldReturnCreatedAppointment()
        {
            // Arrange
            var employeeId = Guid.NewGuid().ToString();
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddHours(1);
            var subject = Guid.NewGuid().ToString();

            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, startDate, endDate, subject);

            var expectedAppointment = new CreateAppointmentResponse
            {
                EmployeeId = createAppointmentRequest.EmployeeId,
                StartDate = createAppointmentRequest.StartDate,
                EndDate = createAppointmentRequest.EndDate,
                Subject = createAppointmentRequest.Subject,
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedAppointment, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var appointmentApi = this.fixture.GetAppointmentApi(expectedResponse);

            var createAppointmentResponse = default(CreateAppointmentResponse);

            // Act
            Func<Task> act = async () => createAppointmentResponse = await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createAppointmentResponse.Should().BeEquivalentTo(expectedAppointment);
        }

        [Fact]
        public async Task CreateAppointment_NullRequestModel_ShouldThrowArgumentNullException()
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
        public async Task CreateAppointmentAsync_InvalidRequestModelEmployeeId_ShouldThrowArgumentNullException(string employeeId)
        {
            // Arrange
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddHours(1);
            var subject = Guid.NewGuid().ToString();

            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, startDate, endDate, subject);

            var appointmentApi = this.fixture.GetAppointmentApi();

            // Act
            Func<Task> act = async () => await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateAppointmentAsync_NullRequestModelSubject_ShouldThrowArgumentNullException()
        {
            // Arrange
            var employeeId = Guid.NewGuid().ToString();
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddHours(1);

            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, startDate, endDate, null);

            var appointmentApi = this.fixture.GetAppointmentApi();

            // Act
            Func<Task> act = async () => await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateAppointment_UnsuccessfulRequest_ShouldThrowAppointmentApiException()
        {
            // Arrange
            var employeeId = Guid.NewGuid().ToString();
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddHours(1);
            var subject = Guid.NewGuid().ToString();

            var createAppointmentRequest = new CreateAppointmentRequest(employeeId, startDate, endDate, subject);

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
            Func<Task> act = async () => await appointmentApi.CreateAppointmentAsync(createAppointmentRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
