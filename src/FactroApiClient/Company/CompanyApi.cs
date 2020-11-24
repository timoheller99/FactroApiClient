namespace FactroApiClient.Company
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Company.Contracts.Basic;
    using FactroApiClient.Company.Contracts.CompanyTag;

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
                const string requestRoute = ApiEndpoints.Company.Create;

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
                var response = await client.GetAsync(ApiEndpoints.Company.GetAll);

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
                var requestRoute = string.Format(CultureInfo.InvariantCulture, ApiEndpoints.Company.GetById, companyId);

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

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = string.Format(CultureInfo.InvariantCulture, ApiEndpoints.Company.Update, companyId);

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
                var requestRoute = string.Format(CultureInfo.InvariantCulture, ApiEndpoints.Company.Delete, companyId);

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
            // TODO: Will be implemented in a future pull request
            return new CreateCompanyTagResponse();
        }

        public async Task<IEnumerable<GetCompanyTagPayload>> GetCompanyTagsAsync()
        {
            // TODO: Will be implemented in a future pull request
            return new List<GetCompanyTagPayload>();
        }

        public async Task<IEnumerable<GetCompanyTagPayload>> GetCompanyTagsAsync(string companyId)
        {
            // TODO: Will be implemented in a future pull request
            return new List<GetCompanyTagPayload>();
        }

        public async Task<DeleteCompanyTagResponse> DeleteCompanyTagAsync(string companyTagId)
        {
            // TODO: Will be implemented in a future pull request
            return new DeleteCompanyTagResponse();
        }

        public async Task AddCompanyTagAsync(string companyId, AddCompanyTagAssociationRequest addCompanyTagAssociationRequest)
        {
            // TODO: Will be implemented in a future pull request
        }

        public async Task RemoveCompanyTagAsync(string companyId, string companyTagId)
        {
            // TODO: Will be implemented in a future pull request
        }
    }
}
