namespace FactroApiClient.Project.Contracts.Structure
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class ProjectStructureTree
    {
        public string Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StructureType Type { get; set; }

        public IEnumerable<ProjectStructureTree> Children { get; set; }
    }
}
