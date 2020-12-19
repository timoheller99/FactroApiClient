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
        public async Task CreateContact_ValidRequest_ShouldReturnCreatedContact()
        {
            // Arrange
            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();

            var createContactRequest = new CreateContactRequest(firstName, lastName);

            var expectedContact = new CreateContactResponse
            {
                FirstName = firstName,
                LastName = lastName,
            };

            var expectedResponseContent =
                new StringContent(JsonConvert.SerializeObject(expectedContact, this.fixture.JsonSerializerSettings));

            var expectedResponse = new HttpResponseMessage
            {
                Content = expectedResponseContent,
            };

            var contactApi = this.fixture.GetContactApi(expectedResponse);

            var createContactResponse = default(CreateContactResponse);

            // Act
            Func<Task> act = async () => createContactResponse = await contactApi.CreateContactAsync(createContactRequest);

            // Assert
            await act.Should().NotThrowAsync();

            createContactResponse.Should().BeEquivalentTo(expectedContact);
        }

        [Fact]
        public async Task CreateContact_NullRequestModel_ShouldThrowArgumentNullException()
        {
            // Arrange
            var contactApi = this.fixture.GetContactApi();

            // Act
            Func<Task> act = async () => await contactApi.CreateContactAsync(createContactRequest: null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateContactAsync_NullFirstName_ShouldThrowArgumentNullException()
        {
            // Arrange
            var lastName = Guid.NewGuid().ToString();

            var createContactRequest = new CreateContactRequest(firstName: null, lastName);

            var contactApi = this.fixture.GetContactApi();

            // Act
            Func<Task> act = async () => await contactApi.CreateContactAsync(createContactRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateContactAsync_NullLastName_ShouldThrowArgumentNullException()
        {
            // Arrange
            var firstName = Guid.NewGuid().ToString();

            var createContactRequest = new CreateContactRequest(firstName, lastName: null);

            var contactApi = this.fixture.GetContactApi();

            // Act
            Func<Task> act = async () => await contactApi.CreateContactAsync(createContactRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateContact_UnsuccessfulRequest_ShouldThrowContactApiException()
        {
            // Arrange
            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();

            var createContactRequest = new CreateContactRequest(firstName, lastName);

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
            Func<Task> act = async () => await contactApi.CreateContactAsync(createContactRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();
        }
    }
}
