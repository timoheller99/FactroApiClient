namespace FactroApiClient.IntegrationTests.ContactApi
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Contact;
    using FactroApiClient.Contact.Contracts;

    using FluentAssertions;
    using FluentAssertions.Execution;

    using Xunit;

    public partial class ContactApiTests
    {
        [Fact]
        public async Task CreateContactAsync_ValidContact_ShouldStoreContact()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var contactApi = this.fixture.GetService<IContactApi>();

            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();
            var description = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createContactRequest = new CreateContactRequest(firstName, lastName)
            {
                Description = description,
            };

            var createContactResponse = default(CreateContactResponse);

            // Act
            Func<Task> act = async () => createContactResponse = await contactApi.CreateContactAsync(createContactRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var contacts = (await contactApi.GetContactsAsync())
                    .Where(x => x.Description.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                contacts.Should().ContainEquivalentOf(createContactResponse);
            }
        }

        [Fact]
        public async Task CreateContactAsync_TwoIdenticalContacts_ShouldStoreBothContacts()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var contactApi = this.fixture.GetService<IContactApi>();

            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();
            var description = $"{BaseTestFixture.TestPrefix}{Guid.NewGuid().ToString()}";

            var createContactRequest = new CreateContactRequest(firstName, lastName)
            {
                Description = description,
            };

            await contactApi.CreateContactAsync(createContactRequest);

            // Act
            Func<Task> act = async () => await contactApi.CreateContactAsync(createContactRequest);

            // Assert
            await act.Should().NotThrowAsync();

            using (new AssertionScope())
            {
                var contacts = (await contactApi.GetContactsAsync())
                    .Where(x => x.Description.StartsWith(BaseTestFixture.TestPrefix)).ToList();

                var matchingContacts = contacts.Where(x => x.Description == description);
                matchingContacts.Should().HaveCount(2);
            }
        }
    }
}
