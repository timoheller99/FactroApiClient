namespace FactroApiClient.IntegrationTests.ContactApi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Contact;
    using FactroApiClient.Contact.Contracts;
    using FactroApiClient.SharedContracts;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Xunit;

    public partial class ContactApiTests
    {
        [Fact]
        public async Task UpdateContactAsync_ValidUpdate_ShouldReturnUpdatedContact()
        {
            // Arrange
            var contactApi = this.fixture.GetService<IContactApi>();

            var existingContact = await this.fixture.CreateTestContactAsync(contactApi);

            var updatedDescription = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";
            var updateContactRequest = new UpdateContactRequest
            {
                Description = updatedDescription,
            };

            var updateContactResponse = new UpdateContactResponse();

            // Act
            Func<Task> act = async () => updateContactResponse = await contactApi.UpdateContactAsync(existingContact.Id, updateContactRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var contacts = (await this.fixture.GetContactsAsync(contactApi)).ToList();

                contacts.Should().ContainEquivalentOf(updateContactResponse);

                contacts.Single(x => x.Id == existingContact.Id).Description.Should().Be(updatedDescription);
            }
        }

        [Fact]
        public async Task UpdateContactAsync_InvalidUpdate_ShouldNotUpdateContact()
        {
            // Arrange
            var contactApi = this.fixture.GetService<IContactApi>();

            var existingContact = await this.fixture.CreateTestContactAsync(contactApi);

            const string updatedDescription = null;
            var updateContactRequest = new UpdateContactRequest
            {
                Description = updatedDescription,
            };

            // Act
            Func<Task> act = async () => await contactApi.UpdateContactAsync(existingContact.Id, updateContactRequest);

            // Assert
            await act.Should().NotThrowAsync();

            var contacts = await this.fixture.GetContactsAsync(contactApi);

            contacts.Single(x => x.Id == existingContact.Id).Should().BeEquivalentTo(existingContact);
        }

        [Fact]
        public async Task UpdateContactAsync_NotExistingContactId_ShouldThrowFactroApiException()
        {
            // Arrange
            var contactApi = this.fixture.GetService<IContactApi>();

            var updatedDescription = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";
            var updateContactRequest = new UpdateContactRequest
            {
                Description = updatedDescription,
            };

            var updateContactResponse = default(UpdateContactResponse);

            // Act
            Func<Task> act = async () => updateContactResponse = await contactApi.UpdateContactAsync(Guid.NewGuid().ToString(), updateContactRequest);

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            using (new AssertionScope())
            {
                var contacts = await this.fixture.GetContactsAsync(contactApi);

                contacts.All(x => x.Description != updatedDescription).Should().BeTrue();

                updateContactResponse.Should().BeNull();
            }
        }
    }
}
