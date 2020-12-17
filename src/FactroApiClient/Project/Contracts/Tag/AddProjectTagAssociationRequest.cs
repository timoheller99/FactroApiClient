namespace FactroApiClient.Project.Contracts.Tag
{
    public class AddProjectTagAssociationRequest
    {
        public AddProjectTagAssociationRequest(string tagId)
        {
            this.TagId = tagId;
        }

        public string TagId { get; set; }
    }
}
