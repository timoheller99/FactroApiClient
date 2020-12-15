namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class PackageDocuments
        {
            public static string Create(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/documents";
            }

            public static string GetAll(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/documents";
            }

            public static string Delete(string projectId, string packageId, string documentId)
            {
                return $"projects/{projectId}/packages/{packageId}/documents/{documentId}";
            }
        }
    }
}
