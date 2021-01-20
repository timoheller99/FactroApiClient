namespace FactroApiClient.Project.Contracts.Base
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class CreateProjectRequest
    {
        public CreateProjectRequest(string title)
        {
            this.Title = title;
        }

        public string Description { get; set; }

        public string Title { get; set; }

        public string OfficerId { get; set; }

        public bool IsArchived { get; set; }

        public bool IsDraft { get; set; }

        public double Priority { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectState ProjectState { get; set; }
    }
}
