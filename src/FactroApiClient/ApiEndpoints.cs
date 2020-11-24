namespace FactroApiClient
{
    internal static class ApiEndpoints
    {
        internal static class Appointment
        {
            public const string Create = "appointments";

            public const string GetAll = "appointments";

            public const string GetById = "appointments/{0}";

            public const string Update = "appointments/{0}";

            public const string Delete = "appointments/{0}";
        }

        internal static class Company
        {
            public const string Create = "companies";

            public const string GetAll = "companies";

            public const string GetById = "companies/{0}";

            public const string Update = "companies/{0}";

            public const string Delete = "companies/{0}";
        }
    }
}
