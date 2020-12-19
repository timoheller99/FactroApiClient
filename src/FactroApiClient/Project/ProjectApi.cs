namespace FactroApiClient.Project
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.AccessRights;
    using FactroApiClient.Project.Contracts.Association;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.Project.Contracts.Comment;
    using FactroApiClient.Project.Contracts.Document;
    using FactroApiClient.Project.Contracts.Structure;
    using FactroApiClient.Project.Contracts.Tag;
    using FactroApiClient.Project.Endpoints;

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
                var requestRoute = ProjectApiEndpoints.Base.Create();

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
                var requestRoute = ProjectApiEndpoints.Base.GetAll();

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
                var requestRoute = ProjectApiEndpoints.Base.GetById(projectId);

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
                var requestRoute = ProjectApiEndpoints.Base.Update(projectId);

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
                var requestRoute = ProjectApiEndpoints.Base.Delete(projectId);

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
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (createProjectCommentRequest == null)
            {
                throw new ArgumentNullException(nameof(createProjectCommentRequest), $"{nameof(createProjectCommentRequest)} can not be null.");
            }

            if (createProjectCommentRequest.Text == null)
            {
                throw new ArgumentNullException(nameof(createProjectCommentRequest), $"{nameof(createProjectCommentRequest.Text)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Comment.Create(projectId);

                var requestString = JsonConvert.SerializeObject(createProjectCommentRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PostAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not create comment in project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<CreateProjectCommentResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<IEnumerable<GetProjectCommentPayload>> GetProjectCommentsAsync(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Comment.GetAll(projectId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch comments of project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<IEnumerable<GetProjectCommentPayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<DeleteProjectCommentResponse> DeleteProjectCommentAsync(string projectId, string commentId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(commentId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Comment.Delete(projectId, commentId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not delete comment with id '{CommentId}' of project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        commentId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<DeleteProjectCommentResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task SetProjectCompanyAsync(string projectId, SetProjectCompanyAssociationRequest setProjectCompanyAssociationRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (setProjectCompanyAssociationRequest == null)
            {
                throw new ArgumentNullException(nameof(setProjectCompanyAssociationRequest), $"{nameof(setProjectCompanyAssociationRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(setProjectCompanyAssociationRequest.CompanyId))
            {
                throw new ArgumentNullException(nameof(setProjectCompanyAssociationRequest), $"{nameof(setProjectCompanyAssociationRequest.CompanyId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Association.SetCompany(projectId);

                var requestString = JsonConvert.SerializeObject(setProjectCompanyAssociationRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not set company of project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
        }

        public async Task RemoveProjectCompanyAsync(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Association.RemoveCompany(projectId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not remove company from project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
        }

        public async Task SetProjectContactAsync(string projectId, SetProjectContactAssociationRequest setProjectContactAssociationRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (setProjectContactAssociationRequest == null)
            {
                throw new ArgumentNullException(nameof(setProjectContactAssociationRequest), $"{nameof(setProjectContactAssociationRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(setProjectContactAssociationRequest.ContactId))
            {
                throw new ArgumentNullException(nameof(setProjectContactAssociationRequest), $"{nameof(setProjectContactAssociationRequest.ContactId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Association.SetContact(projectId);

                var requestString = JsonConvert.SerializeObject(setProjectContactAssociationRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not set contact of project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
        }

        public async Task RemoveProjectContactAsync(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Association.RemoveContact(projectId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not remove contact from project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
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

        public async Task<IEnumerable<GetProjectReadRightsResponse>> GetProjectReadRightsAsync(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.AccessRights.GetReadRights(projectId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch read rights for project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<IEnumerable<GetProjectReadRightsResponse>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<AddProjectReadRightsForUserResponse> GrantProjectReadRightsToUserAsync(
            string projectId,
            AddProjectReadRightsForUserRequest addProjectReadRightsForUserRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (addProjectReadRightsForUserRequest == null)
            {
                throw new ArgumentNullException(nameof(addProjectReadRightsForUserRequest), $"{nameof(addProjectReadRightsForUserRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(addProjectReadRightsForUserRequest.EmployeeId))
            {
                throw new ArgumentNullException(nameof(addProjectReadRightsForUserRequest), $"{nameof(addProjectReadRightsForUserRequest.EmployeeId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.AccessRights.GrantReadRights(projectId);

                var requestString = JsonConvert.SerializeObject(addProjectReadRightsForUserRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not grant read rights to employee with id '{EmployeeId}' for project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        addProjectReadRightsForUserRequest.EmployeeId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<AddProjectReadRightsForUserResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task RevokeProjectReadRightsFromUserAsync(string projectId, string employeeId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(employeeId))
            {
                throw new ArgumentNullException(nameof(employeeId), $"{nameof(employeeId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.AccessRights.RevokeReadRights(projectId, employeeId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not revoke read rights from employee with id '{EmployeeId}' for project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        employeeId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
        }

        public async Task<IEnumerable<GetProjectWriteRightsResponse>> GetProjectWriteRightsAsync(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.AccessRights.GetWriteRights(projectId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch write rights for project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<IEnumerable<GetProjectWriteRightsResponse>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<AddProjectWriteRightsForUserResponse> GrantProjectWriteRightsToUserAsync(
            string projectId,
            AddProjectWriteRightsForUserRequest addProjectWriteRightsForUserRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (addProjectWriteRightsForUserRequest == null)
            {
                throw new ArgumentNullException(nameof(addProjectWriteRightsForUserRequest), $"{nameof(addProjectWriteRightsForUserRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(addProjectWriteRightsForUserRequest.EmployeeId))
            {
                throw new ArgumentNullException(nameof(addProjectWriteRightsForUserRequest), $"{nameof(addProjectWriteRightsForUserRequest.EmployeeId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.AccessRights.GrantWriteRights(projectId);

                var requestString = JsonConvert.SerializeObject(addProjectWriteRightsForUserRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not grant write rights to employee with id '{EmployeeId}' for project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        addProjectWriteRightsForUserRequest.EmployeeId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<AddProjectWriteRightsForUserResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task RevokeProjectWriteRightsFromUserAsync(string projectId, string employeeId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(employeeId))
            {
                throw new ArgumentNullException(nameof(employeeId), $"{nameof(employeeId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.AccessRights.RevokeWriteRights(projectId, employeeId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not revoke write rights from employee with id '{EmployeeId}' for project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        employeeId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
        }

        public async Task<GetProjectStructureResponse> GetProjectStructureAsync(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Structure.GetStructure(projectId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch structure of project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<GetProjectStructureResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<CreateProjectTagResponse> CreateProjectTagAsync(CreateProjectTagRequest createProjectTagRequest)
        {
            if (createProjectTagRequest == null)
            {
                throw new ArgumentNullException(nameof(createProjectTagRequest), $"{nameof(createProjectTagRequest)} can not be null.");
            }

            if (createProjectTagRequest.Name == null)
            {
                throw new ArgumentNullException(nameof(createProjectTagRequest), $"{nameof(createProjectTagRequest.Name)} can not be null.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Tag.Create();

                var requestString = JsonConvert.SerializeObject(createProjectTagRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PostAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not create project tag: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<CreateProjectTagResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<IEnumerable<GetProjectTagPayload>> GetProjectTagsAsync()
        {
            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Tag.GetAll();

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch project tags: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<IEnumerable<GetProjectTagPayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<IEnumerable<GetAssignedProjectTagPayload>> GetTagsOfProjectAsync(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Tag.GetById(projectId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch tags of project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<IEnumerable<GetAssignedProjectTagPayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task<DeleteProjectTagResponse> DeleteProjectTagAsync(string projectTagId)
        {
            if (string.IsNullOrWhiteSpace(projectTagId))
            {
                throw new ArgumentNullException(nameof(projectTagId), $"{nameof(projectTagId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Tag.Delete(projectTagId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not delete project tag with id '{ProjectTagId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        projectTagId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<DeleteProjectTagResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        public async Task AddTagToProjectAsync(string projectId, AddProjectTagAssociationRequest addProjectTagAssociationRequest)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (addProjectTagAssociationRequest == null)
            {
                throw new ArgumentNullException(nameof(addProjectTagAssociationRequest), $"{nameof(addProjectTagAssociationRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(addProjectTagAssociationRequest.TagId))
            {
                throw new ArgumentNullException(nameof(addProjectTagAssociationRequest), $"{nameof(addProjectTagAssociationRequest.TagId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Tag.AddToProject(projectId);

                var requestString = JsonConvert.SerializeObject(addProjectTagAssociationRequest, this.jsonSerializerSettings);
                var requestContent = ApiHelpers.GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not add project tag with id '{ProjectTagId}' to project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        addProjectTagAssociationRequest.TagId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
        }

        public async Task RemoveTagFromProjectAsync(string projectId, string tagId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentNullException(nameof(projectId), $"{nameof(projectId)} can not be null, empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(tagId))
            {
                throw new ArgumentNullException(nameof(tagId), $"{nameof(tagId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = ProjectApiEndpoints.Tag.RemoveFromProject(projectId, tagId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not remove project tag with id '{ProjectTagId}' from project with id '{ProjectId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        tagId,
                        projectId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);
                }
            }
        }
    }
}
