namespace FactroApiClient.Endpoints.PackageApi
{
    internal static partial class PackageApiEndpoints
    {
        public static class Document
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
