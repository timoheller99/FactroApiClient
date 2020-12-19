namespace FactroApiClient.Project.Endpoints
{
    internal static partial class ProjectApiEndpoints
    {
        public static class Document
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
