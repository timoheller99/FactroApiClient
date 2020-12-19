namespace FactroApiClient.Company
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Company.Contracts.Basic;
    using FactroApiClient.Company.Contracts.CompanyTag;
    using FactroApiClient.Company.Endpoints;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    public class CompanyApi : ICompanyApi
    {
        private const string BaseClientName = "BaseClient";

        private readonly ILogger<CompanyApi> logger;

        private readonly IHttpClientFactory httpClientFactory;

        private readonly JsonSerializerSettings jsonSerializerSettings;

        public CompanyApi(ILogger<CompanyApi> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializerSettings = SerializerSettings.JsonSerializerSettings;
        }

        public async Task<CreateCompanyResponse> CreateCompanyAsync(CreateCompanyRequest createCompanyRequest)
        {
            if (createCompanyRequest == null)
            {
                throw new ArgumentNullException(nameof(createCompanyRequest), $"{nameof(createCompanyRequest)} can not be null.");
            }

            if (createCompanyRequest.Name == null)
            {
                throw new ArgumentNullException(nameof(createCompanyRequest), $"{nameof(createCompanyRequest.Name)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = CompanyApiEndpoints.Base.Create();

                var requestString = JsonConvert.SerializeObject(createCompanyRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PostAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not create company: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<CreateCompanyResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<IEnumerable<GetCompanyPayload>> GetCompaniesAsync()
        {
            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRute = CompanyApiEndpoints.Base.GetAll();

                var response = await client.GetAsync(requestRute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch companies: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<List<GetCompanyPayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<GetCompanyByIdResponse> GetCompanyByIdAsync(string companyId)
        {
            if (string.IsNullOrWhiteSpace(companyId))
            {
                throw new ArgumentNullException(nameof(companyId), $"{nameof(companyId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = CompanyApiEndpoints.Base.GetById(companyId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch company with id '{CompanyId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        companyId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<GetCompanyByIdResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<UpdateCompanyResponse> UpdateCompanyAsync(string companyId, UpdateCompanyRequest updateCompanyRequest)
        {
            if (string.IsNullOrWhiteSpace(companyId))
            {
                throw new ArgumentNullException(nameof(companyId), $"{nameof(companyId)} can not be null, empty or whitespace.");
            }

            if (updateCompanyRequest == null)
            {
                throw new ArgumentNullException(nameof(updateCompanyRequest), $"{nameof(updateCompanyRequest)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = CompanyApiEndpoints.Base.Update(companyId);

                var requestString = JsonConvert.SerializeObject(updateCompanyRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not update company with id '{CompanyId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        companyId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<UpdateCompanyResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<DeleteCompanyResponse> DeleteCompanyAsync(string companyId)
        {
            if (string.IsNullOrWhiteSpace(companyId))
            {
                throw new ArgumentNullException(nameof(companyId), $"{nameof(companyId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = CompanyApiEndpoints.Base.Delete(companyId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not delete company with id '{CompanyId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        companyId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<DeleteCompanyResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<CreateCompanyTagResponse> CreateCompanyTagAsync(CreateCompanyTagRequest createCompanyTagRequest)
        {
            if (createCompanyTagRequest == null)
            {
                throw new ArgumentNullException(nameof(createCompanyTagRequest), $"{nameof(createCompanyTagRequest)} can not be null.");
            }

            if (createCompanyTagRequest.Name == null)
            {
                throw new ArgumentNullException(nameof(createCompanyTagRequest), $"{nameof(createCompanyTagRequest.Name)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = CompanyApiEndpoints.Tag.Create();

                var requestString = JsonConvert.SerializeObject(createCompanyTagRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PostAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not create company tag: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<CreateCompanyTagResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<IEnumerable<GetCompanyTagPayload>> GetCompanyTagsAsync()
        {
            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRute = CompanyApiEndpoints.Tag.GetAll();

                var response = await client.GetAsync(requestRute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch company tags: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<List<GetCompanyTagPayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<IEnumerable<GetCompanyTagAssociationPayload>> GetTagsOfCompanyAsync(string companyId)
        {
            if (string.IsNullOrWhiteSpace(companyId))
            {
                throw new ArgumentNullException(nameof(companyId), $"{nameof(companyId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRute = CompanyApiEndpoints.Tag.GetById(companyId);

                var response = await client.GetAsync(requestRute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch company tags for company with ID '{CompanyId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        companyId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<List<GetCompanyTagAssociationPayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<DeleteCompanyTagResponse> DeleteCompanyTagAsync(string companyTagId)
        {
            if (string.IsNullOrWhiteSpace(companyTagId))
            {
                throw new ArgumentNullException(nameof(companyTagId), $"{nameof(companyTagId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = CompanyApiEndpoints.Tag.Delete(companyTagId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not delete company tag with id '{CompanyTagId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        companyTagId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<DeleteCompanyTagResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task AddTagToCompanyAsync(string companyId, AddCompanyTagAssociationRequest addCompanyTagAssociationRequest)
        {
            if (string.IsNullOrWhiteSpace(companyId))
            {
                throw new ArgumentNullException(nameof(companyId), $"{nameof(companyId)} can not be null, empty or whitespace.");
            }

            if (addCompanyTagAssociationRequest == null)
            {
                throw new ArgumentNullException(nameof(addCompanyTagAssociationRequest), $"{nameof(addCompanyTagAssociationRequest)} can not be null.");
            }

            if (addCompanyTagAssociationRequest.TagId == null)
            {
                throw new ArgumentNullException(nameof(addCompanyTagAssociationRequest), $"{nameof(addCompanyTagAssociationRequest.TagId)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = CompanyApiEndpoints.Tag.Add(companyId);

                var requestString = JsonConvert.SerializeObject(addCompanyTagAssociationRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not add company tag '{CompanyTagId}' to company '{CompanyId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        addCompanyTagAssociationRequest.TagId,
                        companyId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
        }

        public async Task RemoveTagFromCompanyAsync(string companyId, string companyTagId)
        {
            if (string.IsNullOrWhiteSpace(companyId))
            {
                throw new ArgumentNullException(nameof(companyId), $"{nameof(companyId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(companyTagId))
            {
                throw new ArgumentNullException(nameof(companyTagId), $"{nameof(companyTagId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = CompanyApiEndpoints.Tag.Remove(companyId, companyTagId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not remove company tag '{CompanyTagId}' to company '{CompanyId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        companyTagId,
                        companyId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
        }
    }
}
