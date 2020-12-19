namespace FactroApiClient.UnitTests.ContactApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Contact.Contracts;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Newtonsoft.Json;

    using Xunit;

    public partial class ContactApiTests
    {
        [Fact]
        public async Task UpdateContactAsync_ValidRequest_ShouldReturnUpdatedContact()
        {
            // Arrange
            var existingContact = new GetContactPayload
            {
                Id = Guid.NewGuid().ToString(),
                Description = "TestDescription",
            };

            var updateContactRequest = new UpdateContactRequest
            {
                Description = "NewDescription",
            };

            var expectedUpdatedContact = new UpdateContactResponse
            {
                Id = existingContact.Id,
                Description = updateContactRequest.Description,
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedUpdatedContact, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var contactApi = this.fixture.GetContactApi(expectedResponse);

            var updateContactResponse = new UpdateContactResponse();

            // Act
            Func<Task> act = async () =>
                updateContactResponse = await contactApi.UpdateContactAsync(existingContact.Id, updateContactRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                updateContactResponse.Id.Should().Be(existingContact.Id);
                updateContactResponse.Description.Should().Be(expectedUpdatedContact.Description);
            }
        }

        [Theory]
        [MemberData(nameof(ContactApiTestFixture.InvalidContactIds), MemberType = typeof(ContactApiTestFixture))]
        public async Task UpdateContactAsync_InvalidContactId_ShouldThrowArgumentNullException(string contactId)
        {
            // Arrange
            var contactApi = this.fixture.GetContactApi();

            var updateContactRequest = new UpdateContactRequest();

            // Act
            Func<Task> act = async () => await contactApi.UpdateContactAsync(contactId, updateContactRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateContactAsync_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var contactId = Guid.NewGuid().ToString();

            var contactApi = this.fixture.GetContactApi();

            // Act
            Func<Task> act = async () => await contactApi.UpdateContactAsync(contactId, updateContactRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateContactAsync_UnsuccessfulRequest_ShouldThrowContactApiException()
        {
            // Arrange
            var contactId = Guid.NewGuid().ToString();

            var updateContactRequest = new UpdateContactRequest();

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
            Func<Task> act = async () => await contactApi.UpdateContactAsync(contactId, updateContactRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
