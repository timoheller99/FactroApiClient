namespace FactroApiClient.Contact.Contracts
{
    public class CreateContactRequest
    {
        public CreateContactRequest(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string Description { get; set; }

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