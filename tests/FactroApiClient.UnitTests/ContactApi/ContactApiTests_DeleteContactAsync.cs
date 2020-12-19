namespace FactroApiClient.UnitTests.ContactApi
{
    using System;
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
        public async Task DeleteContactAsync_ValidId_ShouldReturnDeletedContact()
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

            var deleteContactResponse = new DeleteContactResponse();

            // Act
            Func<Task> act = async () => deleteContactResponse = await contactApi.DeleteContactAsync(existingContact.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deleteContactResponse.Id.Should().Be(existingContact.Id);
        }

        [Theory]
        [MemberData(nameof(ContactApiTestFixture.InvalidContactIds), MemberType = typeof(ContactApiTestFixture))]
        public async Task DeleteContactAsync_InvalidContactId_ShouldThrowArgumentNullException(string contactId)
        {
            // Arrange
            var contactApi = this.fixture.GetContactApi();

            // Act
            Func<Task> act = async () => await contactApi.DeleteContactAsync(contactId);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteContactAsync_UnsuccessfulRequest_ShouldThrowContactApiException()
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
            Func<Task> act = async () => await contactApi.DeleteContactAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
