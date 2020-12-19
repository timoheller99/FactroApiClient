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

        public Task<DeletePackageCommentResponse> DeletePackageCommentCommentAsync(string projectId, string packageId, string commentId);

        // Association
        public Task SetPackageCompanyAsync(string projectId, string packageId, SetCompanyAssociationRequest setCompanyAssociationRequest);

        public Task RemovePackageCompanyAsync(string projectId, string packageId);

        public Task SetPackageContactAsync(string projectId, string packageId, SetContactAssociationRequest setContactAssociationRequest);

        public Task RemovePackageContactAsync(string projectId, string packageId);

        public Task MovePackageIntoPackageAsync(string projectId, string packageId, SetPackageAssociationRequest setPackageAssociationRequest);

        public Task MovePackageIntoProjectAsync(string projectId, string packageId, SetProjectAssociationRequest setProjectAssociationRequest);

        // Documents
        public Task<IEnumerable<GetPackageDocumentPayload>> GetPackageDocumentsAsync(string projectId, string packageId);

        public Task<GetPackageDocumentPayload> UploadPackageDocumentAsync(string projectId, string packageId, byte[] data);

        public Task<DeletePackageDocumentResponse> DeletePackageDocumentAsync(string projectId, string packageId, string documentId);

        // Access Rights
        public Task<IEnumerable<GetPackageReadRightsResponse>> GetPackageReadRightsAsync(string projectId, string packageId);

        public Task<AddPackageReadRightsForUserResponse> GrantReadRightsToUserAsync(string projectId, string packageId, AddPackageReadRightsForUserRequest addPackageReadRightsForUserRequest);

        public Task RevokeReadRightsFromUserAsync(string projectId, string packageId, string employeeId);

        public Task<IEnumerable<GetPackageWriteRightsResponse>> GetWriteRightsAsync(string projectId, string packageId);

        public Task<AddPackageWriteRightsForUserResponse> GrantWriteRightsToUserAsync(string projectId, string packageId, AddPackageWriteRightsForUserRequest addPackageWriteRightsForUserRequest);

        public Task RevokeWriteRightsFromUserAsync(string projectId, string packageId, string employeeId);

        // Misc
        public Task ShiftTasksAsync(string projectId, string packageId, ShiftPackageWithSuccessorsRequest shiftPackageWithSuccessorsRequest);
    }
}
