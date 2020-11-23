namespace FactroApiClient.Company.Contracts.CompanyTag
{
    public class CreateCompanyTagRequest
    {
        public CreateCompanyTagRequest(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
