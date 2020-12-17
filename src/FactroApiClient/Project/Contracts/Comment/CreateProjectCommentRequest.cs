namespace FactroApiClient.Project.Contracts.Comment
{
    public class CreateProjectCommentRequest
    {
        public CreateProjectCommentRequest(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public string ParentCommentId { get; set; }
    }
}
