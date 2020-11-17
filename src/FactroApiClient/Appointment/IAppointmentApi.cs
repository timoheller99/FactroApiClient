namespace FactroApiClient.Appointment
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment.Contracts;

    public interface IAppointmentApi
    {
        public Task<IEnumerable<GetAppointmentPayload>> GetAppointmentsAsync();

        public Task<CreateAppointmentResponse> CreateAppointmentAsync(CreateAppointmentRequest createAppointmentRequest);

        public Task<GetAppointmentByIdResponse> GetAppointmentAsync(string appointmentId);

        public Task<UpdateAppointmentResponse> UpdateAppointmentAsync(string appointmentId, UpdateAppointmentRequest updateAppointmentRequest);

        public Task<DeleteAppointmentResponse> DeleteAppointmentAsync(string appointmentId);
    }
}
