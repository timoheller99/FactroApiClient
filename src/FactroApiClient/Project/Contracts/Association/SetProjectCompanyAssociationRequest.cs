namespace FactroApiClient.Project.Contracts.Association
{
    public class SetProjectCompanyAssociationRequest
    {
        public SetProjectCompanyAssociationRequest(string companyId)
        {
            this.CompanyId = companyId;
        }

        public string CompanyId { get; set; }
    }
}
