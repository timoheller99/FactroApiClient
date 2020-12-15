namespace FactroApiClient.Contact
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FactroApiClient.Contact.Contracts;

    public interface IContactApi
    {
        /// <summary>
        /// Creates a contact.
        /// </summary>
        /// <param name="createContactRequest">Request model to create a new contact.</param>
        /// <returns>Returns the created contact.</returns>
        public Task<CreateContactResponse> CreateContactAsync(CreateContactRequest createContactRequest);

        /// <summary>
        /// Fetches the contacts visible to the requesting user.
        /// </summary>
        /// <returns>Returns the list of contacts visible to the requesting user.</returns>
        public Task<IEnumerable<GetContactPayload>> GetContactsAsync();

        /// <summary>
        /// Fetches the contact with the given contact id.
        /// </summary>
        /// <param name="contactId">Id of the contact to fetch.</param>
        /// <returns>Returns the contact with the given <paramref name="contactId"/>.</returns>
        public Task<GetContactByIdResponse> GetContactByIdAsync(string contactId);

        /// <summary>
        /// Updates the contact with the given contact id.
        /// </summary>
        /// <param name="contactId">Id of the contact to fetch.</param>
        /// <param name="updateContactRequest">Request model to update the contact.</param>
        /// <returns>Returns the updated contact.</returns>
        public Task<UpdateContactResponse> UpdateContactAsync(string contactId, UpdateContactRequest updateContactRequest);

        /// <summary>
        /// Deletes the contact with the given contact id.
        /// </summary>
        /// <param name="contactId">Id of the contact to fetch.</param>
        /// <returns>Returns the deleted contact.</returns>
        public Task<DeleteContactResponse> DeleteContactAsync(string contactId);
    }
}
