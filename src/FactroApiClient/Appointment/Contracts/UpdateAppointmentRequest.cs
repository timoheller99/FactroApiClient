namespace FactroApiClient.Appointment.Contracts
{
    using System;

    public class UpdateAppointmentRequest
    {
        public string Description { get; set; }

        public long Distance { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public string ReferencedTaskId { get; set; }

        public DateTime StartDate { get; set; }

        public string Subject { get; set; }
    }
}
