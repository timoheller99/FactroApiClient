namespace FactroApiClient.Package.Contracts.Document
{
    using System;

    using FactroApiClient.SharedContracts;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class CreatePackageDocumentResponse : IDocumentPayload
    {
        public DateTime ChangeDate { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatorId { get; set; }

        public string Id { get; set; }

        public string ContentType { get; set; }

        public string ReferenceId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DocumentReferenceType ReferenceType { get; set; }

        public double Size { get; set; }

        public string Title { get; set; }
    }
}
