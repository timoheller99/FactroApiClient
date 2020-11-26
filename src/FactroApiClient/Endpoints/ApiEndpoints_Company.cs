namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class Company
        {
            public static string Create()
            {
                return "companies";
            }

            public static string GetAll()
            {
                return "companies";
            }

            public static string GetById(string companyId)
            {
                return $"companies/{companyId}";
            }

            public static string Update(string companyId)
            {
                return $"companies/{companyId}";
            }

            public static string Delete(string companyId)
            {
                return $"companies/{companyId}";
            }
        }
    }
}
