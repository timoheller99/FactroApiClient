namespace FactroApiClient.UnitTests.ContactApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Contact.Contracts;

    using FluentAssertions;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ContactApiTests
    {
        [Fact]
        public async Task GetContactAsync_ValidId_ShouldReturnExpectedContact()
        {
            // Arrange
            var existingContact = new GetContactPayload
            {
                Id = Guid.NewGuid().ToString(),
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(existingContact, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var contactApi = this.fixture.GetContactApi(expectedResponse);

            var getContactByIdResponse = new GetContactByIdResponse();

            // Act
            Func<Task> act = async () => getContactByIdResponse = await contactApi.GetContactByIdAsync(existingContact.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getContactByIdResponse.Id.Should().Be(existingContact.Id);
        }

        [Theory]
        [MemberData(nameof(ContactApiTestFixture.InvalidContactIds), MemberType = typeof(ContactApiTestFixture))]
        public async Task GetContactAsync_InvalidContactId_ShouldThrowArgumentNullException(string contactId)
        {
            // Arrange
            var contactApi = this.fixture.GetContactApi();

            // Act
            Func<Task> act = async () => await contactApi.GetContactByIdAsync(contactId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetContactAsync_UnsuccessfulRequest_ShouldReturnNull()
        {
            // Arrange
            var contactId = Guid.NewGuid().ToString();

            var expectedResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                RequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://www.mock-web-address.com"),
                },
            };

            var contactApi = this.fixture.GetContactApi(expectedResponse);

            var getContactByIdResponse = new GetContactByIdResponse();

            // Act
            Func<Task> act = async () => getContactByIdResponse = await contactApi.GetContactByIdAsync(contactId);

            // Assert
            await act.Should().NotThrowAsync();

            getContactByIdResponse.Should().BeNull();
        }
    }
}
