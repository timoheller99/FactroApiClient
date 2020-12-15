namespace FactroApiClient.Package.Contracts.AccessRights
{
    public class AddPackageWriteRightsForUserRequest
    {
        public AddPackageWriteRightsForUserRequest(string employeeId)
        {
            this.EmployeeId = employeeId;
        }

        public string EmployeeId { get; set; }
    }
}
