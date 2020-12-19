namespace FactroApiClient.Project.Endpoints
{
    internal static partial class ProjectApiEndpoints
    {
        public static class AccessRights
        {
            public static string GetReadRights(string projectId)
            {
                return $"projects/{projectId}/read_rights";
            }

            public static string GrantReadRights(string projectId)
            {
                return $"projects/{projectId}/read_rights";
            }

            public static string RevokeReadRights(string projectId, string employeeId)
            {
                return $"projects/{projectId}/read_rights/{employeeId}";
            }

            public static string GetWriteRights(string projectId)
            {
                return $"projects/{projectId}/write_rights";
            }

            public static string GrantWriteRights(string projectId)
            {
                return $"projects/{projectId}/write_rights";
            }

            public static string RevokeWriteRights(string projectId, string employeeId)
            {
                return $"projects/{projectId}/write_rights/{employeeId}";
            }
        }
    }
}
