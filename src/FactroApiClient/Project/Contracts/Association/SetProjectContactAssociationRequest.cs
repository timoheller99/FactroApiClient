namespace FactroApiClient.Project.Contracts.Association
{
    public class SetProjectContactAssociationRequest
    {
        public SetProjectContactAssociationRequest(string contactId)
        {
            this.ContactId = contactId;
        }

        public string ContactId { get; set; }
    }
}
