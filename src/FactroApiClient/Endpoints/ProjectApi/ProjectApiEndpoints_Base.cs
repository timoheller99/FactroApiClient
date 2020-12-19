namespace FactroApiClient.Endpoints.ProjectApi
{
    internal static partial class ProjectApiEndpoints
    {
        public static class Base
        {
            public static string Create()
            {
                return "projects";
            }

            public static string GetAll()
            {
                return "projects";
            }

            public static string GetById(string projectId)
            {
                return $"projects/{projectId}";
            }

            public static string Update(string projectId)
            {
                return $"projects/{projectId}";
            }

            public static string Delete(string projectId)
            {
                return $"projects/{projectId}";
            }
        }
    }
}
