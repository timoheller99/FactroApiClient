namespace FactroApiClient.Project.Contracts.Base
{
    public class UpdateProjectRequest
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public string OfficerId { get; set; }

        public bool IsArchived { get; set; }

        public bool IsDraft { get; set; }

        public double Priority { get; set; }

        public ProjectState ProjectState { get; set; }
    }
}
