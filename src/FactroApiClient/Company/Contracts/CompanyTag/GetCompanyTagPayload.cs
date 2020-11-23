namespace FactroApiClient.Company.Contracts.CompanyTag
{
    using System;

    public class GetCompanyTagPayload : IGetCompanyTagPayload
    {
        public DateTime ChangeDate { get; set; }

        public string Id { get; set; }

        public string MandantId { get; set; }

        public string Name { get; set; }
    }
}
