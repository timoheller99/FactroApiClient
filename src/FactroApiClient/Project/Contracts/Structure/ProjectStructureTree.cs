namespace FactroApiClient.Project.Contracts.Structure
{
    public class ProjectStructureTree
    {
        public string Id { get; set; }

        public StructureType Type { get; set; }

        public ProjectStructureTree Children { get; set; }
    }
}
