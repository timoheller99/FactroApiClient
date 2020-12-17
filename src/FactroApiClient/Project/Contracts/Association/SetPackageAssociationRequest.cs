namespace FactroApiClient.Project.Contracts.Association
{
    public class SetPackageAssociationRequest
    {
        public SetPackageAssociationRequest(string packageId)
        {
            this.PackageId = packageId;
        }

        public string PackageId { get; set; }
    }
}
