namespace FactroApiClient.Package.Contracts
{
    using System;

    public class CreatePackageResponse : IGetPackagePayload
    {
        public double Number { get; set; }

        public DateTime ChangeDate { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatorId { get; set; }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public string Id { get; set; }

        public string MandantId { get; set; }

        public DateTime StartDate { get; set; }

        public string Title { get; set; }

        public string CompanyContactId { get; set; }

        public string CompanyId { get; set; }

        public string OfficerId { get; set; }

        public string ParentPackageId { get; set; }

        public double PlannedEffort { get; set; }

        public string ProjectId { get; set; }

        public double RealizedEffort { get; set; }

        public double RemainingEffort { get; set; }
    }
}
