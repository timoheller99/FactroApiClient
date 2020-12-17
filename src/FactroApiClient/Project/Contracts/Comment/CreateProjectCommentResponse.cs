namespace FactroApiClient.Project.Contracts.Comment
{
    using System;

    public class CreateProjectCommentResponse : IGetProjectCommentPayload
    {
        public DateTime ChangeDate { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatorId { get; set; }

        public string Id { get; set; }

        public string MandantId { get; set; }

        public string ProjectId { get; set; }

        public string ParentId { get; set; }

        public string Text { get; set; }
    }
}
