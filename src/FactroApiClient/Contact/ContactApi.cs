namespace FactroApiClient.Contact
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Contact.Contracts;
    using FactroApiClient.Contact.Endpoints;
    using FactroApiClient.SharedContracts;

    using Newtonsoft.Json;

    public class ContactApi : IContactApi
    {
        private const string BaseClientName = "BaseClient";

        private readonly JsonSerializerSettings jsonSerializerSettings;

        private readonly HttpClient httpClient;

        public ContactApi(IHttpClientFactory httpClientFactory)
        {
            this.jsonSerializerSettings = SerializerSettings.JsonSerializerSettings;

            this.httpClient = httpClientFactory.CreateClient(BaseClientName);
        }

        public async Task<CreateContactResponse> CreateContactAsync(CreateContactRequest createContactRequest)
        {
            if (createContactRequest == null)
            {
                throw new ArgumentNullException(nameof(createContactRequest), $"{nameof(createContactRequest)} can not be null.");
            }

            if (createContactRequest.FirstName == null)
            {
                throw new ArgumentNullException(nameof(createContactRequest), $"{nameof(createContactRequest.FirstName)} can not be null.");
            }

            if (createContactRequest.LastName == null)
            {
                throw new ArgumentNullException(nameof(createContactRequest), $"{nameof(createContactRequest.LastName)} can not be null.");
            }

            var requestRoute = ContactApiEndpoints.Base.Create();

            var requestString = JsonConvert.SerializeObject(createContactRequest, this.jsonSerializerSettings);
            var requestContent = ApiHelpers.GetStringContent(requestString);

            var response = await this.httpClient.PostAsync(requestRoute, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new FactroApiException(
                    "Could not create contact.",
                    response.RequestMessage.RequestUri.ToString(),
                    response.StatusCode,
                    response.Content == null ? null : await response.Content.ReadAsStringAsync());
            }

            var responseContentString = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<CreateContactResponse>(
                    responseContentString,
                    this.jsonSerializerSettings);

            return result;
        }

        public async Task<IEnumerable<GetContactPayload>> GetContactsAsync()
        {
            var requestRoute = ContactApiEndpoints.Base.GetAll();

            var response = await this.httpClient.GetAsync(requestRoute);

            if (!response.IsSuccessStatusCode)
            {
                throw new FactroApiException(
                    "Could not fetch contacts.",
                    response.RequestMessage.RequestUri.ToString(),
                    response.StatusCode,
                    response.Content == null ? null : await response.Content.ReadAsStringAsync());
            }

            var responseContentString = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<List<GetContactPayload>>(
                    responseContentString,
                    this.jsonSerializerSettings);

            return result;
        }

        public async Task<GetContactByIdResponse> GetContactByIdAsync(string contactId)
        {
            if (string.IsNullOrWhiteSpace(contactId))
            {
                throw new ArgumentNullException(nameof(contactId), $"{nameof(contactId)} can not be null, empty or whitespace.");
            }

            var requestRoute = ContactApiEndpoints.Base.GetById(contactId);

            var response = await this.httpClient.GetAsync(requestRoute);

            if (!response.IsSuccessStatusCode)
            {
                throw new FactroApiException(
                    $"Could not get contact with id '{contactId}'.",
                    response.RequestMessage.RequestUri.ToString(),
                    response.StatusCode,
                    response.Content == null ? null : await response.Content.ReadAsStringAsync());
            }

            var responseContentString = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<GetContactByIdResponse>(
                    responseContentString,
                    this.jsonSerializerSettings);

            return result;
        }

        public async Task<UpdateContactResponse> UpdateContactAsync(string contactId, UpdateContactRequest updateContactRequest)
        {
            if (string.IsNullOrWhiteSpace(contactId))
            {
                throw new ArgumentNullException(nameof(contactId), $"{nameof(contactId)} can not be null, empty or whitespace.");
            }

            if (updateContactRequest == null)
            {
                throw new ArgumentNullException(nameof(updateContactRequest), $"{nameof(updateContactRequest)} can not be null.");
            }

            var requestRoute = ContactApiEndpoints.Base.Update(contactId);

            var requestString = JsonConvert.SerializeObject(updateContactRequest, this.jsonSerializerSettings);
            var requestContent = ApiHelpers.GetStringContent(requestString);

            var response = await this.httpClient.PutAsync(requestRoute, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new FactroApiException(
                    $"Could not update contact with id '{contactId}'.",
                    response.RequestMessage.RequestUri.ToString(),
                    response.StatusCode,
                    response.Content == null ? null : await response.Content.ReadAsStringAsync());
            }

            var responseContentString = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<UpdateContactResponse>(
                    responseContentString,
                    this.jsonSerializerSettings);

            return result;
        }

        public async Task<DeleteContactResponse> DeleteContactAsync(string contactId)
        {
            if (string.IsNullOrWhiteSpace(contactId))
            {
                throw new ArgumentNullException(nameof(contactId), $"{nameof(contactId)} can not be null, empty or whitespace.");
            }

            var requestRoute = ContactApiEndpoints.Base.Delete(contactId);

            var response = await this.httpClient.DeleteAsync(requestRoute);

            if (!response.IsSuccessStatusCode)
            {
                throw new FactroApiException(
                    $"Could not delete contact with id '{contactId}'.",
                    response.RequestMessage.RequestUri.ToString(),
                    response.StatusCode,
                    response.Content == null ? null : await response.Content.ReadAsStringAsync());
            }

            var responseContentString = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<DeleteContactResponse>(
                    responseContentString,
                    this.jsonSerializerSettings);

            return result;
        }
    }
}
