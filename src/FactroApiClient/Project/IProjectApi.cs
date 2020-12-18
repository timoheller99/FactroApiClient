namespace FactroApiClient.Project
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FactroApiClient.Project.Contracts.AccessRights;
    using FactroApiClient.Project.Contracts.Association;
    using FactroApiClient.Project.Contracts.Base;
    using FactroApiClient.Project.Contracts.Comment;
    using FactroApiClient.Project.Contracts.Document;
    using FactroApiClient.Project.Contracts.Structure;
    using FactroApiClient.Project.Contracts.Tag;

    public interface IProjectApi
    {
        // Base
        public Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest createProjectRequest);

        public Task<IEnumerable<GetProjectPayload>> GetProjectsAsync();

        public Task<GetProjectByIdResponse> GetProjectByIdAsync(string projectId);

        public Task<UpdateProjectResponse> UpdateProjectAsync(string projectId, UpdateProjectRequest updateProjectRequest);

        public Task<DeleteProjectResponse> DeleteProjectAsync(string projectId);

        // Comment
        public Task<CreateProjectCommentResponse> CreateProjectCommentAsync(string projectId, CreateProjectCommentRequest createProjectCommentRequest);

        public Task<IEnumerable<GetProjectCommentPayload>> GetProjectCommentsAsync(string projectId);

        public Task<DeleteProjectCommentResponse> DeleteProjectCommentAsync(string projectId, string commentId);

        // Association
        public Task SetProjectCompanyAsync(string projectId, SetProjectCompanyAssociationRequest setProjectCompanyAssociationRequest);

        public Task RemoveProjectCompanyAsync(string projectId);

        public Task SetProjectContactAsync(string projectId, SetProjectContactAssociationRequest setProjectContactAssociationRequest);

        public Task RemoveProjectContactAsync(string projectId);

        // Document
        public Task<CreateProjectDocumentResponse> CreateProjectDocumentAsync(string projectId, byte[] data);

        public Task<IEnumerable<GetProjectDocumentPayload>> GetProjectDocumentsAsync(string projectId);

        public Task<RemoveProjectDocumentResponse> DeleteProjectDocumentAsync(string projectId, string documentId);

        // Access Rights
        public Task<IEnumerable<GetProjectReadRightsResponse>> GetReadRightsAsync(string projectId);

        public Task<AddProjectReadRightsForUserResponse> GrantReadRightsToUserAsync(string projectId, AddProjectReadRightsForUserRequest addProjectReadRightsForUserRequest);

        public Task RevokeReadRightsFromUserAsync(string projectId, string employeeId);

        public Task<IEnumerable<GetProjectWriteRightsResponse>> GetWriteRightsAsync(string projectId);

        public Task<AddProjectWriteRightsForUserResponse> GrantWriteRightsToUserAsync(string projectId, AddProjectWriteRightsForUserRequest addProjectWriteRightsForUserRequest);

        public Task RevokeWriteRightsFromUserAsync(string projectId, string employeeId);

        // Structure
        public Task<GetProjectStructureResponse> GetProjectStructureAsync(string projectId);

        // Tags
        public Task<CreateProjectTagResponse> CreateProjectTagAsync(CreateProjectTagRequest createProjectTagRequest);

        public Task<IEnumerable<GetProjectTagPayload>> GetProjectTagsAsync();

        public Task<IEnumerable<GetAssignedProjectTagPayload>> GetTagsOfProjectAsync(string projectId);

        public Task<DeleteProjectTagResponse> DeleteProjectTagAsync(string projectTagId);

        public Task AddTagToProjectAsync(string projectId, AddProjectTagAssociationRequest addProjectTagAssociationRequest);

        public Task RemoveTagFromProjectAsync(string projectId, string tagId);
    }
}
