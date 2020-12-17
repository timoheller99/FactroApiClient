namespace FactroApiClient.Package.Contracts.Document
{
    using System;

    public interface IGetPackageDocumentPayload
    {
        public DateTime ChangeDate { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatorId { get; set; }

        public string Id { get; set; }

        public string MandantId { get; set; }

        public string ContentType { get; set; }

        public string ReferenceId { get; set; }

        public DocumentReferenceType DocumentReferenceType { get; set; }

        public double Size { get; set; }

        public string Title { get; set; }
    }
}
