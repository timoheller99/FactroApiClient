namespace FactroApiClient.Endpoints.PackageApi
{
    internal static partial class PackageApiEndpoints
    {
        public static class Comment
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
