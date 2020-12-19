namespace FactroApiClient.Package
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FactroApiClient.Package.Contracts;
    using FactroApiClient.Package.Contracts.AccessRights;
    using FactroApiClient.Package.Contracts.Association;
    using FactroApiClient.Package.Contracts.Comment;
    using FactroApiClient.Package.Contracts.Document;

    public interface IPackageApi
    {
        // Base
        public Task<CreatePackageResponse> CreatePackageAsync(string projectId, CreatePackageRequest createPackageRequest);

        public Task<IEnumerable<GetPackagePayload>> GetPackagesAsync(string projectId);

        public Task<IEnumerable<GetPackagePayload>> GetPackagesOfProjectAsync(string projectId);

        public Task<GetPackageByIdResponse> GetPackageByIdAsync(string packageId);

        public Task<GetPackageByIdResponse> GetPackageByIdAsync(string projectId, string packageId);

        public Task<UpdatePackageResponse> UpdatePackageAsync(string projectId, string packageId, UpdatePackageRequest updatePackageRequest);

        public Task<DeletePackageResponse> DeletePackageAsync(string projectId, string packageId);

        // Comments
        public Task<CreatePackageCommentResponse> CreatePackageCommentAsync(string projectId, string packageId, CreatePackageCommentRequest createPackageCommentRequest);

        public Task<IEnumerable<GetPackageCommentPayload>> GetCommentsOfPackageAsync(string projectId, string packageId);

        public Task<DeletePackageCommentResponse> DeleteCommentAsync(string projectId, string packageId, string commentId);

        // Association
        public Task SetCompanyAsync(string projectId, string packageId, SetCompanyAssociationRequest setCompanyAssociationRequest);

        public Task RemoveCompanyAsync(string projectId, string packageId);

        public Task SetContactAsync(string projectId, string packageId, SetContactAssociationRequest setContactAssociationRequest);

        public Task RemoveContactAsync(string projectId, string packageId);

        public Task MoveToPackageAsync(string projectId, string packageId, SetPackageAssociationRequest setPackageAssociationRequest);

        public Task MoveToProjectAsync(string projectId, string packageId, SetProjectAssociationRequest setProjectAssociationRequest);

        // Documents
        public Task<IEnumerable<GetPackageDocumentPayload>> GetDocumentsAsync(string projectId, string packageId);

        public Task<GetPackageDocumentPayload> UploadDocumentAsync(string projectId, string packageId, byte[] data);

        public Task<DeletePackageDocumentResponse> DeleteDocumentAsync(string projectId, string packageId, string documentId);

        // Access Rights
        public Task<IEnumerable<GetPackageReadRightsResponse>> GetReadRightsAsync(string projectId, string packageId);

        public Task<AddPackageReadRightsForUserResponse> GrantReadRightsToUserAsync(string projectId, string packageId, AddPackageReadRightsForUserRequest addPackageReadRightsForUserRequest);

        public Task RevokeReadRightsFromUserAsync(string projectId, string packageId, string employeeId);

        public Task<IEnumerable<GetPackageWriteRightsResponse>> GetWriteRightsAsync(string projectId, string packageId);

        public Task<AddPackageWriteRightsForUserResponse> GrantWriteRightsToUserAsync(string projectId, string packageId, AddPackageWriteRightsForUserRequest addPackageWriteRightsForUserRequest);

        public Task RevokeWriteRightsFromUserAsync(string projectId, string packageId, string employeeId);

        // Misc
        public Task ShiftTasksAsync(string projectId, string packageId, ShiftPackageWithSuccessorsRequest shiftPackageWithSuccessorsRequest);
    }
}
