namespace FactroApiClient.Package.Entpoints
{
    internal static partial class PackageApiEndpoints
    {
        public static class Association
        {
            public static string SetCompany(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/company";
            }

            public static string RemoveCompany(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/company";
            }

            public static string SetContact(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/contact";
            }

            public static string RemoveContact(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/contact";
            }

            public static string MoveIntoPackage(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/package";
            }

            public static string MoveIntoProject(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}/project";
            }
        }
    }
}
