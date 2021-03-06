namespace FactroApiClient.Project.Contracts.AccessRights
{
    using System.Collections.Generic;

    using FactroApiClient.SharedContracts;

    public class GetProjectReadRightsResponse
    {
        public string EmployeeId { get; set; }

        public IEnumerable<AccessRightReason> AccessRights { get; set; }
    }
}
