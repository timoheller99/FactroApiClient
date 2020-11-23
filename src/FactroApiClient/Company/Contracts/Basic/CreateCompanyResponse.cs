namespace FactroApiClient.Company.Contracts.Basic
{
    public class CreateCompanyResponse : IGetCompanyPayload
    {
        public string Description { get; set; }

        public string Id { get; set; }

        public string City { get; set; }

        public long CompanyNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string ShortName { get; set; }

        public string Street { get; set; }

        public string Website { get; set; }

        public string ZipCode { get; set; }

        public string CustomerId { get; set; }
    }
}
