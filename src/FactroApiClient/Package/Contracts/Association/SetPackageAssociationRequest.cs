namespace FactroApiClient.Package.Contracts.Association
{
    public class SetPackageAssociationRequest
    {
        public SetPackageAssociationRequest(string parentPackageId)
        {
            this.ParentPackageId = parentPackageId;
        }

        public string ParentPackageId { get; set; }
    }
}
