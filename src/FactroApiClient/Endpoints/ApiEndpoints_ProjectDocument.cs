namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class ProjectDocuments
        {
            public static string Create(string projectId)
            {
                return $"projects/{projectId}/documents";
            }

            public static string GetAll(string projectId)
            {
                return $"projects/{projectId}/documents";
            }

            public static string Delete(string projectId, string documentId)
            {
                return $"projects/{projectId}/documents/{documentId}";
            }
        }
    }
}
