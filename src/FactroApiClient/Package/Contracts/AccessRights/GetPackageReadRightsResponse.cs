namespace FactroApiClient.Package.Contracts.AccessRights
{
    using System.Collections.Generic;

    using FactroApiClient.SharedContracts;

    public class GetPackageReadRightsResponse
    {
        public string EmployeeId { get; set; }

        public IEnumerable<AccessRightReason> AccessRights { get; set; }
    }
}
