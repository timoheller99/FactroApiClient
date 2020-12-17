namespace FactroApiClient.SharedContracts
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
