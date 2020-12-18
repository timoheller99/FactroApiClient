namespace FactroApiClient.Project.Contracts.Structure
{
    using System.Collections.Generic;

    public class ProjectStructureTree
    {
        public string Id { get; set; }

        public StructureType Type { get; set; }

        public IEnumerable<ProjectStructureTree> Children { get; set; }
    }
}
