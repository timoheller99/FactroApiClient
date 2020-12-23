namespace FactroApiClient.Package
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Package.Contracts;
    using FactroApiClient.Package.Contracts.AccessRights;
    using FactroApiClient.Package.Contracts.Association;
    using FactroApiClient.Package.Contracts.Comment;
    using FactroApiClient.Package.Contracts.Document;
    using FactroApiClient.Package.Entpoints;
    using FactroApiClient.SharedContracts;

    using Newtonsoft.Json;

    public class PackageApi : IPackageApi
    {
        private const string BaseClientName = "BaseClient";

        private readonly IHttpClientFactory httpClientFactory;

        private readonly JsonSerializerSettings jsonSerializerSettings;

        public PackageApi(IHttpClientFactory httpClientFactory)
        {
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
                var requestRoute = PackageApiEndpoints.Base.Create(projectId);

                var requestString = JsonConvert.SerializeObject(createPackageRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PostAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        "Could not create package.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
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
                var requestRoute = PackageApiEndpoints.Base.GetAll(projectId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        "Could not fetch packages.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
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
                var requestRoute = PackageApiEndpoints.Base.GetByProject(projectId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not fetch packages of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
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
                var requestRoute = PackageApiEndpoints.Base.GetById(packageId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not fetch package with id '{packageId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
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
                var requestRoute = PackageApiEndpoints.Base.GetById(projectId, packageId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not fetch package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
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
                var requestRoute = PackageApiEndpoints.Base.Update(projectId, packageId);

                var requestString = JsonConvert.SerializeObject(updatePackageRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not update package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
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
                var requestRoute = PackageApiEndpoints.Base.Delete(projectId, packageId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not delete package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<DeletePackageResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<CreatePackageCommentResponse> CreatePackageCommentAsync(string projectId, string packageId, CreatePackageCommentRequest createPackageCommentRequest)
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
                var requestRoute = PackageApiEndpoints.Comment.Create(projectId, packageId);

                var requestString = JsonConvert.SerializeObject(createPackageCommentRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PostAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not create comment in package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<CreatePackageCommentResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<IEnumerable<GetPackageCommentPayload>> GetCommentsOfPackageAsync(string projectId, string packageId)
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
                var requestRoute = PackageApiEndpoints.Comment.GetAll(projectId, packageId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not fetch comments of package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<List<GetPackageCommentPayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<DeletePackageCommentResponse> DeletePackageCommentAsync(string projectId, string packageId, string commentId)
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
                var requestRoute = PackageApiEndpoints.Comment.Delete(projectId, packageId, commentId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not delete comment with id '{commentId}' of package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<DeletePackageCommentResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task SetPackageCompanyAsync(string projectId, string packageId, SetCompanyAssociationRequest setCompanyAssociationRequest)
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
                var requestRoute = PackageApiEndpoints.Association.SetCompany(projectId, packageId);

                var requestString = JsonConvert.SerializeObject(setCompanyAssociationRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not set company with id '{setCompanyAssociationRequest.CompanyId}' as company of package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task RemovePackageCompanyAsync(string projectId, string packageId)
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
                var requestRoute = PackageApiEndpoints.Association.RemoveCompany(projectId, packageId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not remove company of package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task SetPackageContactAsync(string projectId, string packageId, SetContactAssociationRequest setContactAssociationRequest)
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
                var requestRoute = PackageApiEndpoints.Association.SetContact(projectId, packageId);

                var requestString = JsonConvert.SerializeObject(setContactAssociationRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not set contact with id '{setContactAssociationRequest.ContactId}' as contact of package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task RemovePackageContactAsync(string projectId, string packageId)
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
                var requestRoute = PackageApiEndpoints.Association.RemoveContact(projectId, packageId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not remove contact from package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task MovePackageIntoPackageAsync(string projectId, string packageId, SetPackageAssociationRequest setPackageAssociationRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (setPackageAssociationRequest == null)
            {
                throw new ArgumentNullException(nameof(setPackageAssociationRequest), $"{nameof(setPackageAssociationRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(setPackageAssociationRequest.ParentPackageId))
            {
                throw new ArgumentNullException(nameof(setPackageAssociationRequest), $"{nameof(setPackageAssociationRequest.ParentPackageId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = PackageApiEndpoints.Association.MoveIntoPackage(projectId, packageId);

                var requestString = JsonConvert.SerializeObject(setPackageAssociationRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not move package with id '{packageId}' of project with id '{projectId}' into package with id '{setPackageAssociationRequest.ParentPackageId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task MovePackageIntoProjectAsync(string projectId, string packageId, SetProjectAssociationRequest setProjectAssociationRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (setProjectAssociationRequest == null)
            {
                throw new ArgumentNullException(nameof(setProjectAssociationRequest), $"{nameof(setProjectAssociationRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(setProjectAssociationRequest.ProjectId))
            {
                throw new ArgumentNullException(nameof(setProjectAssociationRequest), $"{nameof(setProjectAssociationRequest.ProjectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = PackageApiEndpoints.Association.MoveIntoProject(projectId, packageId);

                var requestString = JsonConvert.SerializeObject(setProjectAssociationRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not move package with id '{packageId}' of project with id '{projectId}' into project with id '{setProjectAssociationRequest.ProjectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<IEnumerable<GetPackageDocumentPayload>> GetPackageDocumentsAsync(string projectId, string packageId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetPackageDocumentPayload> UploadPackageDocumentAsync(string projectId, string packageId, byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DeletePackageDocumentResponse> DeletePackageDocumentAsync(string projectId, string packageId, string documentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<GetPackageReadRightsResponse>> GetPackageReadRightsAsync(string projectId, string packageId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = PackageApiEndpoints.AccessRights.GetReadRights(projectId, packageId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not fetch read rights for package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<IEnumerable<GetPackageReadRightsResponse>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<AddPackageReadRightsForUserResponse> GrantPackageReadRightsToUserAsync(
            string projectId,
            string packageId,
            AddPackageReadRightsForUserRequest addPackageReadRightsForUserRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (addPackageReadRightsForUserRequest == null)
            {
                throw new ArgumentNullException(nameof(addPackageReadRightsForUserRequest), $"{nameof(addPackageReadRightsForUserRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(addPackageReadRightsForUserRequest.EmployeeId))
            {
                throw new ArgumentNullException(nameof(addPackageReadRightsForUserRequest), $"{nameof(addPackageReadRightsForUserRequest.EmployeeId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = PackageApiEndpoints.AccessRights.GrantReadRights(projectId, packageId);

                var requestString = JsonConvert.SerializeObject(addPackageReadRightsForUserRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not grant read rights to employee with id '{addPackageReadRightsForUserRequest.EmployeeId}' for package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<AddPackageReadRightsForUserResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task RevokePackageReadRightsFromUserAsync(string projectId, string packageId, string employeeId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(employeeId))
            {
                throw new ArgumentNullException(nameof(employeeId), $"{nameof(employeeId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = PackageApiEndpoints.AccessRights.RevokeReadRights(projectId, packageId, employeeId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not revoke read rights to employee with id '{employeeId}' for package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<IEnumerable<GetPackageWriteRightsResponse>> GetPackageWriteRightsAsync(string projectId, string packageId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = PackageApiEndpoints.AccessRights.GetWriteRights(projectId, packageId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not fetch write rights for package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<IEnumerable<GetPackageWriteRightsResponse>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<AddPackageWriteRightsForUserResponse> GrantPackageWriteRightsToUserAsync(
            string projectId,
            string packageId,
            AddPackageWriteRightsForUserRequest addPackageWriteRightsForUserRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (addPackageWriteRightsForUserRequest == null)
            {
                throw new ArgumentNullException(nameof(addPackageWriteRightsForUserRequest), $"{nameof(addPackageWriteRightsForUserRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(addPackageWriteRightsForUserRequest.EmployeeId))
            {
                throw new ArgumentNullException(nameof(addPackageWriteRightsForUserRequest), $"{nameof(addPackageWriteRightsForUserRequest.EmployeeId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = PackageApiEndpoints.AccessRights.GrantWriteRights(projectId, packageId);

                var requestString = JsonConvert.SerializeObject(addPackageWriteRightsForUserRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not grant read rights to employee with id '{addPackageWriteRightsForUserRequest.EmployeeId}' for package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<AddPackageWriteRightsForUserResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task RevokePackageWriteRightsFromUserAsync(string projectId, string packageId, string employeeId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(employeeId))
            {
                throw new ArgumentNullException(nameof(employeeId), $"{nameof(employeeId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = PackageApiEndpoints.AccessRights.RevokeWriteRights(projectId, packageId, employeeId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not revoke read rights to employee with id '{employeeId}' for package with id '{packageId}' of project with id '{projectId}'.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task ShiftTasksOfPackageAsync(
            string projectId,
            string packageId,
            ShiftPackageWithSuccessorsRequest shiftPackageWithSuccessorsRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(packageId))
            {
                throw new ArgumentNullException(nameof(packageId), $"{nameof(packageId)} can not be null, empty or whitespace.");
            }

            if (shiftPackageWithSuccessorsRequest == null)
            {
                throw new ArgumentNullException(nameof(shiftPackageWithSuccessorsRequest), $"{nameof(shiftPackageWithSuccessorsRequest)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = PackageApiEndpoints.ShiftTasksOfPackage.ShiftTasksWithSuccessors(projectId, packageId);

                var requestString = JsonConvert.SerializeObject(shiftPackageWithSuccessorsRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PostAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new FactroApiException(
                        $"Could not shift package with id '{packageId}' of project with id '{projectId}' by '{shiftPackageWithSuccessorsRequest.DaysDelta.ToString(CultureInfo.InvariantCulture)}' days.",
                        response.RequestMessage.RequestUri.ToString(),
                        response.StatusCode,
                        response.Content == null ? null : await response.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
