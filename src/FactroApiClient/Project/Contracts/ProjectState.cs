namespace FactroApiClient.Project.Contracts
{
    using System.Runtime.Serialization;

    public enum ProjectState
    {
        NotDefined = 0,

        [EnumMember(Value = "planned")]
        Planned,

        [EnumMember(Value = "inProcess")]
        InProgress,

        [EnumMember(Value = "closed")]
        Closed,

        [EnumMember(Value = "pushedBack")]
        PushedBack,

        [EnumMember(Value = "stopped")]
        Stopped,
    }
}
