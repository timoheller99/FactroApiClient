namespace FactroApiClient.Project.Contracts.AccessRights
{
    public class AddProjectWriteRightsForUserRequest
    {
        public AddProjectWriteRightsForUserRequest(string employeeId)
        {
            this.EmployeeId = employeeId;
        }

        public string EmployeeId { get; set; }
    }
}
