namespace FactroApiClient.IntegrationTests.ContactApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FactroApiClient.Contact;
    using FactroApiClient.Contact.Contracts;

    public sealed class ContactApiTestFixture : BaseTestFixture
    {
        public ContactApiTestFixture()
        {
            this.ClearFactroInstanceAsync().GetAwaiter().GetResult();
        }

        public async Task<CreateContactResponse> CreateTestContactAsync(IContactApi contactApi)
        {
            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();
            var description = $"{TestPrefix}{Guid.NewGuid().ToString()}";

            var createContactRequest = new CreateContactRequest(firstName, lastName)
            {
                Description = description,
            };

            var createContactResponse = await contactApi.CreateContactAsync(createContactRequest);

            return createContactResponse;
        }

        public async Task<IEnumerable<GetContactPayload>> GetContactsAsync(IContactApi companyApi)
        {
            return (await companyApi.GetContactsAsync()).Where(x => x.Description.StartsWith(TestPrefix));
        }

        protected override async Task ClearFactroInstanceAsync()
        {
            await this.ClearContactsAsync();
        }

        private async Task ClearContactsAsync()
        {
            var service = this.GetService<IContactApi>();

            var contacts = await service.GetContactsAsync();

            var contactsToRemove = contacts.Where(x => x.Description.StartsWith(TestPrefix));

            var tasks = contactsToRemove.Select(x => service.DeleteContactAsync(x.Id));

            await Task.WhenAll(tasks);
        }
    }
}
