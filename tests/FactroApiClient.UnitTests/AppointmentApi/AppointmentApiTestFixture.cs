namespace FactroApiClient.UnitTests.AppointmentApi
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment;

    using Moq;
    using Moq.Protected;

    public class AppointmentApiTestFixture : BaseTestFixture
    {
        public static readonly IEnumerable<object[]> InvalidEmployeeIds = new List<object[]>
        {
            new object[] { null },
            new object[] { string.Empty },
            new object[] { " " },
        };

        public static readonly IEnumerable<object[]> InvalidAppointmentIds = new List<object[]>
        {
            new object[] { null },
            new object[] { string.Empty },
            new object[] { " " },
        };

        public AppointmentApi GetAppointmentApi(HttpResponseMessage response = null)
        {
            var httpClientFactoryMock = this.GetHttpClientFactoryMock(response);

            return new AppointmentApi(httpClientFactoryMock.Object);
        }

        private HttpClient GetHttpClient(HttpMessageHandler messageHandler)
        {
            var httpClient = messageHandler == null ? new HttpClient() : new HttpClient(messageHandler);

            httpClient.BaseAddress = new Uri("http://www.mock-web-address.com");

            return httpClient;
        }

        private Mock<IHttpClientFactory> GetHttpClientFactoryMock(HttpResponseMessage response)
        {
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            if (response != null)
            {
                mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(response);
            }

            var httpClient = this.GetHttpClient(mockHttpMessageHandler.Object);

            mockHttpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            return mockHttpClientFactory;
        }
    }
}
