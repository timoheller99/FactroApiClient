namespace FactroApiClient.Project
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Endpoints;
    using FactroApiClient.Project.Contracts.AccessRights;
    using FactroApiClient.Project.Contracts.Association;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.Project.Contracts.Comment;
    using FactroApiClient.Project.Contracts.Document;
    using FactroApiClient.Project.Contracts.Structure;
    using FactroApiClient.Project.Contracts.Tag;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    public class ProjectApi : IProjectApi
    {
        private const string BaseClientName = "BaseClient";

        private readonly ILogger<ProjectApi> logger;

        private readonly IHttpClientFactory httpClientFactory;

        private readonly JsonSerializerSettings jsonSerializerSettings;

        public ProjectApi(ILogger<ProjectApi> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializerSettings = SerializerSettings.JsonSerializerSettings;
        }

        public async Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest createProjectRequest)
        {
            if (createProjectRequest == null)
            {
                throw new ArgumentNullException(nameof(createProjectRequest), $"{nameof(createProjectRequest)} can not be null.");
            }

            if (createProjectRequest.Title == null)
            {
                throw new ArgumentNullException(nameof(createProjectRequest), $"{nameof(createProjectRequest.Title)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Project.Create();

                var requestString = JsonConvert.SerializeObject(createProjectRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PostAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not create project: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<CreateProjectResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<IEnumerable<GetProjectPayload>> GetProjectsAsync()
        {
            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Project.GetAll();

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch projects: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<IEnumerable<GetProjectPayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<GetProjectByIdResponse> GetProjectByIdAsync(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Project.GetById(projectId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<GetProjectByIdResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<UpdateProjectResponse> UpdateProjectAsync(string projectId, UpdateProjectRequest updateProjectRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (updateProjectRequest == null)
            {
                throw new ArgumentNullException(nameof(updateProjectRequest), $"{nameof(updateProjectRequest)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Project.Update(projectId);

                var requestString = JsonConvert.SerializeObject(updateProjectRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not update project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<UpdateProjectResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<DeleteProjectResponse> DeleteProjectAsync(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ApiEndpoints.Project.Delete(projectId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not delete project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<DeleteProjectResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<CreateProjectCommentResponse> CreateProjectCommentAsync(string projectId, CreateProjectCommentRequest createProjectCommentRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<GetProjectCommentPayload>> GetProjectCommentsAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DeleteProjectCommentResponse> DeleteProjectCommentAsync(string projectId, string commentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetProjectCompanyAsync(string projectId, SetProjectCompanyAssociationRequest setProjectCompanyAssociationRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveProjectCompanyAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetProjectContactAsync(string projectId, SetProjectContactAssociationRequest setProjectContactAssociationRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveProjectContactAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CreateProjectDocumentResponse> CreateProjectDocumentAsync(string projectId, byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<GetProjectDocumentPayload>> GetProjectDocumentsAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RemoveProjectDocumentResponse> DeleteProjectDocumentAsync(string projectId, string documentId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetProjectReadRightsResponse> GetReadRightsAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AddProjectReadRightsForUserResponse> GrantReadRightsToUserAsync(string projectId,
            AddProjectReadRightsForUserRequest addProjectReadRightsForUserRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task RevokeReadRightsFromUserAsync(string projectId, string employeeId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetProjectWriteRightsResponse> GetWriteRightsAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AddProjectWriteRightsForUserResponse> GrantWriteRightsToUserAsync(string projectId,
            AddProjectWriteRightsForUserRequest addProjectWriteRightsForUserRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task RevokeWriteRightsFromUserAsync(string projectId, string employeeId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetProjectStructureResponse> GetProjectStructureAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CreateProjectTagResponse> CreateProjectTagAsync(CreateProjectTagRequest createProjectTagRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<GetProjectTagPayload>> GetProjectTagsAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<GetAssignedProjectTagPayload>> GetTagsOfProjectAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DeleteProjectTagResponse> DeleteProjectTagAsync(string projectTagId)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddTagToProjectAsync(string projectId, AddProjectTagAssociationRequest addProjectTagAssociationRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveTagFromProjectAsync(string projectId, string tagId)
        {
            throw new System.NotImplementedException();
        }
    }
}
