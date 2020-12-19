namespace FactroApiClient.Endpoints.CompanyApi
{
    internal static partial class CompanyApiEndpoints
    {
        public static class Base
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
