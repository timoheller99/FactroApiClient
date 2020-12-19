namespace FactroApiClient.Endpoints.ProjectApi
{
    internal static partial class ProjectApiEndpoints
    {
        public static class Structure
        {
            public static string GetStructure(string projectId)
            {
                return $"projects/{projectId}/structure";
            }
        }
    }
}
