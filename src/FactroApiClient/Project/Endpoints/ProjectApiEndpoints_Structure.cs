namespace FactroApiClient.Project.Endpoints
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
