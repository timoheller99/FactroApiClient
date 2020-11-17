namespace FactroApiClient.Appointment.Contracts
{
    using System;

    public class DeleteAppointmentResponse : IGetAppointmentPayload
    {
        public DateTime ChangeDate { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatorId { get; set; }

        public string Description { get; set; }

        public long Distance { get; set; }

        public long Duration { get; set; }

        public string EmployeeId { get; set; }

        public DateTime EndDate { get; set; }

        public string Id { get; set; }

        public string Location { get; set; }

        public string MandantId { get; set; }

        public string ReferencedTaskId { get; set; }

        public DateTime StartDate { get; set; }

        public string Subject { get; set; }
    }
}
