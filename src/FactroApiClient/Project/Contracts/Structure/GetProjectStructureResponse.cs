namespace FactroApiClient.Project.Contracts.Structure
{
    using System.Collections.Generic;

    public class GetProjectStructureResponse
    {
        public string Id { get; set; }

        public IEnumerable<ProjectStructureTree> Children { get; set; }

        public StructureType Type { get; set; }
    }
}
