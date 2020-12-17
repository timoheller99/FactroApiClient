namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class PackageComment
        {
            public static string Create(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/comments";
            }

            public static string GetAll(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/comments";
            }

            public static string Delete(string projectId, string packageId, string commentId)
            {
                return $"projects/{projectId}/packages/{packageId}/comments/{commentId}";
            }
        }
    }
}
