namespace FactroApiClient.Endpoints.ProjectApi
{
    internal static partial class ProjectApiEndpoints
    {
        public static class Tag
        {
            public static string Create()
            {
                return "projects/tags";
            }

            public static string GetAll()
            {
                return "projects/tags";
            }

            public static string GetById(string projectId)
            {
                return $"projects/{projectId}/tags";
            }

            public static string Delete(string tagId)
            {
                return $"projects/tags/{tagId}";
            }

            public static string AddToProject(string projectId)
            {
                return $"projects/{projectId}/tags/";
            }

            public static string RemoveFromProject(string projectId, string tagId)
            {
                return $"projects/{projectId}/tags/{tagId}";
            }
        }
    }
}
