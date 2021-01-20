namespace FactroApiClient.Project.Contracts.Structure
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class GetProjectStructureResponse
    {
        public string Id { get; set; }

        public IEnumerable<ProjectStructureTree> Children { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StructureType Type { get; set; }
    }
}
