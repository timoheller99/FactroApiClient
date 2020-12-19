namespace FactroApiClient.UnitTests.ContactApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Contact.Contracts;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ContactApiTests
    {
        [Fact]
        public async Task GetContactsAsync_ValidRequest_ShouldReturnContacts()
        {
            // Arrange
            var existingContactsList = new List<GetContactPayload>
            {
                new GetContactPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
                new GetContactPayload
                {
                    Id = Guid.NewGuid().ToString(),
                },
            };

            var expectedResponseContent = new StringContent(JsonConvert.SerializeObject(existingContactsList, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var contactApi = this.fixture.GetContactApi(expectedResponse);

            var getContactsResponse = new List<GetContactPayload>();

            // Act
            Func<Task> act = async () => getContactsResponse = (await contactApi.GetContactsAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            getContactsResponse.Should().HaveCount(existingContactsList.Count);
        }

        [Fact]
        public async Task GetContactsAsync_UnsuccessfulRequest_ShouldThrowContactApiException()
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

            var contactApi = this.fixture.GetContactApi(expectedResponse);

            // Act
            Func<Task> act = async () => await contactApi.GetContactsAsync();

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
