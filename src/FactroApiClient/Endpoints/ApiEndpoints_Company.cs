namespace FactroApiClient.Endpoints
{
    using System.Globalization;

    internal static partial class ApiEndpoints
    {
        internal static class Company
        {
            private const string CreateRoute = "companies";

            private const string GetAllRoute = "companies";

            private const string GetByIdRoute = "companies/{0}";

            private const string UpdateRoute = "companies/{0}";

            private const string DeleteRoute = "companies/{0}";

            public static string Create()
            {
                return CreateRoute;
            }

            public static string GetAll()
            {
                return GetAllRoute;
            }

            public static string GetById(string id)
            {
                return string.Format(CultureInfo.InvariantCulture, GetByIdRoute, id);
            }

            public static string Update(string id)
            {
                return string.Format(CultureInfo.InvariantCulture, UpdateRoute, id);
            }

            public static string Delete(string id)
            {
                return string.Format(CultureInfo.InvariantCulture, DeleteRoute, id);
            }
        }
    }
}
