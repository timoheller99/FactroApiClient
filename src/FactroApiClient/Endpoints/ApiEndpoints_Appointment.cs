namespace FactroApiClient.Endpoints
{
    internal static partial class ApiEndpoints
    {
        internal static class Appointment
        {
            public static string Create()
            {
                return "appointments";
            }

            public static string GetAll()
            {
                return "appointments";
            }

            public static string GetById(string appointmentId)
            {
                return $"appointments/{appointmentId}";
            }

            public static string Update(string appointmentId)
            {
                return $"appointments/{appointmentId}";
            }

            public static string Delete(string appointmentId)
            {
                return $"appointments/{appointmentId}";
            }
        }
    }
}
