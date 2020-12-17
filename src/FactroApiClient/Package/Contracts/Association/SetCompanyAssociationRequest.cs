namespace FactroApiClient.Package.Contracts.Association
{
    public class SetCompanyAssociationRequest
    {
        public SetCompanyAssociationRequest(string companyId)
        {
            this.CompanyId = companyId;
        }

        public string CompanyId { get; set; }
    }
}
