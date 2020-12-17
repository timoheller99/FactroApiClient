namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class ProjectStructure
        {
            public static string GetStructure(string projectId)
            {
                return $"projects/{projectId}/structure";
            }
        }
    }
}
