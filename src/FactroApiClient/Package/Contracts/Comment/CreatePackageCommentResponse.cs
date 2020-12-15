namespace FactroApiClient.Package.Contracts.Comment
{
    using System;

    public class CreatePackageCommentResponse : IGetPackageCommentPayload
    {
        public DateTime ChangeDate { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatorId { get; set; }

        public string Id { get; set; }

        public string MandantId { get; set; }

        public string ParentId { get; set; }

        public string TaskPackageId { get; set; }

        public string Text { get; set; }
    }
}
