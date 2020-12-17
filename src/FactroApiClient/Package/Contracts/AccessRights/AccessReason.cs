namespace FactroApiClient.Package.Contracts.AccessRights
{
    public enum AccessReason
    {
        IsProjectOfficer,
        HasDirectProjectReadRight,
        HasDirectProjectWriteRight,
        IsPackageOfficer,
        HasDirectPackageReadRight,
        HasDirectPackageWriteRight,
        IsTaskOfficer,
        IsTaskExecutor,
        HasDirectTaskReadRight,
        HasDirectTaskWriteRight,
    }
}
