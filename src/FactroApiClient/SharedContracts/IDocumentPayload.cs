namespace FactroApiClient.SharedContracts
{
    using System;

    public interface IDocumentPayload
    {
        public DateTime ChangeDate { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatorId { get; set; }

        public string Id { get; set; }

        public string ContentType { get; set; }

        public string ReferenceId { get; set; }

        public DocumentReferenceType ReferenceType { get; set; }

        public double Size { get; set; }

        public string Title { get; set; }
    }
}