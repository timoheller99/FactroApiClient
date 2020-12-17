namespace FactroApiClient.Project.Contracts.AccessRights
{
    public class AddProjectWriteRightsForUserResponse
    {
        public string EmployeeId { get; set; }

        public string Id { get; set; }

        public string MandantId { get; set; }

        public string ReferenceId { get; set; }

        public bool CanEdit { get; set; }

        public bool IsInherited { get; set; }
    }
}
