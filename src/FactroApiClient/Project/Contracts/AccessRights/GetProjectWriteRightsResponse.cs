namespace FactroApiClient.Project.Contracts.AccessRights
{
    using System.Collections.Generic;

    using FactroApiClient.SharedContracts;

    public class GetProjectWriteRightsResponse
    {
        // TODO: The structure of this model is not clearly descripted in the documentation and has to be tested.
        public IEnumerable<AccessRightReason> AccessRightReasons { get; set; }
    }
}