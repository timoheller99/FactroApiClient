namespace FactroApiClient.Package.Contracts.Association
{
    public class SetProjectAssociationRequest
    {
        public SetProjectAssociationRequest(string projectId)
        {
            this.ProjectId = projectId;
        }

        public string ProjectId { get; set; }
    }
}
