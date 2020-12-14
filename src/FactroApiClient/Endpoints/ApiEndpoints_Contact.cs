namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class Contact
        {
            public static string Create()
            {
                return "contacts";
            }

            public static string GetAll()
            {
                return "contacts";
            }

            public static string GetById(string contactId)
            {
                return $"contacts/{contactId}";
            }

            public static string Update(string contactId)
            {
                return $"contacts/{contactId}";
            }

            public static string Delete(string contactId)
            {
                return $"contacts/{contactId}";
            }
        }
    }
}
