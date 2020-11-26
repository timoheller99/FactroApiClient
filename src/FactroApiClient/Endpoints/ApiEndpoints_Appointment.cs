namespace FactroApiClient.Endpoints
{
    using System.Globalization;

    internal static partial class ApiEndpoints
    {
        internal static class Appointment
        {
            private const string CreateRoute = "appointments";

            private const string GetAllRoute = "appointments";

            private const string GetByIdRoute = "appointments/{0}";

            private const string UpdateRoute = "appointments/{0}";

            private const string DeleteRoute = "appointments/{0}";

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
