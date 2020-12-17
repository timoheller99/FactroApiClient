namespace FactroApiClient.Project.Contracts.AccessRights
{
    public class AddProjectReadRightsForUserResponse
    {
        public string EmployeeId { get; set; }

        public string Id { get; set; }

        public string MandantId { get; set; }

        public string ReferenceId { get; set; }

        public bool CanEdit { get; set; }

        public bool IsInherited { get; set; }
    }
}
