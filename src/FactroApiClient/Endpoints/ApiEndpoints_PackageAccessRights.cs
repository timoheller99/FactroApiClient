namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class PackageAccessRights
        {
            public static string GetReadRights(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/read_rights";
            }

            public static string GrantReadRights(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/read_rights";
            }

            public static string RevokeReadRights(string projectId, string packageId, string employeeId)
            {
                return $"projects/{projectId}/packages/{packageId}/read_rights/{employeeId}";
            }

            public static string GetWriteRights(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/write_rights";
            }

            public static string GrantWriteRights(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/write_rights";
            }

            public static string RevokeWriteRights(string projectId, string packageId, string employeeId)
            {
                return $"projects/{projectId}/packages/{packageId}/write_rights/{employeeId}";
            }
        }
    }
}
