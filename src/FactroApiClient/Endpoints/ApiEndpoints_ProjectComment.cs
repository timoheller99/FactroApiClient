namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class ProjectComment
        {
            public static string Create(string projectId)
            {
                return $"projects/{projectId}/comments";
            }

            public static string GetAll(string projectId)
            {
                return $"projects/{projectId}/comments";
            }

            public static string Delete(string projectId, string commentId)
            {
                return $"projects/{projectId}/comments/{commentId}";
            }
        }
    }
}
