namespace FactroApiClient.Package.Entpoints
{
    internal static partial class PackageApiEndpoints
    {
        public static class Base
        {
            public static string Create(string projectId)
            {
                return $"projects/{projectId}/packages";
            }

            public static string GetAll(string projectId)
            {
                return $"projects/{projectId}/packages";
            }

            public static string GetByProject(string projectId)
            {
                return $"projects/{projectId}/packages";
            }

            public static string GetById(string packageId)
            {
                return $"packages/{packageId}";
            }

            public static string GetById(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}";
            }

            public static string Update(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}";
            }

            public static string Delete(string projectId, string packageId)
            {
                return $"projects/{projectId}/packages/{packageId}";
            }
        }
    }
}
