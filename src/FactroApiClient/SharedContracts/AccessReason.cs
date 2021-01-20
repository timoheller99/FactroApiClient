namespace FactroApiClient.SharedContracts
{
    using System.Runtime.Serialization;

    public enum AccessReason
    {
        NotDefined = 0,

        [EnumMember(Value = "isProjectOfficer")]
        IsProjectOfficer,

        [EnumMember(Value = "hasDirectProjectReadRight")]
        HasDirectProjectReadRight,

        [EnumMember(Value = "hasDirectProjectWriteRight")]
        HasDirectProjectWriteRight,

        [EnumMember(Value = "isPackageOfficer")]
        IsPackageOfficer,

        [EnumMember(Value = "hasDirectPackageReadRight")]
        HasDirectPackageReadRight,

        [EnumMember(Value = "hasDirectPackageWriteRight")]
        HasDirectPackageWriteRight,

        [EnumMember(Value = "isTaskOfficer")]
        IsTaskOfficer,

        [EnumMember(Value = "isTaskExecutor")]
        IsTaskExecutor,

        [EnumMember(Value = "hasDirectTaskReadRight")]
        HasDirectTaskReadRight,

        [EnumMember(Value = "hasDirectTaskWriteRight")]
        HasDirectTaskWriteRight,
    }
}
