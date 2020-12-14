namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class CompanyTag
        {
            public static string Create()
            {
                return "companies/tags";
            }

            public static string GetAll()
            {
                return "companies/tags";
            }

            public static string GetById(string companyId)
            {
                return $"companies/{companyId}/tags";
            }

            public static string Delete(string tagId)
            {
                return $"companies/tags/{tagId}";
            }

            public static string Add(string companyId)
            {
                return $"companies/{companyId}/tags";
            }

            public static string Remove(string companyId, string tagId)
            {
                return $"companies/{companyId}/tags/{tagId}";
            }
        }
    }
}
