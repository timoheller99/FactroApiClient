namespace FactroApiClient.Contact.Contracts
{
    using System.Runtime.Serialization;

    public enum Salutation
    {
        NotDefined = 0,

        [EnumMember(Value = "none")]
        None,
        [EnumMember(Value = "male")]
        Male,
        [EnumMember(Value = "female")]
        Female,
    }
}
