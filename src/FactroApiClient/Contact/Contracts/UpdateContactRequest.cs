namespace FactroApiClient.Contact.Contracts
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class UpdateContactRequest
    {
        public string Description { get; set; }

        public string City { get; set; }

        public string EmailAddress { get; set; }

        public string Phone { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobilePhone { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Salutation Salutation { get; set; }
    }
}
