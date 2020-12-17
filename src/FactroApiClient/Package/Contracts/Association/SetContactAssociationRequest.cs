namespace FactroApiClient.Package.Contracts.Association
{
    public class SetContactAssociationRequest
    {
        public SetContactAssociationRequest(string contactId)
        {
            this.ContactId = contactId;
        }

        public string ContactId { get; set; }
    }
}
