namespace FactroApiClient.Company.Contracts.CompanyTag
{
    public class AddCompanyTagAssociationRequest
    {
        public AddCompanyTagAssociationRequest(string tagId)
        {
            this.TagId = tagId;
        }

        public string TagId { get; set; }
    }
}
