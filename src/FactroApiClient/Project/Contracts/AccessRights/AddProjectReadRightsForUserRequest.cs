namespace FactroApiClient.Project.Contracts.AccessRights
{
    public class AddProjectReadRightsForUserRequest
    {
        public AddProjectReadRightsForUserRequest(string employeeId)
        {
            this.EmployeeId = employeeId;
        }

        public string EmployeeId { get; set; }
    }
}
