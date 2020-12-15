namespace FactroApiClient.Package.Contracts.Comment
{
    public class CreatePackageCommentRequest
    {
        public CreatePackageCommentRequest(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public string ParentCommentId { get; set; }
    }
}
