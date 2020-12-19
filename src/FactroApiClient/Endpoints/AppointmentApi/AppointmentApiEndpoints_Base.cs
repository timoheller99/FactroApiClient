namespace FactroApiClient.Endpoints.AppointmentApi
{
    internal static partial class AppointmentApiEndpoints
    {
        public static class Base
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
