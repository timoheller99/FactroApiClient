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

    public class ProjectApi : IProjectApi
    {
        public async Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest createProjectRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<GetProjectPayload>> GetProjectsAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<GetProjectByIdResponse> GetProjectByIdAsync(string projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UpdateProjectResponse> UpdateProjectAsync(string projectId, UpdateProjectRequest updateProjectRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DeleteProjectResponse> DeleteProjectAsync(string projectId)
        {
            throw new System.NotImplementedException();
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
