namespace FactroApiClient.IntegrationTests.ContactApi
{
    using System;
    using System.Threading.Tasks;

    using FactroApiClient.Contact;
    using FactroApiClient.Contact.Contracts;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;

    using Xunit;

    public partial class ContactApiTests
    {
        [Fact]
        public async Task GetContactAsync_ExistingContact_ShouldReturnExpectedContact()
        {
            // Arrange
            var contactApi = this.fixture.GetService<IContactApi>();

            var existingContact = await this.fixture.CreateTestContactAsync(contactApi);

            var getContactByIdResponse = new GetContactByIdResponse();

            // Act
            Func<Task> act = async () => getContactByIdResponse = await contactApi.GetContactByIdAsync(existingContact.Id);

            // Assert
            await act.Should().NotThrowAsync();

            getContactByIdResponse.Should().BeEquivalentTo(existingContact);
        }

        [Fact]
        public async Task GetContactAsync_NotExistingContact_ResultThrowFactroApiException()
        {
            // Arrange
            var contactApi = this.fixture.GetService<IContactApi>();

            var getContactByIdResponse = default(GetContactByIdResponse);

            // Act
            Func<Task> act = async () => getContactByIdResponse = await contactApi.GetContactByIdAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            getContactByIdResponse.Should().BeNull();
        }
    }
}
