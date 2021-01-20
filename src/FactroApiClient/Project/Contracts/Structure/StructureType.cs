namespace FactroApiClient.Project.Contracts.Structure
{
    using System.Runtime.Serialization;

    public enum StructureType
    {
        NotDefined = 0,

        [EnumMember(Value = "project")]
        Project,

        [EnumMember(Value = "package")]
        Package,

        [EnumMember(Value = "task")]
        Task,
    }
}
