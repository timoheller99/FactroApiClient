namespace FactroApiClient.Endpoints.ProjectApi
{
    internal static partial class ProjectApiEndpoints
    {
        public static class Comment
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
