namespace FactroApiClient.Appointment
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment.Contracts;

    /// <summary>
    /// Interface for the Appointment API
    /// </summary>
    /// <remarks>
    /// Appointments are not yet booked Work-Records.
    /// </remarks>
    public interface IAppointmentApi
    {
        /// <summary>
        /// Creates an appointment.
        /// </summary>
        /// <param name="createAppointmentRequest">The request model to create a new appointment.</param>
        /// <returns>Returns the created appointment.</returns>
        public Task<CreateAppointmentResponse> CreateAppointmentAsync(CreateAppointmentRequest createAppointmentRequest);

        /// <summary>
        /// Fetches the appointments visible to the requesting user.
        /// </summary>
        /// <returns>Returns a list of all appointments that are visible to the requesting user.</returns>
        public Task<IEnumerable<GetAppointmentPayload>> GetAppointmentsAsync();

        /// <summary>
        /// Fetches the appointment with the given appointment id.
        /// </summary>
        /// <param name="appointmentId">The id of the appointment that should be fetched.</param>
        /// <returns>Returns the fetched appointment.</returns>
        public Task<GetAppointmentByIdResponse> GetAppointmentByIdAsync(string appointmentId);

        /// <summary>
        /// Updates the appointment with the given appointment id.
        /// </summary>
        /// <param name="appointmentId">The id oof the appointment that should be updated.</param>
        /// <param name="updateAppointmentRequest">The request model to update the appointment.</param>
        /// <returns>Returns the updated appointment</returns>
        public Task<UpdateAppointmentResponse> UpdateAppointmentAsync(string appointmentId, UpdateAppointmentRequest updateAppointmentRequest);

        /// <summary>
        /// Deletes the appointment with the given appointment id.
        /// </summary>
        /// <param name="appointmentId">The id of the appointment that should be deleted.</param>
        /// <returns>Returns the deleted appointment.</returns>
        public Task<DeleteAppointmentResponse> DeleteAppointmentAsync(string appointmentId);
    }
}
