namespace FactroApiClient.IntegrationTests.ContactApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Contact;
    using FactroApiClient.Contact.Contracts;

    using FluentAssertions;

    using Xunit;

    public partial class ContactApiTests
    {
        [Fact]
        public async Task GetContactsAsync_ExistingContacts_ShouldReturnExistingContacts()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var contactApi = this.fixture.GetService<IContactApi>();

            var existingContacts = new List<CreateContactResponse>();

            const int contactCount = 5;

            for (var i = 0; i < contactCount; i++)
            {
                existingContacts.Add(await this.fixture.CreateTestContactAsync(contactApi));
            }

            var getContactsResponse = new List<GetContactPayload>();

            // Act
            Func<Task> act = async () => getContactsResponse = (await contactApi.GetContactsAsync()).ToList();

            // Assert
            await act.Should().NotThrowAsync();

            foreach (var existingContact in existingContacts)
            {
                getContactsResponse.Should().ContainEquivalentOf(existingContact);
            }
        }
    }
}
