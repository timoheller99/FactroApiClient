namespace FactroApiClient.Package.Contracts.AccessRights
{
    public class AddPackageReadRightsForUserRequest
    {
        public AddPackageReadRightsForUserRequest(string employeeId)
        {
            this.EmployeeId = employeeId;
        }

        public string EmployeeId { get; set; }
    }
}
