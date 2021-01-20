namespace FactroApiClient.SharedContracts
{
    using System.Runtime.Serialization;

    public enum DocumentReferenceType
    {
        NotDefined = 0,

        [EnumMember(Value = "task")]
        Task,

        [EnumMember(Value = "package")]
        Package,

        [EnumMember(Value = "project")]
        Project,
    }
}
