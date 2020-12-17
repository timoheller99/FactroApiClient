namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class ProjectAssociation
        {
            public static string SetCompany(string projectId)
            {
                return $"projects/{projectId}/company";
            }

            public static string RemoveCompany(string projectId)
            {
                return $"projects/{projectId}/company";
            }

            public static string SetContact(string projectId)
            {
                return $"projects/{projectId}/contact";
            }

            public static string RemoveContact(string projectId)
            {
                return $"projects/{projectId}/contact";
            }
        }
    }
}
