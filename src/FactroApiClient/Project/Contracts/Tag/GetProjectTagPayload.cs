namespace FactroApiClient.Project.Contracts.Tag
{
    using System;

    public class GetProjectTagPayload : IGetProjectTagPayload
    {
        public DateTime ChangeDate { get; set; }

        public string Id { get; set; }

        public string MandantId { get; set; }

        public string Name { get; set; }
    }
}
