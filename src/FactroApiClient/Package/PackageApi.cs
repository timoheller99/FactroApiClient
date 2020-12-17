namespace FactroApiClient.Package
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Endpoints;
    using FactroApiClient.Package.Contracts;
    using FactroApiClient.Package.Contracts.AccessRights;
    using FactroApiClient.Package.Contracts.Association;
    using FactroApiClient.Package.Contracts.Comment;
    using FactroApiClient.Package.Contracts.Document;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    public class PackageApi : IPackageApi
    {
        private const string BaseClientName = "BaseClient";

        private readonly ILogger<PackageApi> logger;

        private readonly IHttpClientFactory httpClientFactory;

        private readonly JsonSerializerSettings jsonSerializerSettings;

        public PackageApi(ILogger<PackageApi> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializerSettings = SerializerSettings.JsonSerializerSettings;
        }

        public async Task<CreatePackageResponse> CreatePackageAsync(string projectId, CreatePackageRequest createPackageRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (createPackageRequest == null)
            {
                throw new ArgumentNullException(nameof(createPackageRequest), $"{nameof(createPackageRequest)} can not be null.");
            }

            if (createPackageRequest.Title == null)
            {
                throw new ArgumentNullException(nameof(createPackageRequest), $"{nameof(createPackageRequest.Title)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Package.Create(projectId);

                var requestString = JsonConvert.SerializeObject(createPackageRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PostAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not create package: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<CreatePackageResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<IEnumerable<GetPackagePayload>> GetPackagesAsync(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Package.GetAll(projectId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch packages: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<List<GetPackagePayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<IEnumerable<GetPackagePayload>> GetPackagesOfProjectAsync(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Package.GetByProject(projectId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch packages of project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<List<GetPackagePayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<GetPackageByIdResponse> GetPackageByIdAsync(string packageId)
        {
            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Package.GetById(packageId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch package with id '{PackageId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        packageId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<GetPackageByIdResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<GetPackageByIdResponse> GetPackageByIdAsync(string projectId, string packageId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Package.GetById(projectId, packageId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch package with id '{PackageId}' in project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        packageId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<GetPackageByIdResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<UpdatePackageResponse> UpdatePackageAsync(string projectId, string packageId, UpdatePackageRequest updatePackageRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (updatePackageRequest == null)
            {
                throw new ArgumentNullException(nameof(updatePackageRequest), $"{nameof(updatePackageRequest)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Package.Update(projectId, packageId);

                var requestString = JsonConvert.SerializeObject(updatePackageRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not update package with id '{PackageId}' inside project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        packageId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<UpdatePackageResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<DeletePackageResponse> DeletePackageAsync(string projectId, string packageId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Package.Delete(projectId, packageId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not delete package with id '{PackageId}' inside project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        packageId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<DeletePackageResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<CreatePackageCommentResponse> CreateCommentAsync(string projectId, string packageId, CreatePackageCommentRequest createPackageCommentRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (createPackageCommentRequest == null)
            {
                throw new ArgumentNullException(nameof(createPackageCommentRequest), $"{nameof(createPackageCommentRequest)} can not be null.");
            }

            if (createPackageCommentRequest.Text == null)
            {
                throw new ArgumentNullException(nameof(createPackageCommentRequest), $"{nameof(createPackageCommentRequest.Text)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.PackageComment.Create(projectId, packageId);

                var requestString = JsonConvert.SerializeObject(createPackageCommentRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PostAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not create comment in package with id '{PackageId}' in project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        packageId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<CreatePackageCommentResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<IEnumerable<GetPackageCommentPayload>> GetCommentsAsync(string projectId, string packageId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.PackageComment.GetAll(projectId, packageId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch comments of package with id '{PackageId}' in project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        packageId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<List<GetPackageCommentPayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<DeletePackageCommentResponse> DeleteCommentAsync(string projectId, string packageId, string commentId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(commentId))
            {
                throw new ArgumentNullException(nameof(commentId), $"{nameof(commentId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.PackageComment.Delete(projectId, packageId, commentId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not delete comment with id '{CommentId}' in package with id '{PackageId}' in project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        commentId,
                        packageId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<DeletePackageCommentResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task SetCompanyAsync(string projectId, string packageId, SetCompanyAssociationRequest setCompanyAssociationRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (setCompanyAssociationRequest == null)
            {
                throw new ArgumentNullException(nameof(setCompanyAssociationRequest), $"{nameof(setCompanyAssociationRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(setCompanyAssociationRequest.CompanyId))
            {
                throw new ArgumentNullException(nameof(setCompanyAssociationRequest), $"{nameof(setCompanyAssociationRequest.CompanyId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.PackageAssociation.SetCompany(projectId, packageId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not set company of package with id '{PackageId}' in project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        packageId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
        }

        public async Task SetContactAsync(string projectId, string packageId, SetContactAssociationRequest setContactAssociationRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (setContactAssociationRequest == null)
            {
                throw new ArgumentNullException(nameof(setContactAssociationRequest), $"{nameof(setContactAssociationRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(setContactAssociationRequest.ContactId))
            {
                throw new ArgumentNullException(nameof(setContactAssociationRequest), $"{nameof(setContactAssociationRequest.ContactId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.PackageAssociation.SetCompany(projectId, packageId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not set contact of package with id '{PackageId}' in project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        packageId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
        }

        public async Task MoveToPackageAsync(string projectId, string packageId, SetPackageAssociationRequest setPackageAssociationRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task MoveToProjectAsync(string projectId, string packageId, SetProjectAssociationRequest setPackageAssociationRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<GetPackageDocumentPayload>> GetDocumentsAsync(string projectId, string packageId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetPackageDocumentPayload> UploadDocumentAsync(string projectId, string packageId, byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DeletePackageDocumentResponse> DeleteDocumentAsync(string projectId, string packageId, string documentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetPackageReadRightsResponse> GetReadRightsAsync(string projectId, string packageId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AddPackageReadRightsForUserResponse> GrantReadRightsToUserAsync(string projectId, string packageId,
            AddPackageReadRightsForUserRequest addPackageReadRightsForUserRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task RevokeReadRightsFromUserAsync(string projectId, string packageId, string employeeId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetPackageWriteRightsResponse> GetWriteRightsAsync(string projectId, string packageId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AddPackageWriteRightsForUserResponse> GrantWriteRightsToUserAsync(string projectId, string packageId,
            AddPackageWriteRightsForUserRequest addPackageReadRightsForUserRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task RevokeWriteRightsFromUserAsync(string projectId, string packageId, string employeeId)
        {
            throw new System.NotImplementedException();
        }

        public async Task ShiftTasksAsync(string projectId, string packageId,
            ShiftPackageWithSuccessorsRequest shiftPackageWithSuccessorsRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
