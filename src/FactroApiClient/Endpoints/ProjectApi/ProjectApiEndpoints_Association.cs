namespace FactroApiClient.Endpoints.ProjectApi
{
    internal static partial class ProjectApiEndpoints
    {
        public static class Association
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
