namespace FactroApiClient.Appointment.Contracts
{
    using System;

    public class CreateAppointmentRequest
    {
        public CreateAppointmentRequest(string employeeId, DateTime startDate, DateTime endDate)
        {
            this.EmployeeId = employeeId;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public string EmployeeId { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public string Description { get; set; }

        public long Distance { get; set; }

        public string Location { get; set; }

        public string ReferencedTaskId { get; set; }

        public string Subject { get; set; }
    }
}
