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
        public async Task DeleteContactAsync_ExistingContact_ShouldDeleteExistingContact()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var contactApi = this.fixture.GetService<IContactApi>();

            var existingContact = await this.fixture.CreateTestContactAsync(contactApi);

            var deleteContactResponse = new DeleteContactResponse();

            // Act
            Func<Task> act = async () => deleteContactResponse = await contactApi.DeleteContactAsync(existingContact.Id);

            // Assert
            await act.Should().NotThrowAsync();

            deleteContactResponse.Should().BeEquivalentTo(existingContact);

            await this.fixture.ClearFactroInstanceAsync();
        }

        [Fact]
        public async Task DeleteContactAsync_NotExistingContact_ShouldThrowFactroApiException()
        {
            // Arrange
            await this.fixture.ClearFactroInstanceAsync();

            var contactApi = this.fixture.GetService<IContactApi>();

            var deleteContactResponse = default(DeleteContactResponse);

            // Act
            Func<Task> act = async () => deleteContactResponse = await contactApi.DeleteContactAsync(Guid.NewGuid().ToString());

            // Assert
            await act.Should().ThrowAsync<FactroApiException>();

            deleteContactResponse.Should().BeNull();

            await this.fixture.ClearFactroInstanceAsync();
        }
    }
}
