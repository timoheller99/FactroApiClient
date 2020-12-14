namespace FactroApiClient.Contact.Contracts
{
    public class UpdateContactResponse : IGetContactPayload
    {
        public string Description { get; set; }

        public string Id { get; set; }

        public string City { get; set; }

        public string EmailAddress { get; set; }

        public string Phone { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobilePhone { get; set; }

        public Salutation Salutation { get; set; }
    }
}
