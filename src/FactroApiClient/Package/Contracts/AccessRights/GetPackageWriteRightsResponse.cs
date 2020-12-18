namespace FactroApiClient.Package.Contracts.AccessRights
{
    using System.Collections.Generic;

    using FactroApiClient.SharedContracts;

    public class GetPackageWriteRightsResponse
    {
        public string EmployeeId { get; set; }

        public IEnumerable<AccessRightReason> AccessRights { get; set; }
    }
}
