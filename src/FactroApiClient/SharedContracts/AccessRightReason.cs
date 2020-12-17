namespace FactroApiClient.SharedContracts
{
    public class AccessRightReason
    {
        public AccessReason Reason { get; set; }

        public string TaskId { get; set; }

        public string PackageId { get; set; }

        public string ProjectId { get; set; }
    }
}
