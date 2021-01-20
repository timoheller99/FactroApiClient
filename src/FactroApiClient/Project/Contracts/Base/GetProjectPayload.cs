namespace FactroApiClient.Project.Contracts.Base
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class GetProjectPayload : IGetProjectPayload
    {
        public double Number { get; set; }

        public DateTime ChangeDate { get; set; }

        public DateTime CreateDate { get; set; }

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

        public double PlannedEffort { get; set; }

        public double RealizedEffort { get; set; }

        public double RemainingEffort { get; set; }

        public bool IsArchived { get; set; }

        public bool IsDraft { get; set; }

        public double Priority { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectState ProjectState { get; set; }
    }
}
